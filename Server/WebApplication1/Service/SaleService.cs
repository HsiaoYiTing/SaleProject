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

    public async Task<ResponseBase<List<Sale>?>> GetSaleListAsync(RequestBase request)
    {
        var date = request.Date;
        var startTime = date.ToDateTime(TimeOnly.MinValue);
        var endTime = date.AddDays(1).ToDateTime(TimeOnly.MinValue);

        var list = await _repository.GetSalesByConditionsAsync(startTime, endTime, request.StoreId);

        if (list == null || list.Count == 0)
        {
            return ResponseFactory.CreateErrorResponse<List<Sale>?>(null, "無符合資料");
        }

        return ResponseFactory.CreateErrorResponse<List<Sale>?>(list, "查詢成功");
    }

    // 單筆JSON
    public async Task<ResponseBase> AddSaleAsync(List<AddSaleRequest> request)
    {
        var productList = await _productRepository.GetAllAsync();
        var storeList = await _storerRepository.GetAllAsync();

        List<Sale> sales = [];

        foreach(AddSaleRequest r in request)
        {
            var product = productList.FirstOrDefault(p => p.Id == r.ItemId);
            if (product == null)
            {
                Console.Write("[Import Failed_查無此產品] " + r.ToString());
                continue;
            }

            var store = storeList.FirstOrDefault(p => p.Id == r.Store);
            if (store == null)
            {
                Console.Write("[Import Failed_查無此店] " + r.ToString());
                continue;
            }

            var sale = SaleFactory.ParseByRequest(r, store, product);
            if (sale == null)
            {
                Console.Write("[Add Failed] " + r.ToString());
                continue;
            }

            sales.Add(sale);
        }
        
        var count = await _repository.AddSaleAsync(sales);
        if (count == 0)
        {
            return ResponseFactory.CreateErrorResponse("新增失敗");
        }

        return ResponseFactory.CreateErrorResponse("新增成功");
    }

    // BIG5 File
    public async Task<ResponseBase> ImportAsync(FileRequest request)
    {
        var file =  request.File;
        if (file == null || file.Length == 0) return ResponseFactory.CreateErrorResponse("請選擇檔案");

        List<Sale> sales = [];

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream, Encoding.GetEncoding("Big5"));

        string? line;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var sale = SaleFactory.ParseByLine(line, request.UpdateBy);

            if (sale != null)
            {
                sales.Add(sale);
            }
            else
            {
                Console.Write("[Import Failed] " + line);
            }
        }
        
        var startTime = DateTime.Today;
        var endTime = DateTime.Today.AddDays(1);

        await _repository.DeleteSaleAsync(startTime, endTime, request.UpdateBy);

        await _repository.AddSaleAsync(sales);

        return ResponseFactory.CreateSuccessResponse("import成功");
    }
}