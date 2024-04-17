using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SmsApp.Configuration;
using SmsApp.Data;
using SmsApp.DTO;
using SmsApp.Repositories;
using SmsApp.Validators;
using SmsApp.Consumers;
using SmsApp.Senders;
using SmsApp.Factories;

namespace SmsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var connString = Environment.GetEnvironmentVariable("Default_Connection");

            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddDbContext<ShortMessagesDbContext>(options => options.UseSqlServer(connString));

            // Adding MassTrasit for RabbitMQ.
            builder.Services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<SmsVendorGRConsumer>();
                cfg.AddConsumer<SmsVendorCYConsumer>();
                cfg.AddConsumer<SmsVendorRestConsumer>();

                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");
                    cfg.ReceiveEndpoint("smsVendorGR", ep =>
                    {
                        ep.ConfigureConsumer<SmsVendorGRConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("smsVendorCY", ep =>
                    {
                        ep.ConfigureConsumer<SmsVendorCYConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("smsVendorRest", ep =>
                    {
                        ep.ConfigureConsumer<SmsVendorRestConsumer>(context);
                    });
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddScoped<ISmsRepository, SmsRepository>();
            builder.Services.AddScoped<IValidator<SmsRequest>, SmsValidator>();

            builder.Services.AddScoped<ISmsSender, SmsVendorCYSender>();
            builder.Services.AddScoped<ISmsSender, SmsVendorGRSender>();
            builder.Services.AddScoped<ISmsSender, SmsVendorRestSender>();
            builder.Services.AddTransient<ISmsSenderFactory, SmsSenderFactory>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MapperConfig));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
