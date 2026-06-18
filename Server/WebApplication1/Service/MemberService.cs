using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

public class MemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();


    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }


    public async Task<ResponseBase<Member?>> LoginAsync(Member member)
    {
        if (string.IsNullOrWhiteSpace(member.Account))
        {
            return ResponseFactory.CreateErrorResponse<Member?>(null, "帳號不得為空");
        }
        if (string.IsNullOrWhiteSpace(member.Password))
        {
            return ResponseFactory.CreateErrorResponse<Member?>(null, "密碼不得為空");
        }

        var dbMember = await _memberRepository.GetMemberByAccountAsync(member.Account);
        bool isSuccess = false;

        if (dbMember != null)
        {
             var result = _passwordHasher.VerifyHashedPassword(null, dbMember.Password, member.Password);
             isSuccess = result == PasswordVerificationResult.Success;
        }

        if (isSuccess) 
        {
            return ResponseFactory.CreateSuccessResponse<Member?>(dbMember, "登入成功");
        } 
        else
        {
            return ResponseFactory.CreateErrorResponse<Member?>(null, "帳號/密碼錯誤");
        }
    }

    public async Task<ResponseBase> AddMemberAsync(Member member)
    {
        if (string.IsNullOrWhiteSpace(member.Account))
        {
            return ResponseFactory.CreateErrorResponse("帳號不得為空");
        }
        if (string.IsNullOrWhiteSpace(member.Password))
        {
            return ResponseFactory.CreateErrorResponse("密碼不得為空");
        }
        if (string.IsNullOrWhiteSpace(member.Name))
        {
            return ResponseFactory.CreateErrorResponse("姓名不得為空");
        }


        string hashPassword = _passwordHasher.HashPassword(null, member.Password);
        member.Password = hashPassword;

        var result = await _memberRepository.AddAsync(member);

        return ResponseFactory.CreateSuccessResponse(result > 0 ? "新增成功!" : "新增失敗!");
    }
}