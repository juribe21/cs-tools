/* RecordLock */

/// Create instance of RecordLockEntityAccessor
RecordLockEntityAccessor rlAccessor = new RecordLockEntityAccessor(this.ConnectionString);

/// Get RecordLock Detail
RecordLockExtn recordLock = rlAccessor.GetRecordLockDetail(CustomerCreditDetail.TableName, entity.CustomerMiscellaneousCreditId);

/// Send to LockRecord
RecordLockExtn recordLock = rlAccessor.LockRecord(sessionId, CustomerCreditDetail.TableName, customerCreditDetailId, userId);


/// unlock record
rlAccessor.UnLockRecord(CustomerMiscellaneousCredit.TableName, entity.CustomerMiscellaneousCreditId);

/// Search: rlAccessor.LockRecord

/// If record is lock - return message
if (recordLock != null)
{
    string sLockedSince = "";
    if (recordLock.LockDateTime.HasValue)
    {
        sLockedSince = DateTimeHelper.ToFormattedDateTime(recordLock.LockDateTime.Value);
    }
    throw new CapstoneException(BusinessLogicException.RecordLockedByUser,
            "Record is locked by other user." + "," + recordLock.LockByName + "," + sLockedSince);
}


//// Check If record is lock else return message
/// Step X - Attempt to put a Lock on the BankAccount
RecordLockEntityAccessor rlAccessor = new RecordLockEntityAccessor(context);
RecordLockExtn recordLock = rlAccessor.GetRecordLockDetail(BankAccount.TableName, bankAccountId);

if (recordLock == null)
{
    rlAccessor.LockRecord(sessionId, BankAccountt.TableName, bankAccountIdd, userId);
}
if (recordLock != null && sessionId != recordLock.SessionId)
{
    string sLockedSince = "";
    if (recordLock.LockDateTime.HasValue)
    {
        sLockedSince = DateTimeHelper.ToFormattedDateTime(recordLock.LockDateTime.Value);
    }
    errorMessage = string.Format("Record is locked by other user." + "," + recordLock.LockByName + "," + sLockedSince);
    throw new CapstoneException(BusinessLogicException.GetBankAccountBalanceWithLock, errorMessage);
}