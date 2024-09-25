namespace Wms24.CourierLink.Database.Data.ModelsApi
{
    public class xOrder
    {
        public int Id { get; set; }
        public Guid OwnerToken { get; set; }
        public int Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public bool WantInvoice { get; set; } = false;
        public List<xOrderItem> Items { get; set; }
        public xStatus? InvoiceStatus { get; set; }
        public xStatus? ERPStatus { get; set; }
        public xStatus? ERPDocStatus { get; set; }
        public xStatus? WMSStatus { get; set; }
        public xStatus? WMSDocStatus { get; set; }
        public xStatus? OrderStatus { get; set; }
        public xAddress? ReceiverAddress { get; set; }
        public xAddress? InvoiceAddress { get; set; }
        public xWarehouse? Warehouse { get; set; }
        public string? SourceInfo { get; set; }
        public int? SourceConfigId { get; set; }
        public string? MarketPlaceDocNr { get; set; }
        public int? MarketPlaceDocId { get; set; }
        public string? ERPDocNr { get; set; }
        public int? ERPDocId { get; set; }
        public int? ERPConfigId { get; set; }
        public DateTime? ERPStatusDate { get; set; }
        public string? InvoiceDocNr { get; set; }
        public int? InvoiceDocId { get; set; }
        public int? InvoiceConfigId { get; set; }
        public DateTime? InvoiceStatusDate { get; set; }
        public string? WMSDocNr { get; set; }
        public int? WMSDocId { get; set; }
        public int? WMSConfigId { get; set; }
        public DateTime? WMSStatusDate { get; set; }
        public string? Currency { get; set; }
        public string? PaymentMethod { get; set; }
        public double? PaymentValue { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool? Paid { get; set; }
        public string? UserComments { get; set; }
        public string? AdminComments { get; set; }
        public double? COD { get; set; }
        public string? CODPaymentMethod { get; set; }
        public string? CODCurrency { get; set; }
        public string? BankAccountNr { get; set; }
        public double? Insurance { get; set; }
        public string? InsuranceCurrency { get; set; }
        public double? DeclaredValue { get; set; }
        public string? DeclaredValueCurrency { get; set; }
        public string? ParcelLocker { get; set; }
        public int? CustomSourceId { get; set; }
        public string? MarketplaceUserLogin { get; set; }
        public string? ShippingServiceExternalSourceID { get; set; }
        public string? ShippingServiceSourceExternalName { get; set; }
        public bool? IsFulfillmentOrder { get; set; } = false;
        public string? FulfillmentStatus { get; set; }
        public float? ShippingPrice { get; set; }
        public string? InvoiceFullName { get; set; }
        public string? InvoiceCompany { get; set; }
        public string? InvoiceNip { get; set; }
        public string? ExtraField1 { get; set; }
        public string? ExtraField2 { get; set; }
        public DateTime? OrderStatusDate { get; set; }
        public DateTime? ImportDate { get; set; }
        public DateTime? OrderCloseDate { get; set; }
        public string? OrderPage { get; set; }
        public int? TransportOrderId { get; set; }
        public string[]? TrackingNumbers { get; set; }
        public string? SourceConfigName { get; set; }
        public string? OrderExternalStatusCurrentID { get; set; }
    }
}
