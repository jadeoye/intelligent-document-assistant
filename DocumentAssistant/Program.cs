using Application.Common.Interfaces;
using DocumentAssistant.App;
using DocumentAssistant.App.Interfaces;
using DocumentAssistant.BackgroundTasks;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
{
    var configurationBuilder = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
    services.AddSingleton<IConfiguration>(configurationBuilder);

    services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

    services.AddTransient<IAssistant, Assistant>();
    services.AddSingleton<IDocumentRecognizer, DocumentRecognizer>();

    services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
}).Build();


using(var context = builder.Services.GetService<AppDbContext>())
{
    var assistant = builder.Services.GetService<IAssistant>();

    assistant.Run();
}