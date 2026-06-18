using Dapper;
using Npgsql;

public class MemberRepository : IMemberRepository
{
    private readonly IConfiguration _configuration;

    public MemberRepository(IConfiguration configuration)
    {
        _configuration = configuration;

    }
    public async Task<int> AddAsync(Member member)
    {
        const string sql = """
            INSERT INTO member (account, password, name)
            VALUES (@account, @password, @name);
        """;

        using var connection = CreateConnection();
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@account", member.Account);
        command.Parameters.AddWithValue("@password", member.Password);
        command.Parameters.AddWithValue("@name", member.Name);
        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected;

    }

    public async Task<Member?> GetMemberByAccountAsync(string account)
    {
        const string sql = """
            SELECT * FROM member 
            WHERE account = @account;
        """;

        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Member>(
            sql,
            new { account = account}
        );
    }

    private NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        return new NpgsqlConnection(connectionString);
    }
}