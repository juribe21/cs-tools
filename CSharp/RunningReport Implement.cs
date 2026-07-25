/* -------------------------------------------------------------------------------------------------- */



/* -------------------------------------------------------------------------------------------------- */

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

public void ExportCustomers(ExportCustomersInput input, string filePath, int userId, RunningReport runningReport)
{
    try
    {
        int totalColumnsFile = 81;
        string fileName = string.Format("ExportCustomers_{0}.xlsx", DateTime.Now.ToString("MMddyyyyhhmssfff"));
        string filefullPath = string.Concat(filePath, fileName);

        ExportUserPreferenceseHelper.StoreColumnsToHideUserPreference(this.Context, userId, System.Reflection.MethodBase.GetCurrentMethod().Name, input.ColumnsToHide);

        #region Check UDF to Hide
        var cusInit = Context.CustomerInitEntities.FirstOrDefault();
        if (cusInit != null)
        {
            if (string.IsNullOrEmpty(cusInit.UDF1Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "71" : ",71";
            if (string.IsNullOrEmpty(cusInit.UDF2Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "72" : ",72";
            if (string.IsNullOrEmpty(cusInit.UDF3Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "73" : ",73";
            if (string.IsNullOrEmpty(cusInit.UDF4Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "74" : ",74";
            if (string.IsNullOrEmpty(cusInit.UDF5Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "75" : ",75";
            if (string.IsNullOrEmpty(cusInit.UDF6Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "76" : ",76";
            if (string.IsNullOrEmpty(cusInit.UDF7Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "77" : ",77";
            if (string.IsNullOrEmpty(cusInit.UDF8Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "78" : ",78";
            if (string.IsNullOrEmpty(cusInit.UDF9Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "79" : ",79";
            if (string.IsNullOrEmpty(cusInit.UDF10Label))
                input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "80" : ",80";
        }

        int accountingIntegration_Type = this.Context.ThirdPartyAccountingInitEntities.FirstOrDefault().AccountingIntegrationType;
        if (accountingIntegration_Type != 1 && accountingIntegration_Type != 4)
        {
            input.ColumnsToHide += string.IsNullOrEmpty(input.ColumnsToHide) ? "2" : ",2";
        }

        #endregion


        int[] ctha = Utilities.ValidateColumnsToHide(input.ColumnsToHide, totalColumnsFile);

        int columnsToShow = totalColumnsFile - ctha.Length;
        int totalcolumns = columnsToShow > 3 ? columnsToShow : 3;

        //----

        if (!exportList.Any())
        {
            log_.Info("CreateReplacementCostSpreadsheet : No records found.");
            runningReport.ErrorMessage = "No records found.";
            return;
        }

        /* ************************************************************************************ */

        if (!exportList.Any())
        {
            log_.Info("exportList : No records found.");
            runningReport.ErrorMessage = "No records found.";
            return;
        }

        if (!exportList.Any())
        {
            log_.Info("exportList : No records found.");
            runningReport.ErrorMessage = "No records found.";
            return;
        }

        // -------------------

        var workbook =
             (new ExportCustomersLogic()).ExportToExcel(exportCustomersDataResponseModel.TotalColumns,
                                                        ImportExportConstants.ExportCustomers, exportCustomersDataResponseModel);

        Shared.Utilities.OutPutDestinationPathValidator.ValidateDestinationPath(filePath);

        using (var fileStream = new FileStream(filefullPath, FileMode.Create, FileAccess.Write))
        {
            workbook.Write(fileStream);

            runningReport.CompletedFileName = fileName;
            runningReport.CompletedUTCDateTime = DateTime.UtcNow;
            runningReport.ErrorMessage = string.Empty;

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
        log_.Info($"Work Order Output Export failed, Exception: {ex.Message}.");
        runningReport.ErrorMessage = "Work Order Output Export failed.";
        runningReport.CanceledUTCDateTime = DateTime.UtcNow;
        throw new CapstoneException(BusinessLogicException.ExportToExcelFailed, "Work Order Output Export failed.");
    }
    finally
    {
        var runningReportAccessor = new RunningReportEntityAccessor(this.Context);
        runningReportAccessor.Update(runningReport);
    }
}

/* ---------------------------------------------------------------------------------- */

[TestMethod]
public void TestExportCustomers()
{
    try
    {
        string tempDir = string.Concat(@"C:\inetpub\Capstone_WS_Dev\Services\ImportExport\");
        DateTime endTime = DateTime.Now.AddDays(-20);
        ExportCustomersInput input = new ExportCustomersInput();
        input.ColumnsToHide = "";
        input.CustomerCreditGroupId = null;

        #region Input

        input.AccountStatus = 0; //(0= all, 1= Active Only, 2=Retired Only)
        input.AccountType = 0;   //(0= all, 1= Standard Only, 2=Generic Only)
        input.BranchRelationshipType = 0; //(0= No Filter, 1= SpeciGic Branch, 2=	All customers with no  branch relationship)
        input.CreditHoldStatus = 0; //(0=No Filter, 1=Always, 2=As Required 3=Never)
        input.SalesHoldStatus = 0;//(0=No Filter, 1=Always, 2=As Required 3=Never)
        input.CountryType = 0;//(0=No Filter, 1 = Other, 2 = United States, 3 = Canada)

        #endregion

        CustomerEntityAccessor accessor = new CustomerEntityAccessor(Helper.ConnectionString);

        DateTime startTime = DateTime.Now;

        var runningReportAccessor = new RunningReportEntityAccessor(Helper.ConnectionString);
        RunningReport runningReport = new RunningReport
        {
            LoginSessionId = "1667741072316.79",
            LoginUserId = 1,
            MethodName = "ExportCustomers",
            StartedUTCDateTime = DateTime.UtcNow
        };
        runningReport = runningReportAccessor.Insert(runningReport);

        accessor.ExportCustomersTask(input, tempDir, session.UserId, runningReport);

        TimeSpan timeSpent = endTime.Subtract(startTime);

    }
    catch (Exception ex)
    {
        Assert.Fail(string.Format("Error creating excel file {0}", ex.Message));
    }
}
----------
session.UserId, session.SessionId,

         