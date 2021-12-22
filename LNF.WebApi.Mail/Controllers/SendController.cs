using LNF.Mail;
using LNF.WebApi.Mail.Models;
using System.Net.Mail;
using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class SendController : MailApiController
    {
        public SendController(IProvider provider) : base(provider) { }

        /// <summary>
        /// A basic send email endpoint.
        /// </summary>
        /// <param name="request">Information about the message to send.</param>
        [HttpPost, Route("send")]
        public SendResult Send([FromBody] SendRequest request)
        {
            var result = new SendResult();

            if (string.IsNullOrEmpty(request.From))
            {
                result.Result = false;
                result.Message = "From is required.";
                return result;
            }

            if (string.IsNullOrEmpty(request.To))
            {
                result.Result = false;
                result.Message = "To is required.";
                return result;
            }

            if (string.IsNullOrEmpty(request.Subject))
            {
                result.Result = false;
                result.Message = "Subject is required.";
                return result;
            }

            if (string.IsNullOrEmpty(request.Body))
            {
                result.Result = false;
                result.Message = "Body is required.";
                return result;
            }

            var smtp = Impl.Mail.MailUtility.GetSmtpClient();
            var mm = new MailMessage(request.From, request.To, request.Subject, request.Body);

            if (!string.IsNullOrEmpty(request.Cc))
                mm.CC.Add(request.Cc);

            if (!string.IsNullOrEmpty(request.Bcc))
                mm.Bcc.Add(request.Bcc);

            mm.IsBodyHtml = request.IsHtml;

            smtp.Send(mm);

            result.Result = true;
            result.Message = "Message send OK!";

            return result;
        }

        /// <summary>
        /// Sends a mass email message and returns the number of To recipients.
        /// </summary>
        /// <param name="args">Information used to create and send a message.</param>
        [HttpPost]
        [Route("send/mass-email")]
        public int SendMassEmail([FromBody] MassEmailSendArgs args)
        {
            return Provider.Mail.SendMassEmail(args);
        }

        /// <summary>
        /// Sends a generic message.
        /// </summary>
        /// <param name="args">Information used to create and send a message.</param>
        [HttpPost]
        [Route("send/message")]
        public string SendMessage([FromBody] SendMessageArgs args)
        {
            // returing any non-empty string means an error occured
            Provider.Mail.SendMessage(args);
            return string.Empty;
        }
    }
}
