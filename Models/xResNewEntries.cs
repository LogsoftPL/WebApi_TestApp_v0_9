namespace Wms24.CourierLink.Database.Data.ModelsApi.Responses
{
    public class xResNewEntries : xIResponse
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public object[]? EntryIds { get; set; }
    }
}
