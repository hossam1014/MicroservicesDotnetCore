using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);


builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();



builder.Services.AddOcelot().AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
