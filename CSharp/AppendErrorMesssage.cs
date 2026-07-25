// ----AppendPostErrorMessage-- -
// Step	3.1	- Check the Commissions Paid Through Date
// -- Add
string errorMessage = string.Empty;
var commissionsPaidThroughDate = (from br in context.BranchEntities where br.BranchId == salesInvoiceHeaderToPost.BranchId select br.CommissionsPaidThroughDate).FirstOrDefault();
if (commissionsPaidThroughDate.HasValue && commissionsPaidThroughDate.Value != default(DateTime) && commissionsPaidThroughDate >= salesInvoiceHeaderToPost.InvoiceDate)
{
    AppendPostErrorMessage("Commissions have been paid through the invoice date.", ref errorMessage);
}

private void AppendPostErrorMessage(string message, ref string result)
{
    if (result != null)
    {
        result = (!string.IsNullOrEmpty(result))
            ? result + message + Environment.NewLine
            : message + Environment.NewLine;
    }
}

StringBuilder sb = new StringBuilder();
sb.AppendLine("The specified date does not fall into a valid accounting period.");
sb.AppendLine("CheckDateForOpenGeneralLedgerAccountingPeriod returned error: " + ex.Message);
string errorMessage = sb.ToString();