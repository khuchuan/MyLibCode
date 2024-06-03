using BlazorRealTest.Entity;

namespace BlazorRealTest.Data;


public class MerchantRepository : IMerchantRepository
{
    List<MerchantInfo> listMerchant = new List<MerchantInfo>();

    public MerchantRepository()
    {
        for (int i = 0; i < 15; i++)
        {
            listMerchant.Add(new MerchantInfo
            {
                MerchantCode = $"MC{i}",
                MerchantName = $"Merchant {i}",
                Status = (short)new Random().Next(0, 2)
            });
        }
    }

    public async Task<MerchantDTO> GetMerchantsAsync(short status, int page = 1, int pageSize = 10)
    {
        var list = listMerchant.FindAll(x => (status < 0 || x.Status == status));

        var result = new MerchantDTO
        {
            Data = list.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            PageSize = pageSize,
            TotalCount = list.Count
        };
        return result;
    }

    public int AddMerchant(MerchantInfo merchant)
    {
        if (listMerchant == null)
        {
            listMerchant = new List<MerchantInfo>();
        }
        listMerchant.Add(merchant);
        return listMerchant.Count;
    }
}

public interface IMerchantRepository
{
    public Task<MerchantDTO> GetMerchantsAsync(short status, int page, int pageSize);

    public int AddMerchant(MerchantInfo merchant);
}

