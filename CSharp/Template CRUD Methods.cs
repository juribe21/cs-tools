// Template CRUD Methods

#region TagBundleHistory Methods

/// <summary>
/// Gets a list of all Item Relationship TagBundleHistory records.
/// </summary>
[WebMethod(Description = "Gets a list of all Item Relationship TagBundleHistory records.")]
public List<TagBundleHistory> GetAllTagBundleHistory(SoapUserSession session)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);
        TagBundleHistoryEntityAccessor accessor = new TagBundleHistoryEntityAccessor(this.ConnectionString);

        List<TagBundleHistory> list = new List<TagBundleHistory>();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            list = accessor.GetAllTagBundleHistory();
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            list = accessor.GetAllTagBundleHistory();
        }

        if (log_.IsTraceEnabled)
        {
            log_.Trace("Returning {0} TagBundleHistorys items.", list.Count);
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
/// Gets the TagBundleHistoryId, null if not found.
/// </summary>
[WebMethod(Description = "Gets the TagBundleHistoryId, null if not found.")]
public TagBundleHistory GetTagBundleHistory(SoapUserSession session, int tagBundleHistoryId)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(int), tagBundleHistoryId, "tagBundleHistoryId");

        TagBundleHistoryEntityAccessor accessor = new TagBundleHistoryEntityAccessor(this.ConnectionString);

        TagBundleHistory result = new TagBundleHistory();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.GetTagBundleHistoryById(tagBundleHistoryId);

            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.GetTagBundleHistoryById(tagBundleHistoryId);
        }

        if (log_.IsTraceEnabled)
        {
            log_.Trace(((result == null) ? "TagBundleHistory [TagBundleHistoryId = {0}] not found." : "TagBundleHistory [TagBundleHistoryId = {0}] found."), tagBundleHistoryId);
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
/// Inserts the specified TagBundleHistory object into the DB.
/// </summary>
[WebMethod(Description = "Inserts the specified TagBundleHistory object into the DB.")]
public TagBundleHistory InsertTagBundleHistory(SoapUserSession session, TagBundleHistory tagBundleHistory)
{
    int methodDurationId = 0;

    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(TagBundleHistory), tagBundleHistory, "tagBundleHistory");

        TagBundleHistoryEntityAccessor accessor = new TagBundleHistoryEntityAccessor(this.ConnectionString);


        TagBundleHistory result = new TagBundleHistory();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.InsertTagBundleHistory(tagBundleHistory);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.InsertTagBundleHistory(tagBundleHistory);
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
/// Updates the specified TagBundleHistory object in the DB.
/// </summary>
[WebMethod(Description = "Updates the specified TagBundleHistory object in the DB.")]
public TagBundleHistory UpdateTagBundleHistory(SoapUserSession session, TagBundleHistory tagBundleHistory)
{
    int methodDurationId = 0;

    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(TagBundleHistory), tagBundleHistory, "tagBundleHistory");

        TagBundleHistoryEntityAccessor accessor = new TagBundleHistoryEntityAccessor(this.ConnectionString);

        TagBundleHistory result = new TagBundleHistory();
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.UpdateTagBundleHistory(tagBundleHistory);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.UpdateTagBundleHistory(tagBundleHistory);
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
/// Deletes the specified TagBundleHistory object into the DB.
/// </summary>
[WebMethod(Description = "Deletes the specified TagBundleHistory object from the DB.")]
public bool DeleteTagBundleHistory(SoapUserSession session, int tagBundleHistoryId)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)
        ThrowIfNull(typeof(int), tagBundleHistoryId, "tagBundleHistoryId");

        if (log_.IsTraceEnabled)
            log_.Trace("Deleting, TagBundleHistoryId is: {0}", tagBundleHistoryId);

        TagBundleHistoryEntityAccessor accessor = new TagBundleHistoryEntityAccessor(this.ConnectionString);

        bool result = false;
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.DeleteTagBundleHistory(tagBundleHistoryId);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.DeleteTagBundleHistory(tagBundleHistoryId);
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

#endregion TagBundleHistory Methods