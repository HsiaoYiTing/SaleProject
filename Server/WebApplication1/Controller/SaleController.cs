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
    public async Task<ResponseBase<List<Sale>?>> FindByConditionsAsync([FromBody] RequestBase request)
    {
        var response = await _service.GetSaleListAsync(request);

        return response;
    }

    [HttpPost("add")]
    public async Task<ResponseBase> AddAsync([FromBody] List<AddSaleRequest> request)
    {
        var response = await _service.AddSaleAsync(request);

        return response;
    }

   [HttpPost("import")]
    public async Task<ResponseBase> Import([FromForm] FileRequest request)
    {
        return await _service.ImportAsync(request);
    }
}