using System.Text.Json.Serialization;

namespace Wms24.Web.Api.TestApp_v0_9.Models
{
    public class LoginPost
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
