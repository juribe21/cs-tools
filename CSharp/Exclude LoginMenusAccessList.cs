
#region Private Methods

private void GetListContains()
{
    var glTransactionBatch = context.GeneralLedgerTransactionBatchEntities
            .Where(x => x.CreditCardTransactionId == creditCardTransactionId)
            .ToList();

    List<int> glTransactionBatchIds = glTransactionBatch.Select(s => s.GeneralLedgerTransactionBatchId).ToList();

    List<GeneralLedgerTransactionDetailEntity> gltDetails = context.GeneralLedgerTransactionDetailEntities
    .Where(x => glTransactionBatchIds.Contains(x.GeneralLedgerTransactionBatchId)).ToList();
}

private List<LoginMenuAccessEntity> GetLoginMenusAccessList(List<LoginMenuAccessEntity> entities)
{
    var accountIntegrationTypes = new List<int?>() { 7, 8 };
    List<int> excludedProgramIds =
        new List<int>() { 1115, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113, 1114, 5140, 5141, 5143, 5144, 5145, 5146, 5147, 5148, 5149, 5150, 5151, 5152, 5153, 1027, 1100, 3016 };

    ThirdPartyAccountingInitEntityAccessor inventoryInitAccessor = new ThirdPartyAccountingInitEntityAccessor(this.ConnectionString);
    ThirdPartyAccountingInitSettings inventoryInit = inventoryInitAccessor.GetThirdPartyAccountingInit();
    int? integrationType = inventoryInit.AccountingIntegrationType;

    List<LoginMenuAccessEntity> resultList = new List<LoginMenuAccessEntity>();

    if (!accountIntegrationTypes.Contains(integrationType))
    {
        if (integrationType == 0)
        {
            excludedProgramIds.Remove(1027);
        }
        // do not returned records where the Program_Id in the excludedProgramIds list
        resultList = entities.Where(i => !excludedProgramIds.Any(e => i.ProgramId == e)).ToList(); // ← ← ← 
    }
    else
    {
        resultList = entities;
    }

    return resultList;
}

#endregion Private Methods

public List<LoginMenuAccess> GetAllUserMenuAccessForUserOrGroupAndLoginType(int userOrGroupId, int loginType)
{
    List<LoginMenuAccessEntity> resultList = null;
    List<LoginMenuAccessEntity> entities = this.Where(e => e.UserOrGroupId == userOrGroupId && e.LoginType == loginType).ToList();
    resultList = GetLoginMenusAccessList(entities);

    List<LoginMenuAccess> list = new List<LoginMenuAccess>();
    foreach (LoginMenuAccessEntity l in resultList)
    {
        LoginMenuAccess obj = DTOConversion.ConvertTo<LoginMenuAccess>(l);
        list.Add(obj);
    }

    return list;
}