﻿using LNF.WebApi.Mail.Models;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class SendController : ApiController
    {
        [BasicAuthentication, Route("send")]
        public SendResult Post([FromBody] SendRequest request)
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

            SmtpClient smtp = new SmtpClient(GetSmtpHost(), GetSmtpPort());
            MailMessage mm = new MailMessage(request.From, request.To, request.Subject, request.Body);

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

        private string GetSmtpHost()
        {
            var host = ConfigurationManager.AppSettings["SmtpHost"];

            if (string.IsNullOrEmpty(host))
                throw new Exception("Missing required AppSetting: SmtpHost");

            return host;
        }

        private int GetSmtpPort()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpPort"]))
                return 25;

            int.TryParse(ConfigurationManager.AppSettings["SmtpPort"], out int port);

            return port;
        }
    }
}
