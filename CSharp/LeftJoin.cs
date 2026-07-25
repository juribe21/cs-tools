from SODBM in context.SalesOrderDetailBillOfMaterialsEntities
join SOD in context.SalesOrderDetailEntities on SODBM.SalesOrderDetailId equals SOD.SalesOrderDetailId
join SOH in context.SalesOrderHeaderEntities on SOD.SalesOrderHeaderId equals SOH.SalesOrderHeaderId

join WDR in context.WarehouseDeliveryRouteEntities on SOH.WarehouseDeliveryRouteId equals WDR.RouteId into tWDR
from WDR1 in tWDR.DefaultIfEmpty()

join cur in Context.CurrencyEntities on ven.CurrencyId equals cur.CurrencyId into curr
from cur1 in curr.DefaultIfEmpty()

join va in Context.VendorAddressEntities on ven.VendorId equals va.VendorId into vaa
from va1 in vaa.DefaultIfEmpty()

join ITM in context.ItemEntities on SODBM.ItemId equals ITM.ItemId
join CAT in context.CategoryEntities on ITM.CategoryId equals CAT.CategoryId
where SODBM.SalesOrderDetailBillOfMaterialsId == salesOrderBillOfMaterialsDetailId

IQueryable<SalesInvoiceInfoQuery> salesInvoicesQuery = (from sih in Context.SalesInvoiceHeaderEntities
                                                        join soh in Context.SalesOrderHeaderEntities on sih.SalesOrderHeaderId equals soh.SalesOrderHeaderId
                                                        join cus in Context.CustomerEntities on soh.CustomerId equals cus.CustomerId
                                                        // ↓
                                                        join cj in Context.CustomerJobEntities on soh.CustomerJobId equals cj.JobId into cusj
                                                        from cusj1 in cusj.DefaultIfEmpty
                                                            // ↓
                                                        join sr in Context.SalesRepEntities on soh.SalesRepId equals sr.RepId into tempSrPrimary
                                                        from srPrimary1 in tempSrPrimary.DefaultIfEmpty()
                                                            // ↓
                                                        join srPreparedBy in Context.SalesRepEntities on soh.PreparedBySalesRepId equals srPreparedBy.RepId into tempSrPreparedBy
                                                        from srPreparedBy1 in tempSrPreparedBy.DefaultIfEmpty()
                                                        
                                                        where sih.SalesInvoiceHeaderId == salesInvoiceHeaderId
                                                        select new SalesInvoiceInfoQuery
                                                        {
                                                            SalesInvoiceHeaderId = sih.SalesInvoiceHeaderId,
                                                            SalesOrderHeaderId = sih.SalesOrderHeaderId,
                                                            Amount = sih.TotalInvoiceAmount,
                                                            InvoiceDate = sih.InvoiceDate,
                                                            CustomerName = cus.Name,
                                                            GenericAccountFlag = cus.GenericAccountFlag,
                                                            BillingAddressName = soh.BillingAddressName,
                                                            OrderDate = soh.OrderDate,
                                                            CustomerPONumber = soh.CustomerPurchaseOrderNumber,
                                                            CustomerJobId = cusj1.JobId,
                                                            CJJobName = cusj1.Name,
                                                            JobName = soh.JobName,
                                                        });

//join sre in Context.SalesRepEntities on soh.SalesRepId equals sre.RepId into sre1
//from srep in sre1.DefaultIfEmpty()