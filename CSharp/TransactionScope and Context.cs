// TransactionScope and Context // *** #12116 - #12788 ***

public Tag DuplicateTagForReturn(InputForDuplicateTag inputForDuplicateTag, string sessionId, int userId)
{

    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {

    }

    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {
        try
        {
            using (TransactionScope scope = TransactionScopeHelper.CreateTransactionScope(null, null, true))
            {

                /// Commit The Transaction
                scope.Complete();

                return returnNewTag;
            }
        }
        catch (Exception ex)
        {
            log_.Error(ex.Message);
            throw ex;
        }
    }

    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {
        try
        {
            using (TransactionScope scope = TransactionScopeHelper.CreateTransactionScope(null, null, true))
            {
                if (context.Connection.State == ConnectionState.Closed)
                    context.Connection.Open();

                var trans = context.Connection.BeginTransaction();
                context.Transaction = trans;



                context.Transaction.Commit();
                scope.Complete();

                return returnNewTag;
            }

        }
        catch (Exception ex)
        {
            log_.Error(ex.Message);
            context.Transaction.Rollback();
            throw ex;
        }
    }
}

