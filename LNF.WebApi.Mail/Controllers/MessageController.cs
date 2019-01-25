using LNF.Models.Mail;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    [BasicAuthentication]
    public class MessageController : ApiController
    {
        [Route("message")]
        public string Post([FromBody] SendMessageArgs args)
        {
            // returing any non-empty string means an error occured

            int messageId = Repo.InsertMessage(args.ClientID, args.Caller, args.From, args.Subject, args.Body);

            if (messageId == 0)
                return "Failed to create message [messageId = 0]";

            Repo.InsertRecipients(messageId, AddressType.To, args.To);
            Repo.InsertRecipients(messageId, AddressType.Cc, args.Cc);
            Repo.InsertRecipients(messageId, AddressType.Bcc, args.Bcc);

            try
            {
                MailUtility.Send(args);
                Repo.SetMessageSent(messageId);
                return string.Empty;
            }
            catch (Exception ex)
            {
                Repo.SetMessageError(messageId, ex.Message);
                return ex.ToString();
            }
        }

        [Route("message")]
        public IEnumerable<MessageItem> Get(DateTime sd, DateTime ed, int clientId = 0)
        {
            return Repo.SelectMessages(sd, ed, clientId);
        }

        [Route("message/{messageId}")]
        public MessageItem Get(int messageId)
        {
            return Repo.SelectMessage(messageId);
        }
    }
}
