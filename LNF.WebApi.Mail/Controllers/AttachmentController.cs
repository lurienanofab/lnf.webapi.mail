using LNF.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace LNF.WebApi.Mail.Controllers
{
    public class AttachmentController : MailApiController
    {
        public AttachmentController(IProvider provider) : base(provider) { }

        [HttpPost, Route("attachment")]
        public Guid Attach()
        {
            var attachments = GetAttachments();
            var result = Provider.Mail.Attachment.Attach(attachments);
            return result;
        }

        [HttpDelete, Route("attachment/{guid}")]
        public int Delete(Guid guid)
        {
            var result = Provider.Mail.Attachment.Delete(guid);
            return result;
        }

        private IEnumerable<Attachment> GetAttachments()
        {
            var attachments = new List<Attachment>();

            if (Request.Properties.TryGetValue("MS_HttpContext", out object ctx))
            {
                var context = (HttpContextBase)ctx;
                for (var x = 0; x < context.Request.Files.Count; x++)
                {
                    var f = context.Request.Files[x];
                    attachments.Add(new Attachment
                    {
                        FileName = f.FileName,
                        Data = GetFileData(f)
                    });
                }
            }

            return attachments;
        }

        private byte[] GetFileData(HttpPostedFileBase f)
        {
            using (var ms = new MemoryStream(f.ContentLength))
            {
                f.InputStream.CopyTo(ms);
                var result = ms.ToArray();
                ms.Close();
                return result;
            }
        }
    }
}
