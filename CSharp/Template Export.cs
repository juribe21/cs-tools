public int ExportCustomers(ExportCustomersInput input, string filePath, int userId, string sessionId)
{
    RunningReport runningReport = new RunningReport();

    using (CapstoneModelDataContext contex = new CapstoneModelDataContext(this.ConnectionString))
    {
        var runningReportAccessor = new RunningReportEntityAccessor(contex);
        runningReport = new RunningReport
        {
            StartedUTCDateTime = DateTime.UtcNow,
            LoginUserId = userId,
            LoginSessionId = sessionId,
            MethodName = "ExportCustomers"
        };

        runningReport = runningReportAccessor.Insert(runningReport);
    }

    if (runningReport.RunningReportId > 0)
    {
        ///process the report in background
        System.Threading.Tasks.Task.Run(() =>
        {
            ExportCustomers(input, filePath, userId, runningReport);
        }).ConfigureAwait(false);
    }
    log_.Info($"Before ExportCustomers Started [Thread Id: {Thread.CurrentThread.ManagedThreadId}]. Returned RunningReportId: {runningReport.RunningReportId}");
    return runningReport.RunningReportId;
}


/* -------------------------------------------------------------------------------------------------- */


public void ExportVendorRefunds(ExportCashRequirementsInput input, string filePath, int userId, RunningReport runningReport)
{
    string logInfoReportName = "Export Vendor efunds";
    try
    {
        string fileName = string.Empty;
        // Excel Filename and path.
        fileName = string.Format("ExportVendorRefunds{0}.xlsx", DateTime.Now.ToString("MMddyyyhhmssfff"));
        string filefullPath = string.Concat(filePath, fileName);
        var totalColumnsFile = 7;

        DateTime? _targettedLowDate = input.TargetedDateLow.HasValue ? input.TargetedDateLow.Value.Date : (DateTime?)null;
        DateTime? _targettedHighDate = input.TargetedDateHigh.HasValue ? input.TargetedDateHigh.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

        // with SheetNumber, Currencies, CustomerId
        var exportCustomerTransactions_XXXXX_ResponseModel = new ExportBankTransactionsDataResponseModel();
        var rowIndex = 0;

        XxxxxxxxxxxxxxResult customerTransaction = null;
        List<XxxxxxxxxxxxxxResult> customerTransactions = new List<XxxxxxxxxxxxxxResult>();

        using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
        {
            #region Header and Date Report

            #region Title
            var reportTitle = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = ImportExportConstants.ExportMiscellaneousCustomerCredits,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowReportTitle = new HeaderRowDefinition();
            rowReportTitle.RowIndex = rowIndex++;
            rowReportTitle.RowCells.Add(reportTitle);
            exportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowReportTitle);
            #endregion Title

            #region ReportDate
            string reportExportDate = DateTime.Now.ToString("M/d/yyyy h:mm tt");
            var rowReporExportDateTime = new HeaderRowDefinition();
            var reportExportDateTime = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = reportExportDate,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporExportDateTime.RowIndex = rowIndex++;
            rowReporExportDateTime.RowCells.Add(reportExportDateTime);
            exportCustomerDebitsDataResponseModel.HeaderData.Rows.Add(rowReporExportDateTime);
            #endregion ReportDate

            /*
                . . . 
            */

            #region HeaderRowSeparation
            var reportHeaderRowSeparation = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = string.Empty,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowSeparator = new HeaderRowDefinition();
            rowSeparator.RowIndex = rowIndex++;
            rowSeparator.RowCells.Add(reportHeaderRowSeparation);
            exportVendorDebitDataResponseModel.HeaderData.Rows.Add(rowSeparator);
            #endregion HeaderRowSeparation

            #endregion Header and Date Report

            /// {
            /// 
            /// **** Data and Logic
            ///
            /// }

            if (!customerTransactions.Any())
            {
                log_.Info("customerTransactions : No records returned for given criteria.");
                runningReport.ErrorMessage = "No records found.";
                return;
            }

            exportCustomerTransactionsResponseModel.ColumnsToHide = new int[] { };
            exportCustomerTransactionsResponseModel.TotalColumns = totalColumnsFile;
            exportCustomerTransactionsResponseModel.SheetNumber = 1;
            exportCustomerTransactionsResponseModel.DetailData.DetailRows.AddRange(customerTransactions);


            /// *** ExportToExcelSheetProtect *** ///
            var workbook = (new ExportCustomerTransactionsLogic())
                .ExportToExcel(
                    exportTrailBalanceSummaryResponseModel.TotalColumns,
                    ImportExportConstants.SheetName + exportTrailBalanceSummaryResponseModel.SheetNumber.ToString(),
                    exportTrailBalanceSummaryResponseModel);

            Shared.Utilities.OutPutDestinationPathValidator.ValidateDestinationPath(filePath);

            using (var fileStream = new FileStream(filefullPath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
                runningReport.CompletedFileName = fileName;
                runningReport.CompletedUTCDateTime = DateTime.UtcNow;
                runningReport.ErrorMessage = string.Empty;
            }
        }
    }
    catch (OutOfMemoryException ome)
    {
        runningReport.ErrorMessage = $"{ome.Message}";
        runningReport.CanceledUTCDateTime = DateTime.UtcNow;
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Too many rows returned. Please narrow the filter criteria.");
    }
    catch (Exception ex)
    {
        log_.Info($"{logInfoReportName} failed, Exception: {ex.Message}.");
        runningReport.ErrorMessage = $"{logInfoReportName} failed.";
        runningReport.CanceledUTCDateTime = DateTime.UtcNow;
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, $"{logInfoReportName} failed.");
    }
    finally
    {
        var runningReportAccessor = new RunningReportEntityAccessor(this.Context);
        runningReportAccessor.Update(runningReport);
    }


}



