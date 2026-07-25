/* -- 7648  -- */

#region btnImport

private void btnCustomerDebitsImport_Click(object sender, EventArgs e)
{
    #region define variables
    bool hasCustomerDebitSheet = false;
    //Create int variable to count inserted records
    int insertSuccessfulCount = 0;

    bool isValidRecord = true;

    //Create int variable to log failed inserts
    int insertFailCount = 0;

    //Create int variable to log failed reads
    int readFailCount = 0;

    //Create string variable for error log
    string errorLog = string.Empty;
    var sbErrorLog = new StringBuilder();

    //Log target Database detail
    string dbInfoLog = string.Empty;

    //Prep sql connection string
    string sqlConnectionString;
    if (Settings.Default.SQLIntegratedAuthentication == true)
    {
        //sqlConnectionString = "Server=" + Settings.Default.SQLServer + ";Database=" + Settings.Default.DatabaseName + ";Integrated Security=True";
        sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", Settings.Default.SQLServer,
        Settings.Default.DatabaseName);
    }
    else
    {
        //sqlConnectionString = "Server=" + Settings.Default.SQLServer + ";Database=" + Settings.Default.DatabaseName + ";User Id=" + Settings.Default.SQLUser + ";Password=" + Crypto.DecryptStringAES(Settings.Default.SQLPass, "bayern");
        sqlConnectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password= {3}", Settings.Default.SQLServer,
        Settings.Default.DatabaseName, Settings.Default.SQLUser, Crypto.DecryptStringAES(Settings.Default.SQLPass, "bayern"));
    }

    // Declare OpenFileDialog so the user can select a spreadsheet.
    OpenFileDialog openFileDialog1 = new OpenFileDialog();
    openFileDialog1.Filter = "XLSX Files|*.xlsx";
    openFileDialog1.Title = "Select Capstone Customer Import Template Spreadsheet";
    #endregion

    // Show the Dialog and catch for cancel button
    if (openFileDialog1.ShowDialog() == DialogResult.OK)
    {
        // Lock Busy Form
        LockBusyForm();

        //Process in background process
        BackgroundWorker worker = new BackgroundWorker();

        #region Set_Worker_DoWork_Event
        worker.DoWork += (o, args) =>
        {
            AuxClass ax = new AuxClass();
            string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
            string filenameNoExt = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

            try
            {
                DataTable importCustomerDebitTable = new DataTable();
                importCustomerDebitTable = ExcelReader.ExcelToDataTable(Path.GetFullPath(openFileDialog1.FileName));
                hasCustomerDebitSheet = true;
                int rowNum = 0;
                int dataRowStart = 2;

                using (TransactionScope scope = new TransactionScope())
                {
                    DataRow drh = importCustomerDebitTable.Rows[0];
                    var result = ValidateColumnsHeader.ValidateColumnsHeaderForCustomerDebit(drh);

                    if (!result.IsValidHeader)
                    {
                        SpreadsheetHeaderError headerError = new SpreadsheetHeaderError();
                        headerError.ShowErrorInformation(result.ErrorHeader);
                        headerError.ShowDialog();

                        return;
                    }

                    foreach (DataRow dr in importCustomerDebitTable.Rows)
                    {
                        rowNum++;
                        if (rowNum < dataRowStart)
                            continue;
                        if (rowNum == dataRowStart)
                            rowNum++;

                        isValidRecord = true;

                        // verify Type is required.
                        string customerCode = dr[(int)CustomerDebitEnum.CustomerCode].ToString();
                        string invoice = dr[(int)CustomerDebitEnum.Invoice].ToString();
                        string amount = dr[(int)CustomerDebitEnum.Amount].ToString().Trim();
                        string invoiceDate = dr[(int)CustomerDebitEnum.InvoiceDate].ToString();
                        string dueDate = dr[(int)CustomerDebitEnum.DueDate].ToString();
                        string discountDate = dr[(int)CustomerDebitEnum.DiscountDate].ToString();
                        string discountPercentage = dr[(int)CustomerDebitEnum.DiscountPercentage].ToString();
                        string exchangeRate = dr[(int)CustomerDebitEnum.ExchangeRate].ToString();
                        string description = dr[(int)CustomerDebitEnum.Description].ToString();

                        DateTime InvoiceDate;
                        DateTime DueDate;
                        DateTime DiscountDate;

                        double Amount = 0;
                        decimal Percentage = 0M;
                        decimal ExchangeRate = 0M;

                        bool isValidAmount = double.TryParse(amount, out Amount);
                        bool isValidExchangeRate = decimal.TryParse(exchangeRate, out ExchangeRate);
                        bool isValidInvoiceDate = DateTime.TryParse(invoiceDate, out InvoiceDate);
                        bool isValidDueDate = DateTime.TryParse(dueDate, out DueDate);
                        bool isValidDiscountPercentage = decimal.TryParse(discountPercentage, out Percentage);
                        bool isValidDiscountDate = DateTime.TryParse(discountDate, out DiscountDate);

                        #region Valid CustomerCode
                        int customerId = 0;
                        if (string.IsNullOrEmpty(customerCode))
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Customer Code is required and the row has not been imported.",
                                rowNum));

                            isValidRecord = false;
                        }
                        else
                        { //4172
                            customerId = Customer.GetCustomerID(customerCode);
                        }

                        if (customerId == 0)
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Specified customer code not found '{1}' and the row has not been imported.",
                                rowNum, customerCode));

                            isValidRecord = false;
                        }
                        #endregion Valid CustomerCode

                        #region Valid Invoice
                        if (!string.IsNullOrEmpty(invoice))
                        {
                            if (invoice.Length > 15)
                            {
                                sbErrorLog.AppendLine(
                                string.Format(
                                    "Row '{0}' Specified Invoice # '{1}' is greater than 15 characters and the row has not been imported.",
                                    rowNum, invoice));

                                isValidRecord = false;
                            }
                        }
                        #endregion Valid CustomerCode 

                        #region Amount
                        if (!isValidAmount)
                        {
                            sbErrorLog.AppendLine(string.Format("Row '{0}' Invalid Amount and the row has not been imported.", rowNum));
                            isValidRecord = false;
                        }
                        if (string.IsNullOrEmpty(amount))
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Amount is required and the row has not been imported.",
                                rowNum));
                            isValidRecord = false;
                        }
                        if (Amount > 99999999.99 || Amount < 0)
                        {
                            errorLog += Environment.NewLine + "Amount '" + Amount +
                                        "' Invalid Amount and the row has not been imported.";
                            readFailCount++;
                        }
                        int erDecimals = BitConverter.GetBytes(decimal.GetBits(ExchangeRate)[3])[2];
                        if (erDecimals > 2)
                        {
                            sbErrorLog.AppendLine(
                                string.Format(Environment.NewLine + "Invalid Amount '" + Amount + "' and the row has not been imported."));
                            isValidRecord = false;
                        }
                        #endregion Amount

                        #region InvoiceDate
                        if (string.IsNullOrEmpty(invoiceDate))
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Invoice/Debit Date is required and the row has not been imported.",
                                rowNum));

                            isValidRecord = false;
                        }

                        if (!isValidInvoiceDate)
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Invalid Invoice/Debit Date '{1}' for Customer Code {2} and the row has not been imported.",
                                rowNum, InvoiceDate, customerCode));
                            isValidRecord = false;
                        }
                        #endregion InvoiceDate

                        #region dueDate
                        if (string.IsNullOrEmpty(dueDate))
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Due Date is required and the row has not been imported.",
                                rowNum));
                            isValidRecord = false;
                        }

                        if (!isValidDueDate)
                        {
                            sbErrorLog.AppendLine(
                            string.Format(
                                "Row '{0}' Invalid Due Date '{1}' for Customer Code {2} and the row has not been imported.",
                                rowNum, dueDate, customerCode));
                            isValidRecord = false;
                        }
                        #endregion dueDate

                        #region discountDate
                        if (!string.IsNullOrEmpty(discountDate))
                        {
                            if (!isValidDiscountDate)
                            {
                                sbErrorLog.AppendLine(
                                string.Format(
                                    "Row '{0}' Invalid Discount Date '{1}' for Customer Code {2} and the row has not been imported.",
                                    rowNum, discountDate, customerCode));
                                isValidRecord = false;
                            }
                        }
                        #endregion discountDate

                        #region DiscountPercentage
                        if (!string.IsNullOrEmpty(discountPercentage) && isValidDiscountPercentage)
                        {
                            if (Percentage > 99.99M || Percentage < 0)
                            {
                                sbErrorLog.AppendLine(
                                string.Format(Environment.NewLine + "Invalid Discount Percentage '" + discountPercentage + "' and the row has not been imported."));
                                isValidRecord = false;
                            }
                        }
                        #endregion DiscountPercentage

                        #region ExchangeRate
                        if (!string.IsNullOrEmpty(exchangeRate) && isValidExchangeRate)
                        {
                            int countDecimals = BitConverter.GetBytes(decimal.GetBits(ExchangeRate)[3])[2];

                            if (ExchangeRate > 100000 || ExchangeRate < 0)
                            {
                                sbErrorLog.AppendLine(
                                    string.Format(Environment.NewLine + "Invalid Exchange Rate '" + exchangeRate + "' and the row has not been imported."));
                                isValidRecord = false;
                            }

                            if (countDecimals > 10)
                            {
                                sbErrorLog.AppendLine(
                                    string.Format(Environment.NewLine + "Invalid Exchange Rate '" + exchangeRate + "' and the row has not been imported."));
                                isValidRecord = false;
                            }
                            else
                            {
                                ExchangeRate = 1;
                            }
                        }
                        #endregion ExchangeRate

                        #region Description
                        if (!string.IsNullOrEmpty(description))
                        {
                            if (description.Length > 30)
                            {
                                sbErrorLog.AppendLine(
                                string.Format("Row '{0}' Specified Description '{1}' is greater than 30 characters and the row has not been imported.", rowNum, description));

                                isValidRecord = false;
                            }
                        }
                        #endregion Description

                        if (isValidRecord && string.IsNullOrEmpty(sbErrorLog.ToString()))
                        {
                            int customerDebitId = 0;
                            CustomerDebit customer = null;
                            try
                            {
                                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                                {
                                    connection.Open();
                                    SqlCommand sqlCommand =
                                        new SqlCommand(
                                            DataImportSqlStatements.CustomerDebit,
                                            connection);

                                    /// Step 7 - Add a New Record to CustomerDebit (CD) table
                                    try
                                    {
                                        sqlCommand.Parameters.AddWithValue("@customerId", customerId);
                                        sqlCommand.Parameters.AddWithValue("@PostedFlag", true);
                                        sqlCommand.Parameters.AddWithValue("@PostedByLoginUserId", 1);
                                        sqlCommand.Parameters.AddWithValue("@PostedDateTime", DateTime.Now);
                                        sqlCommand.Parameters.AddWithValue("@TransactionDate", InvoiceDate);
                                        sqlCommand.Parameters.AddWithValue("@Amount", Amount);
                                        sqlCommand.Parameters.AddWithValue("@ExchangeRate", ExchangeRate);
                                        sqlCommand.Parameters.AddWithValue("@OffsetGeneralLedgerAccountId", DBNull.Value);
                                        sqlCommand.Parameters.AddWithValue("@DocumentNumber", invoice);
                                        sqlCommand.Parameters.AddWithValue("@Description", description);
                                        sqlCommand.Parameters.AddWithValue("@DueDate", DueDate);
                                        sqlCommand.Parameters.AddWithValue("@DiscountDate", DiscountDate);
                                        sqlCommand.Parameters.AddWithValue("@DiscountPercentage", Percentage);

                                        //Insert CustomerDebit record, get the new customerDebitId 7969
                                        customerDebitId = (int)sqlCommand.ExecuteScalar();
                                        customer = GetCustomerDebitByCustomerCode(customerDebitId);
                                    }
                                    catch (Exception)
                                    {
                                        /// Duplicate record error based on the indexes
                                        sbErrorLog.AppendLine(string.Format("Row '{0}' Duplicate invoice number and the row has not been imported.",
                                                rowNum, description));
                                        isValidRecord = false;
                                        continue;
                                    }

                                    SqlCommand sqlCommandCT =
                                       new SqlCommand(
                                           DataImportSqlStatements.CustomerTransaction,
                                           connection);

                                    /// Step 8 - Add a New Record to CustomerTransaction (CT) table
                                    sqlCommandCT.Parameters.AddWithValue("@customerId", customer.CustomerId);
                                    sqlCommandCT.Parameters.AddWithValue("@TransactionType", 3);
                                    sqlCommandCT.Parameters.AddWithValue("@DueDate", customer.DueDate);
                                    sqlCommandCT.Parameters.AddWithValue("@TransactionDate", customer.TransactionDate);
                                    if (ExchangeRate > 0)
                                    {
                                        sqlCommandCT.Parameters.AddWithValue("@Amount", (decimal)customer.Amount / customer.ExchangeRate);
                                    }
                                    else
                                    {
                                        sqlCommandCT.Parameters.AddWithValue("@Amount", 0M);
                                    }
                                    sqlCommandCT.Parameters.AddWithValue("@BalanceDue", customer.Amount);
                                    sqlCommandCT.Parameters.AddWithValue("@CustomerDebitId", customerDebitId);

                                    if (ExchangeRate > 0)
                                    {
                                        sqlCommandCT.Parameters.AddWithValue("@ExchangeRate", customer.ExchangeRate);
                                    }
                                    else
                                    {
                                        sqlCommandCT.Parameters.AddWithValue("@ExchangeRate", DBNull.Value);
                                    }
                                    if (!string.IsNullOrEmpty(invoice))
                                    {
                                        sqlCommandCT.Parameters.AddWithValue("@DocumentNumber", customer.DocumentNumber);
                                    }
                                    else
                                    {
                                        sqlCommandCT.Parameters.AddWithValue("@DocumentNumber", customerDebitId);
                                    }
                                    sqlCommandCT.Parameters.AddWithValue("@Description", customer.Description);
                                    sqlCommandCT.Parameters.AddWithValue("@DiscountDate", customer.DiscountDate);
                                    sqlCommandCT.Parameters.AddWithValue("@DiscountPercentage", customer.DiscountPercentage);

                                    sqlCommandCT.ExecuteNonQuery();

                                    insertSuccessfulCount++;
                                }
                            }
                            catch (Exception innerEx)
                            {
                                sbErrorLog.AppendLine(string.Format("Row {0}, Sql error: {1}", rowNum, innerEx.Message));
                                insertFailCount++;
                            }
                        }
                        else
                        {
                            readFailCount++;
                        }

                    }
                    errorLog = sbErrorLog.ToString();

                    string caption = "Import Results";

                    string msg = "Records successfully processed: " + insertSuccessfulCount + Environment.NewLine +
                                 "Records unable to be inserted: " + insertFailCount + Environment.NewLine +
                                 "Records unable to be validated: " + readFailCount;

                    if (insertSuccessfulCount > 0)
                    {
                        msg += Environment.NewLine + Environment.NewLine +
                                 DATABASE_PERSIST_SUCCESSFULLY_IMPORTED_MESSAGE;

                        if (MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            //Commit the change
                            scope.Complete();
                    }
                    else
                    {
                        MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (errorLog.Length > 0)
                    {
                        ax.LogFile(errorLog, filenameNoExt + " Error LogFile", directoryPath + "\\");
                    }

                }
            }
            catch (Exception ex)
            {
                string message = "";
                string title = "";
                if (hasCustomerDebitSheet)
                {
                    title = "Open Spreadsheet";
                    message = "Data import failed, please see error log";
                }
                else
                {
                    title = "Error reading the Excel File";
                    message = "Error reading the Excel File: " + ex.Message;
                }
                MessageBox.Show(message, title, MessageBoxButtons.OK);
            }

        };
        #endregion Set_Worker_DoWork_Event
        worker.RunWorkerCompleted += (o, args) =>
        {
            UnlockBusyForm();
            this.BringToFront();
            this.Cursor = Cursors.Default;
        };
        worker.RunWorkerAsync();
    }
}


#endregion btnImport

public enum CustomerDebitEnum
{
    CustomerCode = 0,
    Invoice = 1,
    Amount = 2,
    InvoiceDate = 3,
    DueDate = 4,
    DiscountDate = 5,
    DiscountPercentage = 6,
    ExchangeRate = 7,
    Description = 8,
}