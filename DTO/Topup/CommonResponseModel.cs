namespace DTO.Topup
{
    public class CommonResponseModel<T>
    {
        public string? Status { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

    }

    public class TopupResultModel
    {
        public int TopupStatus { get; set; }
        public string? TopupMessage { get; set; }
    }
}
