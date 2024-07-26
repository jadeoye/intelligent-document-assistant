using Application.BackgroundTasks;
using Application.Interfaces;
using DocumentAssistant.App;
using DocumentAssistant.App.Interfaces;
using DocumentAssistant.Interfaces;
using DocumentAssistant.Listeners;
using DocumentAssistant.Utilities.Listeners;
using DocumentAssistant.Utilities.Searchs;
using Infrastructure.Configurations.Utilities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configurationBuilder = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        services.AddSingleton<IConfiguration>(configurationBuilder);

        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddSingleton(new ReadOnlyDbConfiguration(connectionString));

        services.AddTransient<IAssistant, Assistant>();
        services.AddSingleton<IDocumentRecognizer, DocumentRecognizer>();
        services.AddSingleton<IIntentRecognizer, IntentRecognizer>();
        services.AddSingleton<ISpeechRecognizer, SpeechRecognizer>();
        services.AddSingleton<IDocumentFinder, DocumentFinder>();
        services.AddSingleton<ISearcher, Searcher>();
        services.AddSingleton<IKeyboardListener, KeyboardListener>();
        services.AddSingleton<IMicrophoneListener, MicrophoneListener>();

        services.AddTransient<IReadOnlyAppDbContext, ReadOnlyAppDbContext>();
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
    }).Build();


using(var context = builder.Services.GetService<AppDbContext>())
{
    var assistant = builder.Services.GetService<IAssistant>();

    assistant.Run();
}