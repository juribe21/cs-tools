/* ***** Insert ADO ***** */

var insertBTH = "INSERT INTO BankTransactionHistory "
     + "(BankTransaction_Id, CreatedBy_LoginUser_Id, Created_UTCDateTime, Transaction_Date, CheckNumber "
     + ",Amount1, Amount2, Amount3, Amount4, Amount5, Amount6, Amount7, Amount8, Amount9, Amount10, TotalAmount"
     + ",Offset1_GeneralLedgerAccount_Id, Offset2_GeneralLedgerAccount_Id, Offset3_GeneralLedgerAccount_Id, Offset4_GeneralLedgerAccount_Id, Offset5_GeneralLedgerAccount_Id"
     + ",Offset6_GeneralLedgerAccount_Id, Offset7_GeneralLedgerAccount_Id, Offset8_GeneralLedgerAccount_Id, Offset9_GeneralLedgerAccount_Id, Offset10_GeneralLedgerAccount_Id"
     + ",Description, PayeeOrPayor,  Voided_Flag) " +
"VALUES(@BankTransactionId,@CreatedByLoginUserId,@CreatedUTCDateTime,@TransactionDate,@CheckNumber,"
     + "@Amount1,@Amount2,@Amount3,@Amount4,@Amount5,@Amount6,@Amount7,@Amount8,@Amount9,@Amount10,@TotalAmount,"
     + "@Offset1GeneralLedgerAccountId,@Offset2GeneralLedgerAccountId,@Offset3GeneralLedgerAccountId,@Offset4GeneralLedgerAccountId,@Offset5GeneralLedgerAccountId,"
     + "@Offset6GeneralLedgerAccountId,@Offset7GeneralLedgerAccountId,@Offset8GeneralLedgerAccountId,@Offset9GeneralLedgerAccountId,@Offset10GeneralLedgerAccountId,"
     + "@Description,@PayeeOrPayor,@VoidedFlag)";

BankTransactionHistory bth = new BankTransactionHistory();
var checkNumber = bankTransaction.CheckNumber.HasValue ? bankTransaction.CheckNumber : null;

using (var dbConnection = new SqlConnection(this.ConnectionString))
{
    dbConnection.Open();
    using (var command = new SqlCommand(insertBTH))
    {
        command.Connection = dbConnection;
        command.Parameters.Clear();

        command.Parameters.Add("@BankTransactionId", SqlDbType.Int).Value = bankTransaction.BankTransactionId;
        command.Parameters.Add("@CreatedByLoginUserId", SqlDbType.Int).Value = userId;
        command.Parameters.Add("@CreatedUTCDateTime", SqlDbType.DateTime).Value = DateTime.UtcNow;
        command.Parameters.Add("@TransactionDate", SqlDbType.Date).Value = bankTransaction.TransactionDate;

        if (bankTransaction.CheckNumber.HasValue)
        {
            command.Parameters.AddWithValue("@CheckNumber", bankTransaction.CheckNumber.HasValue);
        }
        else
        {
            command.Parameters.AddWithValue("@CheckNumber", DBNull.Value);
        }


        command.Parameters.Add("@Amount1", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount2", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount3", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount4", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount5", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount6", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount7", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount8", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount9", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@Amount10", SqlDbType.Decimal).Value = 0;
        command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = 0;

        command.Parameters.Add("@Offset1GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset1GeneralLedgerAccountId;
        command.Parameters.Add("@Offset2GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset2GeneralLedgerAccountId;
        command.Parameters.Add("@Offset3GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset3GeneralLedgerAccountId;
        command.Parameters.Add("@Offset4GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset4GeneralLedgerAccountId;
        command.Parameters.Add("@Offset5GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset5GeneralLedgerAccountId;
        command.Parameters.Add("@Offset6GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset6GeneralLedgerAccountId;
        command.Parameters.Add("@Offset7GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset7GeneralLedgerAccountId;
        command.Parameters.Add("@Offset8GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset8GeneralLedgerAccountId;
        command.Parameters.Add("@Offset9GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset9GeneralLedgerAccountId;
        command.Parameters.Add("@Offset10GeneralLedgerAccountId", SqlDbType.Int).Value = bankTransaction.Offset10GeneralLedgerAccountId;

        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = bankTransaction.Description;
        command.Parameters.Add("@PayeeOrPayor", SqlDbType.NVarChar).Value = bankTransaction.PayeeOrPayor;
        command.Parameters.Add("@VoidedFlag", SqlDbType.Bit).Value = bankTransaction.VoidedFlag;

        command.ExecuteNonQuery();
    }
    dbConnection.Close();
}