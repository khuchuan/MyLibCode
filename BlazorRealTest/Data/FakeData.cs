using BlazorRealTest.Entity;

namespace BlazorRealTest.Data;

public class FakeData
{
    private static List<MerchantInfo> ListMerchant { get; set; } = new List<MerchantInfo>();
    public static List<MerchantInfo> GetMerchants()
    {        
        for (int i = 0; i < 20; i++)
        {
            ListMerchant.Add(new MerchantInfo
            {
                MerchantCode = $"MC{i}",
                MerchantName = $"Merchant {i}",
                Status = (short)new Random().Next(0, 2)
            });
        }
        return ListMerchant;
    }

    public static int AddMerchant(MerchantInfo merchant)
    {
        if (ListMerchant == null)
        {
            ListMerchant = new List<MerchantInfo>();
        }
        ListMerchant.Add(merchant);
        return ListMerchant.Count;
    }

}
