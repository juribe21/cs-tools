/*  Joins in LINQ on multiple fields in single join */
public AssociationsForVendorPayment GetAssociationsForVendorPayment(int vendorPaymentId)
{
    PaymentAssociations paymentAssociations = null;
    //var transactionType = 3;

    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {
        ///Step 1 - Get the Base Currency
        var baseCurrencyId = context.MainInitEntities.FirstOrDefault()?.BaseCurrencyId;

        /// Step 2 -Get The VendorPayment Record and VendorTransaction 
        var vptr = (from vp in context.VendorPaymentEntities
                    join ven in context.VendorEntities on vp.VendorId equals ven.VendorId
                    join cur in context.CurrencyEntities on ven.CurrencyId equals cur.CurrencyId into cur1
                    from cure in cur1.DefaultIfEmpty()
                    join vpvt in context.VendorTransactionEntities
                    on new { vendorPaymentId = vp.VendorPaymentId, transactionType = (short)3 }
                    equals new { vendorPaymentId = (int)vpvt.VendorPaymentId, transactionType = vpvt.TransactionType }
                    where vp.VendorPaymentId == vendorPaymentId
                    select new VendorPaymentRecordQuery
                    {
                        PaymentDate = vp.PaymentDate,
                        PaymentMethodType = vp.PaymentMethodType,
                        CheckNumber = vp.CheckNumber,
                        ReferenceNumber = vp.ReferenceNumber,
                        Amount = vp.Amount,
                        Description = vp.Description,
                        VendorTransactionId = vpvt.VendorTransactionId,
                        BalanceDue = vpvt.Balance,
                        CurrencyId = ven.CurrencyId,
                        Currency = cure.Description,
                        VendorId = vpvt.VendorId,
                    }).FirstOrDefault();
    }
}