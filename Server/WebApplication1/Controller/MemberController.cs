using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/member")]
public class MemberController: ControllerBase
{
     private readonly ILogger<MemberController> _logger;
    private readonly MemberService _memberService;

    public MemberController(ILogger<MemberController> logger, MemberService memberService)
    {
        _logger = logger;
        _memberService = memberService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] Member member)
    {
        await _memberService.AddMemberAsync(member);

        return Ok(new { message = "Member added successfully" });
    }

    [HttpPost("login")]
    public async Task<ResponseBase<Member?>> LoginAsync([FromBody] Member member)
    {
       var result = await _memberService.LoginAsync(member);

        return result;
    }
}