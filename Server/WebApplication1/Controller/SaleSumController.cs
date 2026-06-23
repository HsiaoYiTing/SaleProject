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


    [HttpPost("export")]
    public async Task<IActionResult> Export([FromBody] SaleSumRequest request)
    {
        var bytes = await _service.ExportExcel(request);

        if (bytes == null)
        {
            return BadRequest("查無相關資料");
        }

        return File(bytes,  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Sale_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

    }
}