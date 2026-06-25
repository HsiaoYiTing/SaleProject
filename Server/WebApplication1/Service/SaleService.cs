using System.Globalization;

public class SaleService
{
    private readonly ISaleRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IStoreRepository _storerRepository;


    public SaleService(ISaleRepository repository, IProductRepository productRepository, IStoreRepository storerRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
        _storerRepository = storerRepository;
    }

    public async Task<ResponseBase<List<Sale>?>> GetSaleListAsync(QuerySaleRequest request)
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

    public async Task<ResponseBase> AddSaleAsync(AddSaleRequest request)
    {
        var product = await _productRepository.GetProductByNameAsync(request.ItemName);
        var store = await _storerRepository.GetStoreByIdAsync(request.Store);

        if (product == null)
        {
            return ResponseFactory.CreateErrorResponse("查無此產品");
        }
        if (store == null)
        {
            return ResponseFactory.CreateErrorResponse("查無此店");
        }

        Sale sale = new()
        {   
            Id = request.Guid,
            Store = store,
            SaleTime = DateTime.ParseExact(request.SaleTime, "yyyyMMddHHmmssff", CultureInfo.InvariantCulture),
            Price = request.SalePrice,
            Qty = request.Qty,
            Product = product,
            UpdateBy = "OL"
        };

        var count = await _repository.AddSaleAsync(sale);
        if (count == 0)
        {
            return ResponseFactory.CreateErrorResponse("新增失敗");
        }

        return ResponseFactory.CreateErrorResponse("新增成功");
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