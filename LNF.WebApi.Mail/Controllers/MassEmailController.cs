using LNF.Mail;
using System.Collections.Generic;
using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class MassEmailController : MailApiController
    {
        public MassEmailController(IProvider provider) : base(provider) { }

        [HttpPost, Route("mass-email/recipient")]
        public IEnumerable<MassEmailRecipient> GetRecipients([FromBody] MassEmailRecipientArgs args)
        {
            return Provider.Mail.MassEmail.GetRecipients(args);
        }

        [HttpGet, Route("mass-email/invalid-email")]
        public IEnumerable<IInvalidEmail> GetInvalidRecipients([FromUri] bool? active = null)
        {
            return Provider.Mail.MassEmail.GetInvalidEmails(active);
        }

        [HttpGet, Route("mass-email/invalid-email/{emailId}")]
        public IInvalidEmail GetInvalidEmail([FromUri] int emailId)
        {
            return Provider.Mail.MassEmail.GetInvalidEmail(emailId);
        }

        [HttpPost, Route("mass-email/invalid-email/add")]
        public int AddInvalidRecipient([FromBody] InvalidEmailItem model)
        {
            return Provider.Mail.MassEmail.AddInvalidEmail(model);
        }

        [HttpPost, Route("mass-email/invalid-email/modify")]
        public bool ModifyInvalidRecipient([FromBody] InvalidEmailItem model)
        {
            return Provider.Mail.MassEmail.ModifyInvalidEmail(model);
        }

        [HttpPut, Route("mass-email/invalid-email/{emailId}/active")]
        public bool SetInvalidEmailActive([FromUri] int emailId, bool value)
        {
            return Provider.Mail.MassEmail.SetInvalidEmailActive(emailId, value);
        }

        [HttpDelete, Route("mass-email/invalid-email/{emailId}")]
        public int DeleteInvalidEmail([FromUri] int emailId)
        {
            var delete = Provider.Mail.MassEmail.DeleteInvalidEmail(emailId);
            return delete ? 1 : 0;
        }

        [HttpPost, Route("mass-email/send-message-args")]
        public SendMessageArgs CreateSendMessageArgs([FromBody] MassEmailSendArgs args)
        {
            var sma = Provider.Mail.MassEmail.CreateSendMessageArgs(args);
            return sma;
        }

        [HttpPost, Route("mass-email/criteria")]
        public IRecipientCriteria CreateCriteria([FromBody] MassEmail massEmail)
        {
            var criteria = Provider.Mail.MassEmail.CreateCriteria(massEmail);
            var result = new RecipientCriteria { GroupName = criteria.GroupName, Recipients = criteria.Recipients };
            return result;
        }
    }
}
