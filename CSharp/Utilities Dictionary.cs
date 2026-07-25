/// namespace Bayern.CapstoneService.DAL.Dao.Utilities

/// Ticket reference #13390

/// CustomerPaymentEntityAccessor
PaymentMethod += Utilities.PaymentType[Convert.ToInt32(input.PaymentType)];



#region PaymentType
if (input.PaymentType.HasValue && input.PaymentType > 0)
{
    string PaymentMethod = "Payment Type: ";
    if (Utilities.PaymentType.ContainsKey(input.PaymentType ?? 0))
    {
        /// Reference to statica class
        PaymentMethod += Utilities.PaymentType[Convert.ToInt32(input.PaymentType)];
    }

    var rowPaymentMethod = new HeaderRowDefinition();
    var reportPaymentMethod = new HeaderCellDefinition
    {
        CellIndex = 0,
        CellValue = PaymentMethod,
        TextAlignment = CellTextHorizontalAlignment.Left
    };
    rowPaymentMethod.RowIndex = rowIndex++;
    rowPaymentMethod.RowCells.Add(reportPaymentMethod);
    exportCustomerPaymentsResponseModel.HeaderData.Rows.Add(rowPaymentMethod);
}
#endregion PaymentType

public static class Utilities
{
    public static readonly Dictionary<int, string> PaymentType = new Dictionary<int, string>()
    {
        { 1,"Cash"},
        { 2,"Credit Card"},
        { 3,"Debit Card"},
        { 4,"Check"},
        { 5,"ACH"},
        { 6,"eCheck"},
        { 7,"Other"},
        { 8,"Business Check"},
        { 9,"Account Credit"},
    };
}

/*  *    *   *   *   *   TagStatusType  *   *   *   *   *   *   *   *   */
item.StatusDescription = Utilities.GetTagStatusTypeText(item.StatusType ?? 0);
// Call to GetTagStatusTypeText method in Utilities class
// Call to GetAvailableTagStatusTypes method that contains a Dictionary that contains TagStatusTypes
// TagStatusTypes exist on HelperConstants.TagStatusType