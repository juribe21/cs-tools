using Bayern.CapstoneService.BusinessLogic;
using Bayern.CapstoneService.Shared;
using NLog;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bayern.CapstoneService.DAL.Dao.Accessors
{
public class EmailSerivceAccessor : AccessorBase<ThirdPartyInit, CapstoneModelDataContext>
{
    private readonly Logger log_ = LogManager.GetCurrentClassLogger();
    SendGridThirdPartyInit sendgrid = null;
    SendGridErrorMessage errorMessage = null;
    

    public async Task<SendGridErrorMessage> SendEmail(string emailsTo, string subject, string body)
    {
        errorMessage = new SendGridErrorMessage();

        try
        {
            GetSendGridAccess();

            if (!string.IsNullOrEmpty(errorMessage.ErrorMessage))
            {
                return errorMessage;
            }

            var mailClient = new SendGridClient(sendgrid.SendGridAPIKey);
            var sgmsg = new SendGridMessage();

            // Set From 
            sgmsg.SetFrom(new EmailAddress(sendgrid.SendGridSendFromEmailAddress, sendgrid.SendGridSendFromName));

            List<EmailAddress> recipients = BuildToEmailList(emailsTo);
            sgmsg.AddTos(recipients);

            // ReplyTo
            if (!string.IsNullOrEmpty(sendgrid.SendGridReplyToEmailAddress))
            {
                EmailAddress replayto = GetReplyto();
                sgmsg.ReplyTo = replayto;
            }

            // Subject
            sgmsg.SetSubject(subject);

            // Build message
            string message = body;

            // Build message body
            sgmsg.AddContent(MimeType.Text, message);

            var response = await mailClient.SendEmailAsync(sgmsg);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                log_.Error(string.Format("The SendGrid response get StatusCode: {0}", response.StatusCode.ToString()));
                errorMessage.ErrorMessage = "The SendGrid response get StatusCode: {0}" + response.StatusCode.ToString();
                return errorMessage;
            }
            else
            {
                errorMessage.ErrorMessage = string.Empty;
                return errorMessage;
            }
        }
        catch (Exception ex)
        {
            log_.Error(string.Format("The SendGrid SendEmail Failed Exception message: {0}", ex.Message));
            throw new Exception(ex.Message);
        }
    }

    public EmailAddress GetReplyto()
    {
        EmailAddress replayto = new EmailAddress();
        replayto.Email = sendgrid.SendGridReplyToEmailAddress.Trim();
        replayto.Name = sendgrid.SendGridReplyToName;

        return replayto;
    }

    public List<EmailAddress> BuildToEmailList(string emailsTo)
    {
        List<EmailAddress> recipients = new List<EmailAddress>();

        string[] emailList = emailsTo.Split(';', ',');

        foreach (string email in emailList)
        {
            EmailAddress emailAddress = new EmailAddress();
            emailAddress.Email = email.Trim();
            recipients.Add(emailAddress);
        }

        return recipients;
    }

    public void GetSendGridAccess()
    {
        errorMessage = new SendGridErrorMessage();

        sendgrid = Context.ExecuteQuery<SendGridThirdPartyInit>("Select * From ThirdPartyInit").FirstOrDefault();

        if (sendgrid == null)
        {
            log_.Error(string.Format("The SendGrid API information is not defined on the Manage System Properties screen."));
            errorMessage.ErrorMessage = "The SendGrid API information is not defined on the Manage System Properties screen.";
        }

        if (string.IsNullOrEmpty(sendgrid.SendGridAPIKey))
        {
            log_.Error(string.Format("The SendGrid API Key is not defined on the Manage System Properties screen."));
            errorMessage.ErrorMessage = "The SendGrid API Key is not defined on the Manage System Properties screen.";
        }

        if (string.IsNullOrEmpty(sendgrid.SendGridSendFromEmailAddress))
        {
            log_.Error(string.Format("The SendGrid Send-From Name and Email are not defined on the Manage System Properties screen."));
            errorMessage.ErrorMessage = "The SendGrid Send-From Name and Email are not defined on the Manage System Properties screen.";
        }

    }
}

}

public class SendgridFrom
{
public string FromName { get; set; }
public string FromEmail { get; set; }
}

public class SendgridReply
{
public string ReplyName { get; set; }
public string ReplyEmail { get; set; }
}

public class SendGridThirdPartyInit
{
public string SendGridAPIKey { get; set; }
public string SendGridSendFromName { get; set; }
public string SendGridSendFromEmailAddress { get; set; }
public string SendGridReplyToName { get; set; }
public string SendGridReplyToEmailAddress { get; set; }
}



private void GetAttachment()
{

    // atachments
    //Attachment attachments = GetListAttachments();

    //string filePath = "@C:\\Users\\jorge\\Desktop\\Bayern";
    //byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(filePath));
    //sgmsg.Attachments = new List<Attachment>
    //{
    //    new Attachment
    //    {
    //        Content = Convert.ToBase64String(byteData),
    //        Filename = "data.txt",
    //        Type = "txt/plain",
    //        Disposition = "attachment"
    //    },
    //    new Attachment
    //    {
    //        Content = Convert.ToBase64String(byteData),
    //        Filename = "Diagnostics.txt",
    //        Type = "txt/plain",
    //        Disposition = "attachment"
    //    }
    //};

    //sgmsg.AddAttachment(attachments);
}

public SendGridErrorMessage SendEmail(string emailsTo, string subject, string body)
{
    try
    {
        // make sure parameters are valid                
        ThrowIfNullOrEmpty(emailsTo, "emailsTo");
        ThrowIfNullOrEmpty(subject, "subject");
        ThrowIfNullOrEmpty(body, "body");

        EmailSerivceAccessor emailaccessor = new EmailSerivceAccessor(this.ConnectionString);
        return emailaccessor.SendEmail(emailsTo, subject, body).Result;

    }
    catch (CapstoneException ex)
    {
        log_.Error(ex.ToString());
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        log_.Error(ExceptionFormatter.FormatMessage(ex));
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.SendEmailFailed, ex.Message));
    }
}