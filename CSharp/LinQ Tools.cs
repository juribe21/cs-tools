/// SUM Validation
result.CurrentAmount = (from vt in Context.VendorTransactionEntities
                        where vt.VendorId == vendorId && vt.Balance != 0
                        && (vt.DueDate >= DateTime.Now || vt.DueDate.HasValue == false || vt.DueDate == default(DateTime))
                        select (decimal?)vt.Balance).Sum() ?? 0;

// string Validation
generalLedgerTransactionBatch.VendorName = (from vp in context.VendorPaymentEntities
                                            join ven in context.VendorEntities on vp.VendorId equals ven.VendorId
                                            where vp.VendorPaymentId == (int)generalLedgerTransactionBatch.VendorPaymentId
                                            select ven.VendorName).FirstOrDefault() ?? string.Empty;

// MidpointRounding.AwayFromZero
decimal discountAmount = Math.Round((decimal)salesinvoiceLine.ExtendedPrice * ((decimal)salesinvoiceLine.DiscountPercentage / 100), 2, MidpointRounding.AwayFromZero);

/// → *** → *** → *** → *** → *** → *** → *** → *** → *** → *** → *** → *** 
// Calculate difference between two dates (number of days)?
var days = (EndDate - StartDate).TotalDays;

// 2nd
if (paymentDate != null)
{
    var daysToPay = (item.TransactionDate - paymentDate.PaymentDate).TotalDays;
    totalAmountMultipliedByDaysToPay += (item.Amount * (int)daysToPay);
    totalAmount += item.Amount;
}

/* ********************************* FIND ELEMENTS AMONG TWO LISTS ********************************* */
List<Materia> materias = Contexto.Materias.Where(x => foliosMaterias.Any(m => m.MateriaId == x.MateriaId)).ToList();

// Exclude elements from list - two lists
var results = dataset.Where(i => !excluded.Any(e => i.Contains(e))); // check Contains is not working

// Exclude elements from list - two lists
var results = List.Except(excludedProgramIds); // OK

// Exclude elements from list - from db list with columns[entities], 
// compare property ProgramiId with element of excluded list
var results = entities.Where(i => !excludedProgramIds.Any(e => i.ProgramId == e)); // Work OK
/// → *** → *** → *** → *** → *** → *** → *** → *** → *** → *** → *** → *** 

/// Retrieve LINQ to sql statement (IQueryable) WITH parameters
var query = accountBalance.ToString();

/// FIND ID IN OBJECT CONTEXT
var undepositedFundids = undepositedTransactions.Select(s => s.UndepositedFundsId).ToList();
var undepositedFunds = context.UndepositedFundsEntities.Where(x => undepositedFundids.Contains(x.UndepositedFundsId)).ToList();

// Left Join with same Table - Context.SalesRepEntities
IQueryable<OpenSalesOrderList>
    openSalesOrderList = (from soh in Context.SalesOrderHeaderEntities
                          join cus in Context.CustomerEntities on soh.CustomerId equals cus.CustomerId
                          join wdr in Context.WarehouseDeliveryRouteEntities on soh.WarehouseDeliveryRouteId equals wdr.RouteId
                          // ↓
                          join sr in Context.SalesRepEntities on soh.SalesRepId equals sr.RepId into tempSrPrimary
                          from srPrimary1 in tempSrPrimary.DefaultIfEmpty()
                              // ↓
                          join srPreparedBy in Context.SalesRepEntities on soh.PreparedBySalesRepId equals srPreparedBy.RepId into tempSrPreparedBy
                          from srPreparedBy1 in tempSrPreparedBy.DefaultIfEmpty()

                          where soh.WarehouseId == warehouseId && soh.ClosedFlag == false && soh.WarehouseDeliveryRouteId > 0
                          orderby wdr.Name, soh.SalesOrderHeaderId
                          select new OpenSalesOrderList
                          {
                              TrackDeliveryOrShipOrPickUpDateTimeLineByLineFlag = soh.TrackDeliveryOrShipOrPickUpDateTimeLineByLineFlag,
                              RepId = srPrimary1 != null ? srPrimary1.RepId : 0,
                              SalesRepId = soh.SalesRepId,
                              PreparedBySalesRepId = soh.PreparedBySalesRepId,
                              SalesRepName = srPrimary1 != null ? srPrimary1.Name : string.Empty,
                              PrimaryRep = srPrimary1 != null ? srPrimary1.Name : string.Empty,
                              PreparedByRep = srPreparedBy1 != null ? srPreparedBy1.Name : string.Empty,
                          });

// Check for default DateTime value
if (dateTime == default(DateTime))
    //
    if (nullableDateTime.HasValue)
    {
    }

// Null validation
string vendorGroupDescription = context.VendorGroupEntities.Where(x => x.GroupId == input.VendorGroupId).FirstOrDefault()?.Description;

///*** 
private int GetNextGapCreditNumber(List<int> miscellaneousCreditIds)
{
    var gapNextCreditNumber = Enumerable.Range(miscellaneousCreditIds.Min(),
        miscellaneousCreditIds.Count).Except(miscellaneousCreditIds).FirstOrDefault();
    return gapNextCreditNumber;
}

/// Math.Round
Math.Round(((totalWeight / scaleWtCalcPiecesWeighed) * tagPieceCount), 3, MidpointRounding.AwayFromZero);
Math.Round(tempExtendedUnitCost / quantityInPieces, 4, MidpointRounding.AwayFromZero);
Math.Round(unitCost * quantityInPieces, 2, MidpointRounding.AwayFromZero);

// ***  return multiple fields with lambda
public NamePriceModel[] AllProducts()
{
    try
    {
        using (UserDataDataContext db = new UserDataDataContext())
        {
            return db.mrobProducts
                .Where(x => x.Status == 1)
                .Select(x => new NamePriceModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price
                })
                .OrderBy(x => x.Id)
                .ToArray();
        }
    }
    catch
    {
        return null;
    }
}
