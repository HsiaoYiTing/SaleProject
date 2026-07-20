
using OfficeOpenXml;

var PolicyName = "VuePolicy";
var builder = WebApplication.CreateBuilder(args);

ExcelPackage.License.SetNonCommercialPersonal("HsiaoYiTing");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
// builder.Services.AddScoped<ISaleSumRepository, SaleSumRepository>();
builder.Services.AddScoped<ISaleSumRepository, PSaleSumRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<SaleSumService>();
builder.Services.AddScoped<SaleService>();


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
