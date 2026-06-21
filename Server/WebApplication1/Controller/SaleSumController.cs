using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sum")]

public class SaleSumController: ControllerBase
{
    private readonly ILogger<SaleSumController> _logger;
    private readonly SaleSumService _service;

    public SaleSumController(ILogger<SaleSumController> logger, SaleSumService service)
    {
        _logger = logger;
        _service = service;
    }


    [HttpPost("findbyconditions")]
    public async Task<ResponseBase<List<SaleSum>?>> FindByConditions([FromBody] SaleSumRequest request)
    {
        var response = await _service.GetSaleSumListAsync(request);

        return response;
    }
}