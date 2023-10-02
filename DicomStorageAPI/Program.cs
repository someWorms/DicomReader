using DicomStorageAPI.Database.Interfaces;
using DicomStorageAPI.Database;
using DicomStorageAPI.EventBusConsumer;
using EventBus.Messaging.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using DicomStorageAPI.Services.Interfaces;
using DicomStorageAPI.Services;

namespace DicomStorageAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("DatabaseConnection");
            var host = builder.Configuration["EventBus:Host"];

            // Add services to the container.

            //Configure MassTransit + RabbitMQ
            builder.Services.AddMassTransit(conf =>
            {
                conf.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(host);
                    cfg.ReceiveEndpoint(EventBusConstants.DataQueue, c =>
                    {
                        c.ConfigureConsumer<DataConsumer>(ctx);
                    });
                });
                conf.AddConsumer<DataConsumer>();
            });
            //builder.Services.AddScoped<DataConsumer>();
            builder.Services.AddTransient<IDataService, DataService>();

            //Configure DB
            builder.Services.AddDbContext<IDatabaseContext, DatabaseContext>(opt => opt.UseSqlServer(connection));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}