public void Pagination(int input.PageNumber)
{

    int _pageNumber = 1;
    if (input.PageNumber > 0)
        _pageNumber = input.PageNumber;
    int countRecord = 1;

    var spResult = contex.SpCall();
    /// var spResult = (from some Query);

    foreach (var item in spResult)
    {
        GeneralLedgerAccountInquiryList accountInquiryList = new GeneralLedgerAccountInquiryList();

        accountInquiryList.TransactionDate = item.TransactionDate;
        accountInquiryList.GeneralLedgerTransactionBatchId = item.GeneralLedgerTransactionBatchId;
        accountInquiryList.BatchType = item.TransactionType;
        accountInquiryList.DocumentNumber = item.DocumentNumber;
        accountInquiryList.Description = item.Description;
        accountInquiryList.Amount = item.Amount;

        accountInquiryList.RecordId = countRecord;
        countRecord++;

        ledgerAccountInquiry.generalLedgerAccounts.Add(accountInquiryList);
    }

    var endId = input.PageNumber * 50;
    var startId = ((input.PageNumber - 1) * 50) + 1;

    ledgerAccountInquiry.generalLedgerAccounts.Where(x => x.RecordId >= startId && x.RecordId <= endId).ToList();
}


/// Add *RecordId* property on object to be listed

ThrowIfNull(typeof(int), workOrderHeaderId, "workOrderHeaderId");
ThrowIfNull(typeof(DateTime), gLTransactionDate, "gLTransactionDate");