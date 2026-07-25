insertSql = string.Empty;
insertSql = String.Format($"INSERT INTO TAG (Tag_Id, Warehouse_Id,Status_Type,TagBundle_Id,BundlePieceNumber) VALUES ({newTagId},{(int)tagObj.WarehouseId},29,{tb.TagBundleId},{bundlePieceNumber})");
context.ExecuteCommand(insertSql);
var newTag = context.TagEntities.FirstOrDefault(e => e.TagId == newTagId);

// ***** --- ****
/* @VendorPaymentId entry parameter */


public void TestCancelVendorPaymentForCheck(int VendorPaymentId)
{
    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {
        List<VendorPaymentForCheck> vendorPaymentData = context.ExecuteQuery<VendorPaymentForCheck>(
            @"SELECT 
                VP.VendorPayment_Id AS VendorPaymentId,
                VP.PaymentMethod_Type AS PaymentMethodType,
                VT.VendorTransaction_Id AS VendorTransactionId,	
                BT.BankTransaction_Id AS BankTransactionId,
                ISNULL(GLB.GeneralLedgerTransactionBatch_Id, 0) AS GeneralLedgerTransactionBatchId
            FROM VendorPayment VP
                LEFT JOIN VendorTransaction VT on VP.VendorPayment_Id = VT.VendorPayment_Id	
                LEFT JOIN BankTransaction BT on VP.VendorPayment_Id = BT.VendorPayment_Id	
                LEFT JOIN GeneralLedgerTransactionBatch GLB on VP.VendorPayment_Id = GLB.VendorPayment_Id	
            WHERE VP.VendorPayment_Id = {0} 
            ORDER BY GLB.GeneralLedgerTransactionBatch_Id", @VendorPaymentId).ToList();
    }

}

public class VendorPaymentForCheck
{
    public int VendorPaymentId { get; set; }
    public short PaymentMethodType { get; set; }
    public int VendorTransactionId { get; set; }
    public int BankTransactionId { get; set; }
    public int GeneralLedgerTransactionBatchId { get; set; }
}