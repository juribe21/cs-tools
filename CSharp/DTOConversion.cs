
/// List of records
IList<BankAccountStatementEntity> bankAccountStatementEntities = this.OrderBy(BankAccountStatement.SortByColumn);

List<BankAccountStatement> list = new List<BankAccountStatement>();
foreach (BankAccountStatementEntity ba in bankAccountStatementEntities)
{
    BankAccountStatement obj = DTOConversion.ConvertTo<BankAccountStatement>(ba);
    list.Add(obj);
}
return list;

// *************************************************  //

/// One Record
var bankAccountResult = context.BankAccountEntities.Where(x => x.CurrencyId == currencyId).FirstOrDefault(); // Return an Entity
var obj = DTOConversion.ConvertTo<BankAccountEx>(bankAccountResult); // Convert To local object BankAccountEx