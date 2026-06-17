using Microsoft.AspNetCore.Identity.Data;

public class MemberService
{
    private readonly IMemberRepository _memberRepository;


    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }


    public async Task<ResponseBase<Member?>> LoginAsync(Member member)
    {
        if (member.Account == "")
        {
            return ResponseFactory.CreateErrorResponse<Member?>(null, "帳號不得為空");
        }
        if (member.Password == "")
        {
            return ResponseFactory.CreateErrorResponse<Member?>(null, "密碼不得為空");
        }

        var loginMember = await _memberRepository.LogInAsync(member);
        if (loginMember == null)
        {
            return ResponseFactory.CreateErrorResponse<Member?>(null, "帳號/密碼錯誤");
        }

        return ResponseFactory.CreateSuccessResponse<Member?>(loginMember, "登入成功");
    }

    public async Task<ResponseBase> AddMemberAsync(Member member)
    {
        if (member.Account == "")
        {
            return ResponseFactory.CreateErrorResponse("帳號不得為空");
        }
        if (member.Password == "")
        {
            return ResponseFactory.CreateErrorResponse("密碼不得為空");
        }
        if (member.Name == "")
        {
            return ResponseFactory.CreateErrorResponse("姓名不得為空");
        }

        var result = await _memberRepository.AddAsync(member);

        return ResponseFactory.CreateSuccessResponse(result > 0 ? "新增成功!" : "新增失敗!");
    }
}