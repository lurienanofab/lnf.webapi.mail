using Newtonsoft.Json;

namespace LNF.WebApi.Mail.Models
{
    public class SendRequest
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("subj")]
        public string Subject { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("cc")]
        public string Cc { get; set; }

        [JsonProperty("bcc")]
        public string Bcc { get; set; }

        [JsonProperty("is_html")]
        public bool IsHtml { get; set; }
    }
}