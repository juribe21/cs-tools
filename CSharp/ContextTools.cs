/// Array declaration
int[] statusTypeShipmentPickedUpOrDelivered = new[] { 2, 3, 5 };

/// SUm Validation
result.CurrentAmount = (from vt in Context.VendorTransactionEntities
                        where vt.VendorId == vendorId && vt.Balance != 0
                        && (vt.DueDate >= DateTime.Now || vt.DueDate.HasValue == false || vt.DueDate == default(DateTime))
                        select (decimal?)vt.Balance).Sum() ?? 0;
                        

// ---- COVERT TO DATETIME ----
DateTime lowdate = Convert.ToDateTime("2021-01-01 00:00:00.000");
DateTime highdate = Convert.ToDateTime("2021-12-31 00:00:00.000");

public string PostCustomerMiscellaneousCreditTransaction(int customerMiscellaneousCreditId, DateTime gLPostingDate)
{
    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {
        try
        {
            using (TransactionScope scope = TransactionScopeHelper.CreateTransactionScope(null, null, true))
            {
                if (context.Connection.State == ConnectionState.Closed)
                    context.Connection.Open();

                // CODE HERE ..

                context.Transaction.Commit();
                scope.Complete();
                return string.Empty;
            }

        }
        catch (Exception ex)
        {
            log_.Error(ex.Message);
            context.Transaction.Rollback();
            throw ex;
        }
    }

    return string.Empty;
}

public bool CheckOnHandInventoryForItem(int itemId)
{
    try
    {
        using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
        {
            List<Tag> tags = new List<Tag>();
            tags = context
                .ExecuteQuery<Tag>(@"SELECT TOP 1 Tag_Id FROM Tag WHERE	Tag.Item_Id	= {0} AND Tag.Status_Type = 1",
                    itemId).ToList();

            if (tags.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    catch (Exception ex)
    {
        throw new CapstoneException(BusinessLogicException.GetFailed,
            "Get Record process failed. " + ", Exception Message: " + ex.Message);
    }
}

context.ExecuteQuery

using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
{
}


updateHeaderResult = context.ExecuteCommand(@"UPDATE WorkOrderHeader SET WorkOrderHeader.LastEditedBy_LoginUser_Id = {0}, WorkOrderHeader.LastEdited_DateTime = {1} WHERE WorkOrderHeader.WorkOrderHeader_Id = {2}",
                        userId, systemDateTime, workOrderHeaderId);

context.ExecuteCommand(@"
        UPDATE Tag SET Observation1_TagObservationCode_Id = {0}, Observation2_TagObservationCode_Id = null, Observation3_TagObservationCode_Id = null, Observation4_TagObservationCode_Id = null,
                       Observation5_TagObservationCode_Id = null WHERE Tag_Id = {1}", tagEntity.Observation1TagObservationCodeId, tagId);



workOrderHeaderIdList = Context.ExecuteQuery<int>(@"SELECT DISTINCT (WOID.WorkOrderHeader_Id)
                        FROM WorkOrderOutputDetail WOOD
                            JOIN WorkOrderInputDetail WOID ON WOID.WorkOrderInputDetail_Id = WOOD.WorkOrderInputDetail_Id
                        WHERE WOOD.Output_SalesOrderDetail_Id = {0}", item.SalesOrderDetailId).ToList();

updateTagResult = context.ExecuteCommand(@"UPDATE Tag SET 
                                                     Tag.Status_Type = 4,
                                                     Tag.ShippedUnder_SalesInvoiceDetail_Id = null,
                                                     Tag.TotalInvoiceWeight = null                                                     
                                                     where Tag.ShippedUnder_SalesInvoiceDetail_Id = {0}", salesInvoiceDetailId);


public int LockRecord(string sessionId, string tableName, int recordId, RecordLock record = null)
{
    try
    {
        var recordLocked = record ?? GetRecordLockDetail(tableName, recordId);
        if (recordLocked == null)
        {
            var insertQuery = "INSERT INTO RecordLock (Session_Id, TableName, Record_Id, Lock_DateTime) " +
                "VALUES(@Session_Id, @TableName, @Record_Id, @Lock_DateTime)";

            using (var command = new SqlCommand(insertQuery))
            {
                command.Parameters.Add("@Session_Id", SqlDbType.NVarChar).Value = sessionId;
                command.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tableName;
                command.Parameters.Add("@Record_Id", SqlDbType.Int).Value = recordId;
                command.Parameters.Add("@Lock_DateTime", SqlDbType.DateTime).Value = DateTime.Now;
                return ExecuteNonQuery(command);
            }
        }
        return 0;
    }
    catch (Exception ex)
    {
        _logger.Error(ex);
        throw;
    }
}


// -------------------------------SalesOrder.UnitTest--------------------------------------------------
[TestMethod]
public void TestAutoStageInventoryForSalesOrderBillOfMaterialsDetail()
{
    try
    {
        SalesOrderDetailEntityAccessor accessor = new SalesOrderDetailEntityAccessor(Helper._ConnectionString);
        DateTime startTimef = DateTime.Now;

        var result = accessor.AutoStageInventoryForSalesOrderBillOfMaterialsDetail(63, "1588755270622.03", 1);

        DateTime endTimef = DateTime.Now;
        TimeSpan AvgtimeSpent = endTimef.Subtract(startTimef);


        if (result != null)
        {

        }
    }
    catch (Exception ex)
    {
        string message = ex.Message;
    }
}

[TestMethod]
public void TestAutoStageInventoryForSalesOrderBillOfMaterialsDetail()
{
    try
    {
        DateTime startTimef = DateTime.Now;
        // Code Here
        DateTime endTimef = DateTime.Now;
        TimeSpan AvgtimeSpent = endTimef.Subtract(startTimef);
    }
    catch (Exception ex)
    {
        string message = ex.Message;
    }
}


// Get the query generated in SQL
string sql = openSales.ToString();
// --------------------------------------------------------------------------------------------------------
// Get the count rows
int records = openSales.AsEnumerable().Count();

// --------------------------------------------------------------------------------------------------------
string updateString = "UPDATE Tag SET ";
updateString += observation1TagObservationCodeId > 0 ? $"Observation1_TagObservationCode_Id = {observation1TagObservationCodeId}," : "Observation1_TagObservationCode_Id = NULL,";
updateString += observation2TagObservationCodeId > 0 ? $"Observation2_TagObservationCode_Id = {observation2TagObservationCodeId}," : "Observation2_TagObservationCode_Id = NULL,";
updateString += observation3TagObservationCodeId > 0 ? $"Observation3_TagObservationCode_Id = {observation3TagObservationCodeId}," : "Observation3_TagObservationCode_Id = NULL,";
updateString += observation4TagObservationCodeId > 0 ? $"Observation4_TagObservationCode_Id = {observation4TagObservationCodeId}," : "Observation4_TagObservationCode_Id = NULL,";
updateString += observation5TagObservationCodeId > 0 ? $"Observation5_TagObservationCode_Id = {observation5TagObservationCodeId}" : "Observation5_TagObservationCode_Id = NULL";
updateString += $" WHERE Tag_Id = {tagId}";
context.ExecuteCommand(updateString);
context.SubmitChanges();

//----DTOConversion----
var loginUserUpdateobj = DTOConversion.ConvertTo<LoginUser>(obj);