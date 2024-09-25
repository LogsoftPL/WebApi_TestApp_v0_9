namespace Wms24.Web.Api.TestApp_v0_9.Models
{
    public class ApiToken
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }

        public override string ToString()
        {
            return Token;
        }
    }
}
