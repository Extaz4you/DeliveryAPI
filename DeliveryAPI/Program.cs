
using DeliveryAPI.Data;
using DeliveryAPI.Interfaces;
using DeliveryAPI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DeliveryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
            try
            {
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                //builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DeliveryAPI", Version = "v1" }));
                builder.Services.AddSwaggerGen();
                builder.Services.AddDbContext<DeliveryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryDb")));
                builder.Services.AddScoped<IDeliveryService, DeliveryService>();
                var app = builder.Build();

                app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeliveryAPI_v1");
                //    c.RoutePrefix = string.Empty;
                //});
                app.UseSwaggerUI();

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                Log.Information("Запуск приложения");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Ошибка при запуске приложения");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
