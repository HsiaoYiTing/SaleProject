using System.Globalization;
using System.Text;

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

        var list = await _repository.GetSalesByConditionsAsync(startTime, endTime, request.StoreId);

        if (list == null || list.Count == 0)
        {
            return ResponseFactory.CreateErrorResponse<List<Sale>?>(null, "無符合資料");
        }

        return ResponseFactory.CreateErrorResponse<List<Sale>?>(list, "查詢成功");
    }

    // 單筆JSON
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

        Sale sale = SaleFactory.ParseByRequest(request, store, product);

        var count = await _repository.AddSaleAsync(sale);
        if (count == 0)
        {
            return ResponseFactory.CreateErrorResponse("新增失敗");
        }

        return ResponseFactory.CreateErrorResponse("新增成功");
    }

    // BIG5 File
    public async Task<ResponseBase> ImportAsync(IFormFile file)
    {
        if (file == null || file.Length == 0) return ResponseFactory.CreateErrorResponse("請選擇檔案");

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var stream = file.OpenReadStream();

        using var reader = new StreamReader(stream, Encoding.GetEncoding("Big5"));

        List<SaleLine> sales = [];

        string? line;

        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            sales.Add(SaleFactory.ParseByLine(line));
        }

        await _repository.AddSaleAsync(sales);

        return ResponseFactory.CreateSuccessResponse("import成功");
    }
}