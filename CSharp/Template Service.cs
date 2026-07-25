//Service Template


#region TagBundleAllocation Methods

/// <summary>
/// Gets a list of all Tag Bundle Note records.
/// </summary>
[WebMethod(Description = "Gets a list of all Tag Bundle Allocations Note records.")]
public List<TagBundleAllocation> GetAllTagBundleAllocations(SoapUserSession session)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);
        TagBundleAllocationEntityAccessor accessor = new TagBundleAllocationEntityAccessor(this.ConnectionString);

        List<TagBundleAllocation> list = new List<TagBundleAllocation>();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            list = accessor.GetAll();
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            list = accessor.GetAll();
        }

        if (log_.IsTraceEnabled)
        {
            log_.Trace("Returning {0} TagBundleAllocations items.", list.Count);
        }
        return list;
    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetAllFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetAllFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.GetAllFailed, ex.Message));
    }
}

/// <summary>
/// Gets the Vendor Refund matching the TagBundleAllocationId , null if not found.
/// </summary>
[WebMethod(Description = "Gets the Vendor Refund matching the TagBundleAllocationId , null if not found.")]
public TagBundleAllocation GetTagBundleAllocation(SoapUserSession session, int tagBundleId)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(int), tagBundleId, "tagBundleId");

        TagBundleAllocationEntityAccessor accessor = new TagBundleAllocationEntityAccessor(this.ConnectionString);

        TagBundleAllocation result = new TagBundleAllocation();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.GetById(tagBundleId);

            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.GetById(tagBundleId);

        }

        if (log_.IsTraceEnabled)
        {
            log_.Trace(((result == null) ? "TagBundleAllocation [TagBundleAllocationId = {0}] not found." : "TagBundleAllocation [TagBundleAllocationId = {0}] found."), tagBundleId);
        }
        return result;
    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetByIdFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetByIdFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.GetByIdFailed, ex.Message));
    }
}

/// <summary>
/// Inserts the specified TagBundleAllocation object into the DB.
/// </summary>
[WebMethod(Description = "Inserts the specified TagBundleAllocation object into the DB.")]
public TagBundleAllocation InsertTagBundleAllocation(SoapUserSession session, TagBundleAllocation vendorRefund)
{
    int methodDurationId = 0;

    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(TagBundleAllocation), vendorRefund, "vendorRefund");

        TagBundleAllocationEntityAccessor accessor = new TagBundleAllocationEntityAccessor(this.ConnectionString);


        TagBundleAllocation result = new TagBundleAllocation();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.InsertTagBundleAllocation(vendorRefund);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.InsertTagBundleAllocation(vendorRefund);
        }

        return result;
    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.InsertFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.InsertFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.InsertFailed, ex.Message));
    }
}

/// <summary>
/// Updates the specified TagBundleAllocation object in the DB.
/// </summary>
[WebMethod(Description = "Updates the specified TagBundleAllocation object in the DB.")]
public TagBundleAllocation UpdateTagBundleAllocation(SoapUserSession session, TagBundleAllocation vendorRefund)
{
    int methodDurationId = 0;

    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(TagBundleAllocation), vendorRefund, "vendorRefund");

        TagBundleAllocationEntityAccessor accessor = new TagBundleAllocationEntityAccessor(this.ConnectionString);

        TagBundleAllocation result = new TagBundleAllocation();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.UpdateTagBundleAllocation(vendorRefund);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.UpdateTagBundleAllocation(vendorRefund);
        }

        return result;
    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.UpdateFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.UpdateFailed, ex.Message));
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.UpdateFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.UpdateFailed, ex.Message));
    }
}

/// <summary>
/// Deletes the specified TagBundleAllocation object into the DB.
/// </summary>
[WebMethod(Description = "Deletes the specified TagBundleAllocation object from the DB.")]
public bool DeleteTagBundleAllocation(SoapUserSession session, int tagBundleId)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(int), tagBundleId, "tagBundleId");

        if (log_.IsTraceEnabled)
            log_.Trace("Deleting, TagBundleAllocationId is: {0}", tagBundleId);

        TagBundleAllocationEntityAccessor accessor = new TagBundleAllocationEntityAccessor(this.ConnectionString);

        bool result = false;
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.DeleteTagBundleAllocation(tagBundleId);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.DeleteTagBundleAllocation(tagBundleId);
        }

        return result;

    }

    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.DeleteFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.DeleteFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException
    (new CapstoneException(BusinessLogicException.FailedToCreateExportSpreadSheet,
        "Failed to delete credit card Payment, see error log.", ex.Message, ex.StackTrace));
    }
}

#endregion TagBundleAllocation Methods


/// Create Server Directory
string logDirectory = string.Concat(Server.MapPath("/"), @"\", @"Services\ImportExport\");

if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}
if (log_.IsTraceEnabled)
{
    log_.Trace(((creditCardsAccount == null) ? "GetActiveCreditCardAccountsForCurrency [CurrencyId = {0}] not found."
        : "GetActiveCreditCardAccountsForCurrency [CurrencyId = {0}] found."), currencyId);
}

public AssociationsForCustomerDebit GetAssociationsForCustomerDebit(SoapUserSession session, int customerDebitId)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);
        CustomerTransactionEntityAccessor accessor = new CustomerTransactionEntityAccessor(this.ConnectionString);

        // validate required parameter(s)               
        ThrowIfNull(typeof(int), customerDebitId, "customerDebitId");

        AssociationsForCustomerDebit associations = new AssociationsForCustomerDebit();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            associations = accessor.GetAssociationsForCustomerDebit(customerDebitId, session.UserId);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            associations = accessor.GetAssociationsForCustomerDebit(customerDebitId, session.UserId);
        }

        if (log_.IsTraceEnabled)
        {
            log_.Trace(((associations == null) ? "GetAssociationsForCustomerDebit [CustomerDebitId = {0}] not found."
                : "GetAssociationsForCustomerDebit [CustomerDebitId = {0}] found."), customerDebitId);
        }
        return associations;
    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetAssociationsForCustomerDebitFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetAssociationsForCustomerDebitFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.GetAssociationsForCustomerDebitFailed, ex.Message));
    }
}
