using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

public class SaleSumService
{
    private readonly ISaleSumRepository _repository;


    public SaleSumService(ISaleSumRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseBase<List<SaleSum>?>> GetSaleSumListAsync(SaleSumRequest request)
    {
        
        var storeId = request.StoreId;

        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        var startDate = request.StartDate;
        var endDate = request.EndDate;

        var sumList = await _repository.GetSalesByConditionsAsync(startDate, endDate, storeId);

        if (sumList != null)
        {
            return ResponseFactory.CreateSuccessResponse<List<SaleSum>?>(sumList, "取得成功");
        } 
        else
        {
            return ResponseFactory.CreateErrorResponse<List<SaleSum>?>(null, "查無相關資料");
        }
    }

}