namespace Bayern.CapstoneService.DAL.ExcelUtility.ResponseModels
{
    public class ExportCustomerTransactionsResponseModel
    {
        public int[] ColumnsToHide { get; set; }
        public int TotalColumns { get; set; }
        public int SheetNumber { get; set; }
        public ExportCustomerTransactionsHeaderDataResponseModel HeaderData { get; private set; }
        public ExportCustomerTransactionsDetailDataResponseModel DetailData { get; private set; }
        public ExportCustomerTransactionsResponseModel()
        {
            HeaderData = new ExportCustomerTransactionsHeaderDataResponseModel();
            DetailData = new ExportCustomerTransactionsDetailDataResponseModel();
        }
    }
}


/*


 #region HeaderTitle Date
            var reportTitle = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = ImportExportConstants.ExportVendorDebits,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            var rowReportTitle = new HeaderRowDefinition();
            rowReportTitle.RowIndex = rowIndex++;
            rowReportTitle.RowCells.Add(reportTitle);
            exportVendorDebitDataResponseModel.HeaderData.Rows.Add(rowReportTitle);

            string reportDate = DateTime.Now.ToString("M/d/yyyy h:mm tt");
            var rowReporDateTime = new HeaderRowDefinition();
            var reportDateTime = new HeaderCellDefinition
            {
                CellIndex = 0,
                CellValue = reportDate,
                TextAlignment = CellTextHorizontalAlignment.Left
            };
            rowReporDateTime.RowIndex = rowIndex++;
            rowReporDateTime.RowCells.Add(reportDateTime);
            exportVendorDebitDataResponseModel.HeaderData.Rows.Add(rowReporDateTime);
            #endregion HeaderTitle Date

            #region Currency

                    var reportCurrencyId = new HeaderCellDefinition
                    {
                        CellIndex = 0,
                        CellValue = "Currency: " + context.CurrencyEntities.Where(x => x.CurrencyId == input.CurrencyId).FirstOrDefault().Description,
                        TextAlignment = CellTextHorizontalAlignment.Left
                    };
                    var rowReportCurrencyId = new HeaderRowDefinition();
                    rowReportCurrencyId.RowIndex = rowIndex + 1;
                    rowReportCurrencyId.RowCells.Add(reportCurrencyId);
                    scheduledVendorPaymentDataResponseModel.HeaderData.Rows.Add(rowReportCurrencyId);

            #endregion Currency


            #region vendorGroupId
            if (vendorGroupId > 0)
            {
                var reportVendorGroupId = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = "Vendor Group: " + context.VendorGroupEntities.Where(x => x.GroupId == vendorGroupId).FirstOrDefault().Description,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReportVendorGroupId = new HeaderRowDefinition();
                rowReportVendorGroupId.RowIndex = rowIndex + 1;
                rowReportVendorGroupId.RowCells.Add(reportVendorGroupId);
                exportVendorDebitDataResponseModel.HeaderData.Rows.Add(rowReportVendorGroupId);
            }
            #endregion vendorGroupId

            #region vendor
            if (vendorId > 0)
            {
                var reportVendorId = new HeaderCellDefinition
                {
                    CellIndex = 0,
                    CellValue = "Vendor: " + context.VendorEntities.Where(x => x.VendorId == vendorId).FirstOrDefault().VendorName,
                    TextAlignment = CellTextHorizontalAlignment.Left
                };
                var rowReportVendorId = new HeaderRowDefinition();
                rowReportVendorId.RowIndex = rowIndex + 1;
                rowReportVendorId.RowCells.Add(reportVendorId);
                exportVendorDebitDataResponseModel.HeaderData.Rows.Add(rowReportVendorId);
            }
            #endregion vendor

            #region Transaction Date
            if (input.LowTransactionDate.HasValue || input.HighTransactionDate.HasValue)
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

                if (!String.IsNullOrEmpty(reportTransactionDate))
                {
                    var rowReporFilterDateTime = new HeaderRowDefinition();
                    var reportFilterDateTime = new HeaderCellDefinition
                    {
                        CellIndex = 0,
                        CellValue = reportTransactionDate,
                        TextAlignment = CellTextHorizontalAlignment.Left
                    };
                    rowReporFilterDateTime.RowIndex = rowIndex++;
                    rowReporFilterDateTime.RowCells.Add(reportFilterDateTime);
                    exportVendorDebitDataResponseModel.HeaderData.Rows.Add(rowReporFilterDateTime);
                }
            }
            #endregion Transaction Date

*/