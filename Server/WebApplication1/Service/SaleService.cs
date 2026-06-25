public class SaleService
{
    private readonly ISaleRepository _repository;


    public SaleService(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseBase<List<Sale>?>> GetSaleListAsync(SaleRequest request)
    {
        var startTime = request.Date.ToDateTime(TimeOnly.MinValue);
        var endTime = request.Date.AddDays(1).ToDateTime(TimeOnly.MinValue);

        var list = await _repository.GetSalesByDateAsync(startTime, endTime, request.StoreId);

        if (list == null || list.Count == 0)
        {
            return ResponseFactory.CreateErrorResponse<List<Sale>?>(null, "無符合資料");
        }

        return ResponseFactory.CreateErrorResponse<List<Sale>?>(list, "查詢成功");
    }

    public async Task<ResponseBase> AddSaleAsync(Sale sale)
    {
        var count = await _repository.AddSaleAsync(sale);

        if (count == 0)
        {
            return ResponseFactory.CreateErrorResponse("新增失敗");
        }

        return ResponseFactory.CreateErrorResponse("新增成功");
    }

    public async Task<ResponseBase> AddSaleAsync(List<Sale> saleList)
    {
        var count = await _repository.AddSaleAsync(saleList);

        if (count == 0)
        {
            return ResponseFactory.CreateErrorResponse("新增失敗");
        }
        
        return ResponseFactory.CreateErrorResponse("新增成功");
    }
}