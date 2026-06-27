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
    public async Task<ResponseBase<List<SaleSum>?>> FindByConditionsAsync([FromBody] RequestBase request)
    {
        var response = await _service.GetSaleSumListAsync(request);

        return response;
    }

    [HttpPost("summary")]
    public async Task<ResponseBase> SummaryAsync([FromBody] RequestBase request)
    {
        return await _service.Summary(request);
    }

    [HttpPost("export")]
    public async Task<IActionResult> ExportAsync([FromBody] RequestBase request)
    {
        var bytes = await _service.ExportExcel(request);

        if (bytes == null)
        {
            return BadRequest("查無相關資料");
        }

        return File(bytes,  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Sale_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
    }
}