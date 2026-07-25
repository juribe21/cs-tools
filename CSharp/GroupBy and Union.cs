//Step    3 - Get The Data
var exportGLTransactionsForPOReceiptData = (from GLT in Context.GeneralLedgerUnpostedTransactionEntities
                                            join GLA in Context.GeneralLedgerAccountEntities on GLT.GeneralLedgerAccountId equals GLA.GeneralLedgerAccountId
                                            join GLB in Context.GeneralLedgerTransactionBatchEntities on GLT.GeneralLedgerTransactionBatchId equals GLB.GeneralLedgerTransactionBatchId
                                            where GLB.Transactiontype == 2 && GLB.ReceivingHeaderId == receivingHeaderId
                                            orderby GLA.AccountNumber
                                            group GLT by new { GLA.AccountNumber, GLA.AccountName } into g
                                            select new GLTransactionForInvoiceResult
                                            {
                                                AccountNumber = g.Key.AccountNumber,
                                                AccountName = g.Key.AccountName,
                                                Amount = g.Sum(p => p.Amount)
                                            }).ToList();

/// Count and Sum
IQueryable<TagBundleOnHandQuantities>
    query = (from tag in context.TagEntities
             where tag.WarehouseId == input.WarehouseId
                    && tag.TagBundleId > 0
                    && tag.ItemStandardSizeId == input.ItemStandardSizeId
                    && tag.StatusType == 1
             group tag by new { tag.TagBundleId } into taag
             select new TagBundleOnHandQuantities
             {
                 NumberOfBundles = taag.Count(t => t.TagBundleId != null),
                 TotalPieceCount = taag.Sum(t => t.PieceCount),
                 TotalQuantityInStockedByUnitOfMeasure = taag.Sum(t => t.QuantityInStockedByUnitOfMeasure)
             });

// -----------  select()GroupBy() And Select Top 1--------------
// https://stackoverflow.com/questions/4472369/returning-a-distinct-iqueryable-with-linq
var listItemsCode = (from ic in itemsCode
                     where ic.StandardSizeId > 0
                     orderby ic.EffectiveDate // ← ← Select Top 1 Based on orderby
                     select ic).GroupBy(g => g.StandardSizeId).Select(x => x.FirstOrDefault());



// union linq: https://dotnettutorials.net/lesson/linq-union-method/

---------------------------------------------------------------------------------------------
// distict linq: https://dotnettutorials.net/lesson/linq-distinct-method/
var distinctJurisdictions = (from inv in invoices select inv.SalesJurisdictionId).Distinct();
---------------------------------------------------------------------------------------------

// Example One
public List<ActiveWarehouse> GetAllActiveWarehousesForUser(int userId)
{
    List<ActiveWarehouse> activeWarehouse = null;

    try
    {
        // Step 1 - Get the Active Warehouses Not Owned By a Branch
        var notownedwarehouses = (from w in Context.WarehouseEntities
                                  where w.RetiredFlag == false && w.OwnedByBranchId == null
                                  select new ActiveWarehouse
                                  {
                                      WarehouseId = w.WarehouseId,
                                      Name = w.Name
                                  });

        //Step 2 - Get All Warehouses Owned By Branch Where User is Associated
        var WarehousesOwned = (from wh in Context.WarehouseEntities
                               join lub in Context.LoginUserBranchXrefEntities on wh.OwnedByBranchId equals lub.BranchId
                               where wh.RetiredFlag == false && lub.LoginUserId == userId
                               select new ActiveWarehouse
                               {
                                   WarehouseId = wh.WarehouseId,
                                   Name = wh.Name
                               });

        activeWarehouse = notownedwarehouses.Union(WarehousesOwned).ToList();

        return activeWarehouse;

    }
    catch (Exception e)
    {
        throw new CapstoneException(BusinessLogicException.GetAllFailed, "Get All Active Warehouses for user, failed.");
    }
}

// Example Two
public List<ActiveWarehouse> GetAllActiveWarehousesForUser(int userId)
{
    #region Step 2 - Get the Output Detail Records Where Tags Will Be Created
    var workOrderOutputDetailType6 = listWorkOrderOutputDetails.Where(x => x.OutputType != 6).ToList();

    var totalNumberOfOutputDetails = workOrderOutputDetailType6 != null ? workOrderOutputDetailType6.Count : 0;
    var loopCtr = 0;

    // **** Union *****
    var tagIdList = workOrderOutputDetailType6.Select(x => x.TagId).ToList().Union(workOrderOutputDetailType6.Select(x => x.ParentTagId).ToList()).ToList();
    var tagList = context.TagEntities.Where(x => tagIdList.Contains(x.TagId)).ToList();


    var workOrderOutputDetailType6Ids = workOrderOutputDetailType6.Select(x => x.WorkOrderOutputDetailId).ToList();
    var workOrderOutputDetailType6ParentTagsIds = workOrderOutputDetailType6.Select(x => x.ParentTagId ?? 0).ToList();
    var tagAttachmentsForParentTags = tagAttachmentEntityAccessor.GetAllForTag(workOrderOutputDetailType6ParentTagsIds, null);
}

---------------------

