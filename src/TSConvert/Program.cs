using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TSConvert.Services;
using TSConvert.Services.Interfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IResponseService, ResponseService>();
    })
    .Build();

host.Run();
