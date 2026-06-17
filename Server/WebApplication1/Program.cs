
var PolicyName = "VuePolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<MemberService>();


builder.Services.AddCors(options =>
{
   options.AddPolicy(PolicyName, policy =>
   {
       policy
       .WithOrigins("http://localhost:5173")
       .AllowAnyHeader()
       .AllowAnyMethod();
    }); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors(PolicyName);
app.MapControllers();
app.Run();
