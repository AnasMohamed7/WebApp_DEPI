
using Microsoft.EntityFrameworkCore;
using WebApp_Depi.core.Interfaces;
using WebApp_Depi.infrastructure.Data;
using WebApp_Depi.infrastructure.Services;


namespace WebApp_Depi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Database Connection
            builder.Services.AddDbContext<WebAppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            // DI
            builder.Services.AddScoped<ICategoryService, CategoryService>();



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
