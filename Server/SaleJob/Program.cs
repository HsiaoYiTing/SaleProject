using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);

// Repository 註冊
services.AddScoped<ISaleSumRepository, SaleSumRepository>();
services.AddScoped<ISaleRepository, SaleRepository>();

// Service 註冊
services.AddScoped<SaleSumService>();

var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

var saleSumService = scope.ServiceProvider.GetRequiredService<SaleSumService>();

var request = new RequestBase
{
    StoreId = "123331",
    //Date = DateOnly.Parse("2026-05-01")
    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1))
};


Console.WriteLine("排程開始！！！");

var result = await saleSumService.Summary(request);

Console.WriteLine("排程結束！！！" + result.Message);