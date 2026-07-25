public class ExportCreditCardTransactionsDataResponseModel
{
    public int[] ColumnsToHide { get; set; }
    public int TotalColumns { get; set; }
    public int SheetNumber { get; set; }
    public int Currencies { get; set; }
    public ExportCreditCardTransactionsHeaderDataResponseModel HeaderData { get; private set; }
    public ExportCreditCardTransactionsDetailDataResponseModel DetailData { get; private set; }

    public ExportCreditCardTransactionsDataResponseModel()
    {
        HeaderData = new ExportCreditCardTransactionsHeaderDataResponseModel();
        DetailData = new ExportCreditCardTransactionsDetailDataResponseModel();
    }
}

/*
var workbook =
    (new ExportCustomerMiscellaneousCreditsLogic()).ExportToExcelSheetProtect(
        exportCustomerDebitsDataResponseModel.TotalColumns,
    → → ImportExportConstants.SheetName + exportCustomerDebitsDataResponseModel.SheetNumber.ToString(),  ← ←
        exportCustomerDebitsDataResponseModel, false);

string filePath = string.Concat(@"C:\inetpub\Capstone_WS_Dev\Services\ImportExport\");
*/

public class ExportBankTransactionsHeaderResponseModel
{
    public List<HeaderRowDefinition> Rows { get; private set; }
    public ExportBankTransactionsHeaderResponseModel()
    {
        Rows = new List<HeaderRowDefinition>();
    }
}

public class ExportBranchCategoryGeneralLedgerAccountsDetailDataResponseMode
{
    public List<BranchCategoryXrefExport> DetailRows { get; private set; }

    public ExportBranchCategoryGeneralLedgerAccountsDetailDataResponseMode()
    {
        DetailRows = new List<BranchCategoryXrefExport>();
    }
}