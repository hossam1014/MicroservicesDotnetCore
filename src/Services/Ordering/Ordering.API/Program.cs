using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.API.Mapping;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);



// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();

    config.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        config.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, x => {
            x.ConfigureConsumer<BasketCheckoutConsumer>(context);
        });
    });
});



// General configurations
builder.Services.AddAutoMapper(typeof(OrderingProfile).Assembly);

builder.Services.AddScoped<BasketCheckoutConsumer>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migrations
app.MigrateDatabase<OrderContext>(
    (context, services) => {
        var logger = services.GetService<ILogger<OrderContextSeed>>();
        OrderContextSeed.SeedAsync(context, logger).Wait();
    }
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
