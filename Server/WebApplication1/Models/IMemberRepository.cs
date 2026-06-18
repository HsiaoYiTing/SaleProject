

public interface IMemberRepository
{
    Task<Member?> GetMemberByAccountAsync(string account);

    Task<int> AddAsync(Member member);
}