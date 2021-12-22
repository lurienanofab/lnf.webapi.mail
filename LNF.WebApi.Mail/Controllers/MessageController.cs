using LNF.Data;
using LNF.Mail;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class MessageController : MailApiController
    {
        public MessageController(IProvider provider) : base(provider) { }

        [HttpGet, Route("message")]
        public IEnumerable<IMessage> GetMessages(DateTime sd, DateTime ed, int clientId = 0)
        {
            return Provider.Mail.GetMessages(sd, ed, clientId);
        }

        [HttpGet, Route("message/{messageId}")]
        public IMessage GetMessage([FromUri] int messageId)
        {
            return Provider.Mail.GetMessage(messageId);
        }

        [HttpGet, Route("message/{messageId}/recipient")]
        public IEnumerable<IRecipient> GetRecipients([FromUri] int messageId)
        {
            return Provider.Mail.GetRecipients(messageId);
        }

        [HttpGet, Route("message/recipient")]
        public IEnumerable<string> GetEmailListByPrivilege(ClientPrivilege privs)
        {
            return Provider.Mail.GetEmailListByPrivilege(privs);
        }
    }
}
