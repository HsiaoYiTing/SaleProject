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
    public async Task<ResponseBase> AddAsync([FromBody] Member member)
    {
        var response = await _memberService.AddMemberAsync(member);

        return response;
    }

    [HttpPost("login")]
    public async Task<ResponseBase<Member?>> LoginAsync([FromBody] Member member)
    {
       var result = await _memberService.LoginAsync(member);

        return result;
    }
}