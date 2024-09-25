using Wms24.Web.Api.TestApp_v0_9.Models;

namespace Wms24.CourierLink.Database.Data.ModelsApi
{
    public class xShippingService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourierName { get; set; }
        public string? Description { get; set; }
        public string? SubCourierName { get; set; }
        public string? Marketplace { get; set; }
        public string? WMSService { get; set; }
        public string? CourierService { get; set; }
        public ShippingServiceStatus Status { get; set; }
        public bool? OnePackageService { get; set; } = false;
        public string? DefaultPackageType { get; set; }
        public string? DefaultPackageSizeType { get; set; }
        public int? DefaultPackageLength { get; set; }
        public int? DefaultPackageWidth { get; set; }
        public int? DefaultPackageHeight { get; set; }
        public double? DefaultPackageWeight { get; set; }
        public double? DefaultInsurance { get; set; }
        public string? DefaultInsuranceCurrency { get; set; }
    }
}
