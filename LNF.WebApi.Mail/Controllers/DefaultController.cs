﻿using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class DefaultController : MailApiController
    {
        public DefaultController(IProvider provider) : base(provider) { }

        /// <summary>
        /// Gets the API name.
        /// </summary>
        [AllowAnonymous, Route("")]
        public string Get()
        {
            return "mail-api";
        }
    }
}
