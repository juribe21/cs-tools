#region VendorRefund

[TestMethod]
public void TestGetAllVendorRefunds()
{
    try
    {
        VendorRefundEntityAccessor accessor = new VendorRefundEntityAccessor(Helper.ConnectionString);
        var obj = accessor.GetAll();
    }
    catch (Exception ex)
    {
        Assert.Fail(ex.Message);
    }
}

[TestMethod]
public void TestGetVendorRefund()
{
    try
    {
        int creditCardPaymentId = 1;

        VendorRefundEntityAccessor accessor = new VendorRefundEntityAccessor(Helper.ConnectionString);
        var obj = accessor.GetById(creditCardPaymentId);
    }
    catch (Exception ex)
    {
        Assert.Fail(ex.Message);
    }
}

[TestMethod]
public void TestInsertVendorRefund()
{
    try
    {
        VendorRefund creditCardPayment = new VendorRefund
        {
            VoidedFlag = false,
            VoidedByLoginUserId = 1,
            VoidedUTCDateTime = DateTime.Now,
            PaymentMethodType = 1,
            PaymentDate = DateTime.Now,
            CreditCardAccountId = 1,
            BankAccountId = 11,
            CheckNumber = 2,
            ReferenceNumber = "RefNumberTest2",
            PaidByCreditCardAccountId = 1,
            Amount = 11.5M,
            ExchangeRate = 1.00012M
        };

        VendorRefundEntityAccessor accessor = new VendorRefundEntityAccessor(Helper.ConnectionString);
        var obj = accessor.Insert(creditCardPayment);
    }
    catch (Exception ex)
    {
        Assert.Fail(ex.Message);
    }
}

[TestMethod]
public void TestUpdateVendorRefund()
{
    try
    {
        VendorRefund creditCardPayment = new VendorRefund
        {
            VendorRefundId = 2,
            VoidedFlag = false,
            VoidedByLoginUserId = 1,
            VoidedUTCDateTime = DateTime.Now.AddDays(3),
            PaymentMethodType = 1,
            PaymentDate = DateTime.Now.AddDays(5),
            CreditCardAccountId = 1,
            BankAccountId = 11,
            CheckNumber = 2,
            ReferenceNumber = "RefNumberTest2a",
            PaidByCreditCardAccountId = 1,
            Amount = 11.53M,
            ExchangeRate = 1.05012M
        };

        VendorRefundEntityAccessor accessor = new VendorRefundEntityAccessor(Helper.ConnectionString);
        var obj = accessor.Update(creditCardPayment);
    }
    catch (Exception ex)
    {
        Assert.Fail(ex.Message);
    }
}

[TestMethod]
public void TestDeleteVendorRefund()
{
    try
    {
        int creditCardPaymentId = 1;

        VendorRefundEntityAccessor accessor = new VendorRefundEntityAccessor(Helper.ConnectionString);
        var obj = accessor.Delete(creditCardPaymentId);
    }
    catch (Exception ex)
    {
        Assert.Fail(ex.Message);
    }
}


#endregion VendorRefund