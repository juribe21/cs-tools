/* Filters */

#region Filters

if (input.WorkOrderStatus == 2)
{
    workOrderQuery = workOrderQuery.Where(x => x.PostedByLoginUserId > 0);
}
if (input.WorkOrderStatus == 3)
{
    workOrderQuery = workOrderQuery.Where(x => x.PostedByLoginUserId == null);
}
if (DueDateLow != null)
{
    workOrderQuery = workOrderQuery.Where(x => x.DueDateTime > DueDateLow);
}
if (DueDateHigh != null)
{
    workOrderQuery = workOrderQuery.Where(x => x.DueDateTime < DueDateHigh);
}
if (WarehouseWorkCenterId.HasValue && WarehouseWorkCenterId > 0)
{
    workOrderQuery = workOrderQuery.Where(x => (int)x.WarehouseWorkCenterId == WarehouseWorkCenterId);
}
if (CategoryGroupId.HasValue && CategoryGroupId > 0)
{
    workOrderQuery = workOrderQuery.Where(x => (int)x.Group1CategoryGroupId == CategoryGroupId || (int)x.Group2CategoryGroupId == CategoryGroupId
                                        || (int)x.Group3CategoryGroupId == CategoryGroupId || (int)x.Group4CategoryGroupId == CategoryGroupId || x.Group5CategoryGroupId == CategoryGroupId);
}
if (CategoryId.HasValue && CategoryId > 0)
{
    workOrderQuery = workOrderQuery.Where(x => x.CategoryId == CategoryId);
}
if (ItemId.HasValue && ItemId > 0)
{
    workOrderQuery = workOrderQuery.Where(x => x.ItemId == ItemId);
}

/* ************************************************************** */

if (DueDateLow != null)
{
    workOrderQuery = workOrderQuery.Where(x => x.DueDateTime >= DueDateLow);
}

if (DueDateHigh != null)
{
    workOrderQuery = workOrderQuery.Where(x => x.DueDateTime <= DueDateHigh);
}

if (input.CustomerId.HasValue && input.CustomerId > 0)
{
    transactionsQuery = transactionsQuery.Where(x => x.CustomerId == input.CustomerId);
}
if (input.CustomerGroupId.HasValue && input.CustomerGroupId > 0)
{
    transactionsQuery = transactionsQuery.
            Where(x => x.Group1CustomerGroupId == input.CustomerGroupId
                    || x.Group2CustomerGroupId == input.CustomerGroupId
                    || x.Group3CustomerGroupId == input.CustomerGroupId
                    || x.Group4CustomerGroupId == input.CustomerGroupId
                    || x.Group5CustomerGroupId == input.CustomerGroupId);
}

if (input.IncludeType.HasValue)
{
    if (input.IncludeType == 1)
    {
        transactionsQuery = transactionsQuery.Where(x => x.Balance != 0);
    }
    if (input.IncludeType == 2)
    {
        transactionsQuery = transactionsQuery.Where(x => x.DueDate < DateTime.Now);
    }
}

#endregion Filters

if (item.RefundMethodType == (int)RefundMethodTypeEnum.Check)
{
    vendorRefund.RefundMethod = EnumHelper.GetDescription((RefundMethodTypeEnum)item.RefundMethodType);
}
if (item.RefundMethodType == (int)RefundMethodTypeEnum.ACH)
{
    vendorRefund.RefundMethod = EnumHelper.GetDescription((RefundMethodTypeEnum)item.RefundMethodType);
}
if (item.RefundMethodType == (int)RefundMethodTypeEnum.CreditCard)
{
    vendorRefund.RefundMethod = EnumHelper.GetDescription((RefundMethodTypeEnum)item.RefundMethodType);
}
if (item.RefundMethodType == (int)RefundMethodTypeEnum.Cash)
{
    vendorRefund.RefundMethod = EnumHelper.GetDescription((RefundMethodTypeEnum)item.RefundMethodType);
}
if (item.RefundMethodType == (int)RefundMethodTypeEnum.Other)
{
    vendorRefund.RefundMethod = EnumHelper.GetDescription((RefundMethodTypeEnum)item.RefundMethodType);
}