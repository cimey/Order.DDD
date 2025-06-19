
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Consumer;
using Notification.Consumer.Consumers.Order;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<IEmailSender, EmailSender>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderPlacedEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("order-placed-queue2", e =>
                {
                    e.ConfigureConsumer<OrderPlacedEventConsumer>(context);
                });
            });
        });
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Hello, World!");

// If you have background services, run the host:
await host.RunAsync();

// If you just want to resolve services and exit, you can do:
// await host.StartAsync();
// ... your code ...
// await host.StopAsync();