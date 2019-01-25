using LNF.Models.Mail;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace LNF.WebApi.Mail
{
    public static class MailUtility
    {
        public static void Send(SendMessageArgs args)
        {
            MailMessage mm = new MailMessage { From = GetFromAddress(args) };

            AddAddresses(mm.To, args.To);
            AddAddresses(mm.CC, args.Cc);
            AddAddresses(mm.Bcc, args.Bcc);

            mm.Subject = args.Subject;
            mm.Body = args.Body;
            mm.IsBodyHtml = args.IsHtml;

            AddAttachments(mm.Attachments, args.Attachments);

            var smtp = GetSmtpClient();

            smtp.Send(mm);
        }

        public static SmtpClient GetSmtpClient()
        {
            var host = GetSmtpHost();
            var port = GetSmtpPort();
            var un = GetSmtpUsername();
            var pw = GetSmtpPassword();

            SmtpClient client = new SmtpClient(host, port);

            if (!string.IsNullOrEmpty(un) && !string.IsNullOrEmpty(pw))
            {
                client.Credentials = new NetworkCredential(un, pw);
                client.EnableSsl = GetSmtpEnableSsl();
            }

            return client;
        }

        public static MailAddress GetFromAddress(SendMessageArgs args)
        {
            MailAddress result;

            if (!string.IsNullOrEmpty(args.DisplayName))
                result = new MailAddress(args.From, args.DisplayName);
            else
                result = new MailAddress(args.From);

            return result;
        }

        public static void AddAddresses(MailAddressCollection col, IEnumerable<string> addrs)
        {
            if (addrs != null)
            {
                foreach (var addr in addrs)
                {
                    col.Add(new MailAddress(addr));
                }
            }
        }

        public static void AddAttachments(AttachmentCollection col, IEnumerable<string> atts)
        {
            if (atts != null)
            {
                foreach (var att in atts)
                {
                    col.Add(new Attachment(att));
                }
            }
        }

        public static string GetSmtpHost()
        {
            string result = ConfigurationManager.AppSettings["SmtpHost"];

            if (string.IsNullOrEmpty(result))
                return "localhost";
            else
                return result;
        }

        public static int GetSmtpPort()
        {
            string result = ConfigurationManager.AppSettings["SmtpPort"];

            if (!int.TryParse(result, out var port))
                return 25;
            else
                return port;
        }

        public static string GetSmtpUsername()
        {
            return ConfigurationManager.AppSettings["SmtpUsername"];
        }

        public static string GetSmtpPassword()
        {
            return ConfigurationManager.AppSettings["SmtpPassword"];
        }

        public static bool GetSmtpEnableSsl()
        {
            string result = ConfigurationManager.AppSettings["SmtpEnableSsl"];

            if (!bool.TryParse(result, out var enableSsl))
                return false;
            else
                return enableSsl;
        }
    }
}