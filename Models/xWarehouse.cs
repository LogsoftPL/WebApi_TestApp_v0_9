namespace Wms24.CourierLink.Database.Data.ModelsApi
{
    public class xWarehouse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public xAddress Address { get; set; }
        public string? Description { get; set; }
    }
}
