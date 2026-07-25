
/// #13676 Add New Methods to Export / Update General Ledger Beginning Balances
// ***** GeneralLedgerService ***** //
public string UpdateItemCodeSpreadsheet(SoapUserSession session, byte[] excelSpreadsheet)
{
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        ItemEntityAccessor accessorItem = new ItemEntityAccessor(this.ConnectionString);

        string logDirectory = string.Concat(Server.MapPath("/"), @"\", @"Services\ImportExport\");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        return accessorItem.UpdateItemCodeSpreadsheet(fileBytes: excelSpreadsheet, logDirectory: logDirectory);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.FailedToUpdateSpreadsheet, "Failed to Update ItemCode data, see error log."));
    }
}

/* ******************************************************************************** */

public string UpdateItemCodeSpreadsheet(byte[] fileBytes, string logDirectory)
{

    //Bayern.CapstoneService.Shared.Utilities class
    var sb = new stringbuilder();
    string errorFileName = string.Empty;
    string errorMessage = string.Empty;
    bool hasValidSheet = false;

    //Get the fileName 
    var fileName = System.IO.Path.GetTempFileName();

    //Get connections to DataBase Tables
    var itemEntityAccessor = new ItemEntityAccessor(this.ConnectionString);

    try
    {

        // Assuming that fileBytes is a byte[] containing what you read from your database        
        System.IO.File.WriteAllBytes(fileName, fileBytes);

        //Read the SpreadSheet ItemCodeExport, if exist any error write into errorLog File
        SLExcelData slExcelData = new SLExcelReader().ReadExcel(fileName,
            ImportExportConstants.ItemCodeExportSheetName);
        if (!slExcelData.Status.Success)
        {
            sb.AppendToStringBuilder("Error reading uploaded file: " + slExcelData.Status.Message);
        }

        // variable to write the rowNumber into the errorLogFile
        int excelRowNumber = 1;
        hasValidSheet = true;
        foreach (var excelDR in slExcelData.DataRows)
        {
            //Auxiliar Variable
            bool rowIsValid = true;

            //Read the rows in the spreadSheet
            excelRowNumber++;


            //Read the fields in SpreadSheet
            string excelItemId = excelDR[(int)ItemCodeExportColumnEnum.ItemId].ToString();
            string excelCurrentItemCode = excelDR[(int)ItemCodeExportColumnEnum.CurrentItemCode].ToString();
            string excelDescription = excelDR[(int)ItemCodeExportColumnEnum.Description].ToString();
            string excelNewItemCode = excelDR[(int)ItemCodeExportColumnEnum.NewItemCode].ToString();
            excelNewItemCode = excelNewItemCode.ToUpper();

            //step - 1 Check if the row should be processed 

            //if NewItemCode is null ignore and move to next record without write into errorLogFile
            if (string.IsNullOrEmpty(excelNewItemCode))
            {
                continue;
            }

            // Step - 2 Validate the New Item Code.	
            // if is greater than 25 characters write into errorLogFile.
            if (excelNewItemCode.Trim().Length > 25)
            {
                sb.AppendToStringBuilder(
                    string.Format("Row: {0} new item Code is greater than 25 characters.",
                        excelRowNumber));
                rowIsValid = false;
            }

            // Step-3 Check to See if Item Record Exists.	
            // Read Item Table Where Item.Item_Id = excel.ItemId if record not found write into ErrorLogFile.

            int itemId = 0;
            int.TryParse(excelItemId, out itemId);
            var item = itemEntityAccessor.Where(x => x.ItemId == itemId).FirstOrDefault();

            if (item == null)
            {
                sb.AppendToStringBuilder(
                    string.Format("Row: {0} Specified item not found.",
                        excelRowNumber));
                rowIsValid = false;
                continue;
            }

            //Step-4 Check to See if New Item Code Already Exists
            //Read Item Table But to compare if ItemCode exists if exists write into errorLogFile
            if (!string.IsNullOrEmpty(excelNewItemCode))
            {

                var itemCode = (from itemE in Context.ItemEntities
                                select new { ItemCode = itemE.ItemCode, ItemId = itemE.ItemId })
                    .FirstOrDefault(x => x.ItemCode.ToUpper() == excelNewItemCode.ToUpper() &&
                                         x.ItemId != itemId);
                if (itemCode != null)
                {

                    sb.AppendToStringBuilder(
                        string.Format("Row: {0} Specified new item code already exists.",
                            excelRowNumber));
                    rowIsValid = false;
                }
            }

            //Step 5 - Update the Item Record
            if (rowIsValid && !string.IsNullOrEmpty(excelNewItemCode))
            {
                // update the Record into Item Table set Item.ItemCode = newItemCode.
                // Create The String Sql to update the record.
                string sql = string.Format("UPDATE Item SET ItemCode = '{0}' WHERE Item_Id = {1}",
                    excelNewItemCode,
                    itemId);

                //update
                Context.ExecuteCommand(sql);

            }
            else
            {
                // if record is invalid move to next record.
                continue;
            }
        }

    }
    catch (Exception ex)
    {
        //Invalid SpreadSheet
        if (!hasValidSheet)
        {
            sb.AppendToStringBuilder("Invalid Spreadsheet " + "Error: " + ex.Message);

        }
        else
        {
            sb.AppendToStringBuilder("Error: " + ex.ToString());
        }

    }
    finally
    {
        //Write error message to log
        errorMessage = sb.ToString;
        if (!string.IsNullOrEmpty(errorMessage))
        {
            errorFileName = string.Format("ItemCodeExport_Error_{0}.txt",
                DateTime.Now.ToString("MMddyyyhhmssfff"));
            string destinationFile = string.Concat(logDirectory, errorFileName);
            System.IO.File.WriteAllText(destinationFile, errorMessage);
        }
        //Cleanup
        System.IO.File.Delete(fileName);
    }

    return errorFileName;
}
