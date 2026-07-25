// LogMethodDuration
/*
    int methodDurationId = 0;
    if (logMethodDuration)
    {
    }
    Inside catch(...)
*/
[WebMethod(Description = "Export Vendor Credits")]
public string ExportVendorCredits(SoapUserSession session, ExportVendorCreditInput input)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);
        ThrowIfNull(typeof(ExportVendorCreditInput), input, "input");

        VendorCreditEntityAccessor accessor = new VendorCreditEntityAccessor(this.ConnectionString);

        string destinationDirectory = string.Concat(Server.MapPath("/"), @"\", @"Services\ImportExport\");

        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }

        var result = string.Empty;
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;

            result = accessor.ExportVendorCredits(input, session.UserId, destinationDirectory);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            result = accessor.ExportVendorCredits(input, session.UserId, destinationDirectory);
        }

        return result;
    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.ExportToExcelFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ex.ToString());
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.ExportToExcelFailed.ToString());
        }
        throw SoapExceptionHelper.ToSoapException
            (new CapstoneException(BusinessLogicException.FailedToCreateExportSpreadSheet,
                "Failed to create Export Vendor Credits spreadsheet, see error log.", ex.Message, ex.StackTrace));
    }
}
