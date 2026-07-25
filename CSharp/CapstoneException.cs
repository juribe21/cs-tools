public void ExceptionList()
{
    try
    {

    }
    catch (CapstoneException ex)
    {
        if (ex.Code == BusinessLogicException.RecordLockedByUser)
        {
            //log_.Info("** Record locked by other user.");
            string sMessage = ex.Message;
            string[] messageList = sMessage.Split(',');
            throw SoapExceptionHelper.ToSoapRecordLockException(SoapExceptionHelper.GenericExceptionId,
                BusinessLogicException.RecordLockedByUser, messageList.Length > 0 ? messageList[0] : "",
                messageList.Length > 1 ? messageList[1] : "",
                messageList.Length > 2 ? messageList[2] : "");
        }
        Assert.Fail(ex.Message);
    }
}


/*
Reference - 3880 - 3914
EditSalesOrderHeader * SalesOrderService
GetLastBankStatement - BankService
*/
[service]
public LastBankStatement GetLastBankStatement(SoapUserSession session, int bankAccountId)
{
    try
    {

    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetLastBankStatement.ToString());
        }
        /*             BusinessLogicException.RecordLockedByUser              */
        if (ex.Code == BusinessLogicException.RecordLockedByUser)
        {
            string sMessage = ex.Message;
            string[] messageList = sMessage.Split(',');
            throw SoapExceptionHelper.ToSoapRecordLockException(SoapExceptionHelper.GenericExceptionId,
                BusinessLogicException.RecordLockedByUser,
                messageList.Length > 0 ? messageList[0] : "",
                messageList.Length > 1 ? messageList[1] : "",
                messageList.Length > 2 ? messageList[2] : "");
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
}


public BankAccountBalanceWithLock GetBankAccountBalanceWithLock(BankAccountBalanceWithLockInput input, string sessionId, int userId)
{
    /// Step 2 - Attempt to Put a Lock on the BankAccount
    RecordLockEntityAccessor rlAccessor = new RecordLockEntityAccessor(this.ConnectionString);
    RecordLockExtn recordLock = rlAccessor.GetRecordLockDetail(BankAccount.TableName, input.BankAccountId);

    if (recordLock == null)
    {
        rlAccessor.LockRecord(sessionId, BankAccount.TableName, input.BankAccountId, userId);
    }
    if (recordLock != null && sessionId != recordLock.SessionId)
    {
        string sLockedSince = "";
        if (recordLock.LockDateTime.HasValue)
        {
            sLockedSince = DateTimeHelper.ToFormattedDateTime(recordLock.LockDateTime.Value);
        }
        errorMessage = string.Format("Record is locked by other user." + "," + recordLock.LockByName + "," + sLockedSince);
        /*                          BusinessLogicException.RecordLockedByUser                   */
        throw new CapstoneException(BusinessLogicException.RecordLockedByUser, errorMessage);
    }

}