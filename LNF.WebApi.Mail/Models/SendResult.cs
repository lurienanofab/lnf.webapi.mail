using Newtonsoft.Json;

namespace LNF.WebApi.Mail.Models
{
    public class SendResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        public string Message { get; internal set; }
    }
}