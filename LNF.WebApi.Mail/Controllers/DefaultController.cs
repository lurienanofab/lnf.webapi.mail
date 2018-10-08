using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class DefaultController : ApiController
    {
        [Route("")]
        public string Get()
        {
            return "mail-api";
        }
    }
}
