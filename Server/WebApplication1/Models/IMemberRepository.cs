

public interface IMemberRepository
{
    Task<Member?> LogInAsync(Member member);

    Task<int> AddAsync(Member member);
}