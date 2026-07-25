/* How to get a list of Id's and update in code */

foreach (var dtl in listSalesInvoiceDetail)
{
    var tagEntityAccessor = new TagEntityAccessor(context);
    var tagIdList =
        tagEntityAccessor.GetTagIdsByShippedUnderSalesInvoiceDetailId(dtl.SalesInvoiceDetailId);

    /// #12495 add new updates
    if (tagIdList.Any())
    {
        var tagIds = string.Join(",", tagIdList);

        if (newStatusType == 5)
        {
            context.ExecuteQuery<string>($"UPDATE Tag Set Status_Type = 28 WHERE Tag_Id in ({tagIds});");
        }
        else if (newStatusType == 4)
        {
            context.ExecuteQuery<string>($"UPDATE Tag Set Status_Type = 17 WHERE Tag_Id in ({tagIds});");
        }
        else
        {
            context.ExecuteQuery<string>($"UPDATE Tag Set Status_Type = 5 WHERE Tag_Id in ({tagIds});");
        }
    }
}