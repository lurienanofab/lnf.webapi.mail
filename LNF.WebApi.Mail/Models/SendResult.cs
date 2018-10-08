using Newtonsoft.Json;

namespace LNF.WebApi.Mail.Models
{
    public class SendResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}