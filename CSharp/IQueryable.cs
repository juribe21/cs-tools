

#region GetData [ExportSalesQuoteLines]

IQueryable<SalesQuoteOrderDetail> salesQuoteDetails = (from SQD in context.SalesQuoteDetailEntities
                                                       join SQH in context.SalesQuoteHeaderEntities on SQD.SalesQuoteHeaderId equals SQH.SalesQuoteHeaderId
                                                       join CUS in context.CustomerEntities on SQH.CustomerId equals CUS.CustomerId
                                                       join ITM in context.ItemEntities on SQD.ItemId equals ITM.ItemId
                                                       join CAT in context.CategoryEntities on ITM.CategoryId equals CAT.CategoryId
                                                       orderby SQH.QuoteDate, CUS.CustomerCode, SQH.SalesQuoteHeaderId, SQD.LineNumber
                                                       select new SalesQuoteOrderDetail
                                                       {
                                                           SalesQuoteHeaderId = SQH.SalesQuoteHeaderId,
                                                           QuoteDate = SQH.QuoteDate,
                                                           ExpirationDate = SQH.ExpirationDate,
                                                           AnticipatedReadyDate = SQH.AnticipatedReadyDate,
                                                           ConvertedToOrderDateTime = SQH.ConvertedToOrderDateTime,
                                                           BillingAddressName = SQH.BillingAddressName,
                                                           SalesRepId = SQH.SalesRepId,
                                                           DeliveryAddressType = SQH.DeliveryAddressType,
                                                           BranchId = SQH.BranchId,
                                                           CustomerId = SQH.CustomerId,
                                                           CnalAndExternalNote,
                                                           SQH_NonPrintingNote = SQD.NonPrintingNote,
                                                           CustomerCode = CUS.CustomerCode,
                                                           Name = CUS.Name,
                                                           GenericAccountFlag = CUS.GenericAccountFlag,
                                                           ItemId = ITM.ItemId,
                                                           ItemCode = ITM.ItemCode,
                                                           BriefDescription = ITM.BriefDescription,
                                                           SortSequence = ITM.SortSequence,
                                                           ITM_Description = ITM.Description,
                                                           ITM_Description2 = ITM.Description2,
                                                           ITM_BriefDescription = ITM.BriefDescription,
                                                           CategoryId = CAT.CategoryId,
                                                           CAT_SortSequence = CAT.SortSequence,
                                                           CAT_Description = CAT.Description

                                                       });

if (!salesQuoteDetails.Any())
{
    return null;
}

#endregion GetData

#region Filters Example
if (input.IncludeExpiredQuotesFlag == false)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.ExpirationDate >= DateTime.Now);
if (input.IncludeType == 2)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.ConvertedToOrderDateTime != null);
if (input.IncludeType == 3)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.SalesQuoteHeaderId == input.SalesQuoteHeaderId);
if (input.BranchId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.BranchId == input.BranchId);
if (input.WarehouseId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.WarehouseId == input.WarehouseId);
if (input.PrimarySalesRepId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.SalesRepId == input.PrimarySalesRepId);
if (input.CreatedLoginUserId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.CreatedLoginUserId == input.CreatedLoginUserId);
if (input.PreparedBySalesRepId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.PreparedBySalesRepId == input.PreparedBySalesRepId);
if (input.QuoteDateLow != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.QuoteDate == input.QuoteDateLow);
if (input.QuoteDateHigh != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.QuoteDate == input.QuoteDateHigh);

if (input.AnticipatedDateLow != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.AnticipatedDateLow == input.AnticipatedDateLow);
if (input.AnticipatedDateHigh != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.AnticipatedDateHigh == input.AnticipatedDateHigh);

if (input.CustomerId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.CustomerId == input.CustomerId);
if (input.ItemId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.ItemId == input.ItemId);
if (input.CategoryId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.CategoryId == input.CategoryId);
if (input.CategoryGroupId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.Group1CategoryGroupId == input.CategoryGroupId || x.Group2CategoryGroupId == input.CategoryGroupId || x.Group3CategoryGroupId == input.CategoryGroupId
    || x.Group4CategoryGroupId == input.CategoryGroupId || x.Group5CategoryGroupId == input.CategoryGroupId);
if (input.CustomerGroupId != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.Group1CustomerGroupId == input.CustomerGroupId || x.Group2CustomerGroupId == input.CustomerGroupId || x.Group3CustomerGroupId == input.CustomerGroupId
    || x.Group4CustomerGroupId == input.CustomerGroupId || x.Group5CustomerGroupId == input.CustomerGroupId);
if (input.UDF1 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF1 == input.UDF1);
if (input.UDF2 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF2 == input.UDF2);
if (input.UDF3 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF3 == input.UDF3);
if (input.UDF4 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF4 == input.UDF4);
if (input.UDF5 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF5 == input.UDF5);
if (input.UDF6 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF6 == input.UDF6);
if (input.UDF7 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF7 == input.UDF7);
if (input.UDF8 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF8 == input.UDF8);
if (input.UDF9 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF9 == input.UDF9);
if (input.UDF10 != null)
    salesQuoteDetails = salesQuoteDetails.Where(x => x.UDF10 == input.UDF10);

#endregion Filters Example

int? dimensionType = Context.WarehouseEntities.FirstOrDefault(x => x.WarehouseId == input.WarehouseId).DimensionsType;

#region Step 3

if (salesQuoteDetails.Count() > 0)
{
    bool WholeNumbersOnlyFlag = false;

    foreach (var record in salesQuoteDetails)
    {
    }
}


