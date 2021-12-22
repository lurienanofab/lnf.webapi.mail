using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public abstract class MailApiController : ApiController
    {
        protected IProvider Provider { get; }

        public MailApiController(IProvider provider)
        {
            Provider = provider;
        }
    }
}
