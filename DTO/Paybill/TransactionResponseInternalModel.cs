namespace DTO.Paybill
{

    public class TransactionResponseModel
    {
        public string Code { get; set; }
        public int GatewayCode { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public PayBillResponseInternalModel Result { get; set; }
    }

    public class PayBillResponseInternalModel : PaybillResponse
    {
        public int? WalletStatus { get; set; }
        public int? WalletResponse { get; set; }
        public string WalletMessage { get; set; }
        public string ProviderResponse { get; set; }
        public string ProviderTransactionId { get; set; }
        public int? Status { get; set; }
        public int? GatewayCode { get; set; }
        public string Provider { get; set; }
        public string GatewayUrl { get; set; }
    }

}
