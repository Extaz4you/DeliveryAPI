
using DeliveryAPI.Data;
using DeliveryAPI.Interfaces;
using DeliveryAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c=>c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DeliveryAPI", Version = "v1"}));
            builder.Services.AddDbContext<DeliveryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(ApiConstant.ConnectionString)));
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeliveryAPI_v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
