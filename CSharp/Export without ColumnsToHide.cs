public string ExportCustomerDebits(CustomerDebitsInput input, string filePath)
{
    try
    {
        using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
        {
            string fileName = string.Empty;
            // Excel Filename and path.
            fileName = string.Format("ExportCustomerDebits{0}.xlsx", DateTime.Now.ToString("MMddyyyhhmssfff"));
            string filefullPath = string.Concat(filePath, fileName);
            var totalColumnsFile = 8;

            var exportExportCustomerDebitsDataResponseModel = new ExportCustomerDebitsResponseModel();
            var rowIndex = 0;
            CustomerDebitResult customerDebitResult = null;
            List<CustomerDebitResult> debitResults = new List<CustomerDebitResult>();

            #region Headers

            #region Title
            var reportTitle = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = ImportExportConstants.CustomerDebits,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowReportTitle = new HeaderRowDefinition();
            rowReportTitle.RowIndex = rowIndex++;
            rowReportTitle.RowCells.Add(reportTitle);
            exportExportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowReportTitle);
            #endregion Title

            #region ReportDate
            string reportExportDate = DateTime.Now.ToString("M/d/yyyy h:mm tt");
            var rowReporExportDateTime = new HeaderRowDefinition();
            var reportExportDateTime = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = "Export Date/Time: " + reportExportDate,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporExportDateTime.RowIndex = rowIndex++;
            rowReporExportDateTime.RowCells.Add(reportExportDateTime);
            exportExportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowReporExportDateTime);
            #endregion ReportDate

            #region CustomerGroupId
            if (input.CustomerGroupId > 0)
            {
                rowIndex++;
                string customerGroupDescription = context.CustomerGroupEntities.Where(x => x.GroupId == input.CustomerGroupId).Select(x => x.Description).FirstOrDefault();
                var customerGroupCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Customer Group: {customerGroupDescription}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCustomerGroup = new HeaderRowDefinition();
                rowCustomerGroup.RowIndex = rowIndex;
                rowCustomerGroup.RowCells.Add(customerGroupCell);
                exportExportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowCustomerGroup);
            }
            #endregion CustomerGroupId

            #region CustomerId
            if (input.CustomerId > 0)
            {
                rowIndex++;
                string customerCode = context.CustomerEntities.Where(x => x.CustomerId == input.CustomerId).Select(x => x.CustomerCode).FirstOrDefault();
                var customerNameCell = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = $"Customer Code: {customerCode}",
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowCustomerName = new HeaderRowDefinition();
                rowCustomerName.RowIndex = rowIndex;
                rowCustomerName.RowCells.Add(customerNameCell);
                exportExportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowCustomerName);
            }
            #endregion CustomerId

            #region TransactionDate
            if (input.LowTransactionDate.HasValue && input.HighTransactionDate.HasValue)
            {
                rowIndex++;
                var reportTransactionDate = string.Empty;
                if (input.LowTransactionDate == input.HighTransactionDate)
                {
                    reportTransactionDate = $"Transaction Date: {input.LowTransactionDate.Value.ToString("M/d/yyyy")}";
                }
                if (input.LowTransactionDate.HasValue && input.HighTransactionDate.HasValue && input.LowTransactionDate < input.HighTransactionDate)
                {
                    reportTransactionDate = $"Transaction Date: {input.LowTransactionDate.Value.ToString("M/d/yyyy")} thru {input.HighTransactionDate.Value.ToString("M/d/yyyy")}";
                }
                if (input.LowTransactionDate.HasValue && input.HighTransactionDate == null)
                {
                    reportTransactionDate = $"Transaction Date: On or after {input.LowTransactionDate.Value.ToString("M/d/yyyy")}";
                }
                if (input.LowTransactionDate == null && input.HighTransactionDate.HasValue)
                {
                    reportTransactionDate = $"Transaction Date: On or before {input.HighTransactionDate.Value.ToString("M/d/yyyy")}";
                }

                var reportHeaderQuoteDate = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = reportTransactionDate,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReporDateTime = new HeaderRowDefinition();

                rowReporDateTime.RowIndex = rowIndex;
                rowReporDateTime.RowCells.Add(reportHeaderQuoteDate);
                exportExportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            }
            #endregion TransactionDate

            #region reportRowseparation
            var reportHeaderRowSeparation = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = string.Empty,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowSeparator = new HeaderRowDefinition();
            rowSeparator.RowIndex = rowIndex++;
            rowSeparator.RowCells.Add(reportHeaderRowSeparation);
            exportExportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowSeparator);
            #endregion reportRowseparation

            #endregion Headers

            IQueryable<CustomerDebitQuery> result = (from cd in context.CustomerDebitEntities
                                                     join cus in context.CustomerEntities on cd.CustomerId equals cus.CustomerId
                                                     select new CustomerDebitQuery
                                                     {
                                                         Debit = cd.CustomerDebitId,
                                                         TransactionDate = cd.TransactionDate,
                                                         DueDate = cd.DueDate,
                                                         CustomerCode = cus.CustomerCode,
                                                         CustomerName = cus.Name,
                                                         Amount = cd.Amount,
                                                         DocumentNumber = cd.DocumentNumber,
                                                         Description = cd.Description,
                                                         CustomerId = cus.CustomerId,
                                                         Group1CustomerGroupId = cus.Group1CustomerGroupId,
                                                         Group2CustomerGroupId = cus.Group2CustomerGroupId,
                                                         Group3CustomerGroupId = cus.Group3CustomerGroupId,
                                                         Group4CustomerGroupId = cus.Group4CustomerGroupId,
                                                         Group5CustomerGroupId = cus.Group5CustomerGroupId,
                                                     });

            /// *************
            #region  Filters


            if (input.CustomerId.HasValue && input.CustomerId > 0)
            {
                result = result.Where(x => x.CustomerId == input.CustomerId);
            }
            if (input.CustomerGroupId.HasValue && input.CustomerGroupId > 0)
            {
                result = result.
                        Where(x => x.Group1CustomerGroupId == input.CustomerGroupId
                        || x.Group2CustomerGroupId == input.CustomerGroupId
                        || x.Group3CustomerGroupId == input.CustomerGroupId
                        || x.Group4CustomerGroupId == input.CustomerGroupId
                        || x.Group5CustomerGroupId == input.CustomerGroupId);
            }
            if (input.LowTransactionDate.HasValue)
            {
                result = result.Where(x => x.TransactionDate >= input.LowTransactionDate);
            }
            if (input.HighTransactionDate.HasValue)
            {
                result = result.Where(x => x.TransactionDate <= input.HighTransactionDate);
            }

            #endregion  Filters
            /// *************

            var list = result.OrderBy(o => o.Debit).ThenBy(o => o.TransactionDate).ToList();

            foreach (var item in list)
            {
                customerDebitResult = new CustomerDebitResult();

                customerDebitResult.Debit = item.Debit;
                customerDebitResult.TransactionDate = item.TransactionDate;
                customerDebitResult.DueDate = item.DueDate;
                customerDebitResult.CustomerCode = item.CustomerCode;
                customerDebitResult.CustomerName = item.CustomerName;
                customerDebitResult.Amount = item.Amount;
                customerDebitResult.DocumentNumber = item.DocumentNumber;
                customerDebitResult.Description = item.Description;

                debitResults.Add(customerDebitResult);
            }

            exportExportCustomerDebitsDataResponseModel.ColumnsToHide = new int[] { }; // implement it without ColumnsToHide
            exportExportCustomerDebitsDataResponseModel.TotalColumns = totalColumnsFile;
            exportExportCustomerDebitsDataResponseModel.DetailData.DetailRows.AddRange(debitResults);


            /// See ExportCustomerDebitsLogic how implement it without ColumnsToHide
            var workbook =
                (new ExportCustomerDebitsLogic()).ExportToExcelSheetProtect(
                    exportExportCustomerDebitsDataResponseModel.TotalColumns,
                    ImportExportConstants.CustomerDebits, exportExportCustomerDebitsDataResponseModel, false);

            Shared.Utilities.OutPutDestinationPathValidator.ValidateDestinationPath(filePath);

            using (var fileStream = new FileStream(filefullPath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
                return fileName;
            }
        }
    }
    catch (OutOfMemoryException ome)
    {
        log_.Info($"Work Order Output Export failed, Exception: {ome.Message}.");
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Too many rows returned. Please narrow the filter criteria.");
    }
    catch (Exception ex)
    {
        log_.Info($"Work Order Output Export failed, Exception: {ex.Message}.");
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Work Order Output Export failed.");
    }
}
