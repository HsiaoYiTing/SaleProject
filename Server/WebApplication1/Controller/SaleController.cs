using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sale")]

public class SaleController: ControllerBase
{
    private readonly ILogger<SaleController> _logger;
    private readonly SaleService _service;

    public SaleController(ILogger<SaleController> logger, SaleService service)
    {
        _logger = logger;
        _service = service;
    }


    [HttpPost("findbyDate")]
    public async Task<ResponseBase<List<Sale>?>> FindByConditionsAsync([FromBody] SaleRequest request)
    {
        var response = await _service.GetSaleListAsync(request);

        return response;
    }

    [HttpPost("add")]
    public async Task<ResponseBase> AddAsync([FromBody] Sale sale)
    {
        var response = await _service.AddSaleAsync(sale);

        return response;
    }


    [HttpPost("addList")]
    public async Task<ResponseBase> AddAsync([FromBody] List<Sale> saleList)
    {
        var response = await _service.AddSaleAsync(saleList);

        return response;
    }
}