using Wms24.Web.Api.TestApp_v0_9.Models;

namespace Wms24.CourierLink.Database.Data.ModelsApi
{
    public class xOwner
    {
        public Guid Token { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public xAddress? Address { get; set; }
        public string? Description { get; set; }
        public string? WmsId { get; set; }
    }
}
