namespace Wms24.CourierLink.Database.Data.ModelsApi
{
    public class xOrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double OrderedQuantity { get; set; }
        public int? LineNumber { get; set; }
        public int? ProductId { get; set; } = 0;
        public string? ProductExternalId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductAlternativeCode { get; set; }
        public string? ProductWarehouseGroup { get; set; }
        public string? ProductEAN { get; set; }
        public string? ProductUnit { get; set; }
        public double? ProductWeight { get; set; }
        public double? ProductVolume { get; set; }
        public double? ProductHeight { get; set; }
        public double? ProductLength { get; set; }
        public double? ProductWidth { get; set; }
        public string? ProductBoxUnit { get; set; }
        public string? ProductBoxEAN { get; set; }
        public double? ProductBoxQuantity { get; set; }
        public string? ProductPalletUnit { get; set; }
        public string? ProductPalletEAN { get; set; }
        public double? ProductPalletQuantity { get; set; }
        public bool? ProductIsService { get; set; } = false;
        public double? ConfirmedQuantity { get; set; }
        public double? PriceNetto { get; set; }
        public double? TaxRate { get; set; }
        public double? PriceBrutto { get; set; }
        public string? Offer { get; set; }
        public string? Set { get; set; }
        public DateTime? BoughtDate { get; set; }
    }
}
