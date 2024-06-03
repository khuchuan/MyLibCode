namespace BlazorRealTest.Entity;

public class MerchantDTO
{
    public MerchantDTO()
    {
        Data = new List<MerchantInfo>();
    }

    public List<MerchantInfo> Data { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