// **** Union Group Union *****
/// // Example 3 -	Get	Records	For	Invoices
var invoices = (from sta in context.SalesInvoiceDetailSalesTaxAmountEntities
                join sih in context.SalesInvoiceHeaderEntities on sta.SalesInvoiceHeaderId equals sih.SalesInvoiceHeaderId
                join stj in context.SalesTaxJurisdictionEntities on sta.SalesTaxJurisdictionId equals stj.SalesTaxJurisdictionId
                where sih.InvoiceDate >= lowInvoiceDate && sih.InvoiceDate <= highInvoiceDate && (stj.SalesTaxJurisdictionId > 0 || sta.SalesTaxJurisdictionId == salesTaxJurisdictionId)
                group sta by new { sta.SalesTaxJurisdictionId, stj.Name, sta.SalesTaxCustomerResaleCertificateId, sta.SalesTaxJurisdictionExemptionCertificateId } into g
                select new SalesTaxRecords
                {
                    SalesJurisdictionId = g.Key.SalesTaxJurisdictionId,
                    JurisdictionName = g.Key.Name,
                    TotalInvoiceAmount = g.Sum(p => p.ExtendedPrice),
                    TotalTaxAmount = g.Sum(p => p.Amount),
                    SalesTaxCustomerResaleCertificateId = g.Key.SalesTaxCustomerResaleCertificateId,
                    SalesTaxJurisdictionExemptionCertificateId = g.Key.SalesTaxJurisdictionExemptionCertificateId
                });





/// Get Records For Customer Credit Memos
var customerCredit = (from cta in context.CustomerCreditDetailSalesTaxAmountEntities
                      join cch in context.CustomerCreditHeaderEntities on cta.CustomerCreditHeaderId equals cch.CustomerCreditHeaderId
                      join stj in context.SalesTaxJurisdictionEntities on cta.SalesTaxJurisdictionId equals stj.SalesTaxJurisdictionId
                      where cch.TransactionDate >= lowInvoiceDate && cch.TransactionDate <= highInvoiceDate && (stj.SalesTaxJurisdictionId > 0 || cta.SalesTaxJurisdictionId == salesTaxJurisdictionId)
                      group cta by new { cta.SalesTaxJurisdictionId, stj.Name, cta.SalesTaxCustomerResaleCertificateId, cta.SalesTaxJurisdictionExemptionCertificateId } into g
                      select new SalesTaxRecords
                      {
                          SalesJurisdictionId = g.Key.SalesTaxJurisdictionId,
                          JurisdictionName = g.Key.Name,
                          TotalInvoiceAmount = g.Sum(p => p.ExtendedPrice),
                          TotalTaxAmount = g.Sum(p => p.Amount),
                          SalesTaxCustomerResaleCertificateId = g.Key.SalesTaxCustomerResaleCertificateId,
                          SalesTaxJurisdictionExemptionCertificateId = g.Key.SalesTaxJurisdictionExemptionCertificateId
                      });

/// Union lists
salesTaxSummary = invoices.Union(customerCredit).ToList();


// **** GroupBy and Sum *****
List<TagCostsSalesInvoiceDetailId> tagsList =
        (from t in context.TagEntities.ToList()
         join il in exportSalesInvoiceLinesList.Select(x => new { SalesInvioceDetailId = x.SalesInvoiceDetailId }).ToList()
             on t.ShippedUnderSalesInvoiceDetailId equals il.SalesInvioceDetailId
         select t)
        .GroupBy(x => x.ShippedUnderSalesInvoiceDetailId)
        .Select(x => new TagCostsSalesInvoiceDetailId
        {
            SalesInvoiceHeaderId = x.Max(y => y.ShippedUnderSalesInvoiceDetailId),
            TotalMaterialCost = x.Sum(y => y.TotalMaterialCost),
            TotalInboundFreightCost = x.Sum(y => y.TotalInboundFreightCost),
            TotalCost3 = x.Sum(y => y.TotalCost3),
            TotalCost4 = x.Sum(y => y.TotalCost4),
            TotalCost5 = x.Sum(y => y.TotalCost5),
            TotalCost6 = x.Sum(y => y.TotalCost6),
            TotalCost7 = x.Sum(y => y.TotalCost7),
            TotalCost8 = x.Sum(y => y.TotalCost8),
            TotalCost9 = x.Sum(y => y.TotalCost9),
            TotalCost10 = x.Sum(y => y.TotalCost10)
        }).ToList();


/// ************************** string.Join ************************************
var tagIdList =
tagEntityAccessor.GetTagIdsByShippedUnderSalesInvoiceDetailId(dtl.SalesInvoiceDetailId);

/// TODO: CHECK HERE NEW CHANGES
if (tagIdList.Any())
{
    var tagIds = string.Join(",", tagIdList);
    context.ExecuteQuery<string>($"UPDATE Tag Set Status_Type = 28 WHERE Tag_Id in ({tagIds});");
}



/// **** DTOConversion
var activeWarehouses = notownedwarehouses.Union(WarehousesOwned).ToList();

foreach (WarehouseEntity warahouse in activeWarehouses)
{
    activeWarehouse.Add(DTOConversion.ConvertTo<Warehouse>(warahouse));
}