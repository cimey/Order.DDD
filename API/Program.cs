using MassTransit;
using Microsoft.EntityFrameworkCore;
using Test.Application.CommandsAndQueries.Orders.Commands;
using Test.Application.Interfaces;
using Test.Application.Services;
using Test.Domain.DomainEvents;
using Test.Domain.Interfaces;
using Test.Infrastructure.Data;
using Test.Infrastructure.EventHandlers.InternalEvents.Order;
using Test.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(OrderPlacedEvent).Assembly, // Domain
        typeof(OrderPlacedEventHandler).Assembly, // Infrastructure,
        typeof(CancelOrderCommand).Assembly // Applicatiton,
    );
});

builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingRabbitMq((context, config) =>
    {
        config.Host(
            builder.Configuration.GetValue<string>("RabbitMQ:HostName"),"/",
            h =>
            {
                h.Username(builder.Configuration.GetValue<string>("RabbitMQ:UserName")!);
                h.Password(builder.Configuration.GetValue<string>("RabbitMQ:Password")!);
            });

        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

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
