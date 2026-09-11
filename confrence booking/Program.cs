using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Add this for OpenApiOperation etc.
using Swashbuckle.AspNetCore.SwaggerGen; // Add this for SwaggerGenOptions and IOperationFilter

namespace confrence_booking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
           
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    builder => builder
                        .WithOrigins("https://mnu-research.vercel.app", "http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // قبل app.UseAuthorization()
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
            app.UseStaticFiles(); // لتشغيل خدمة الملفات في مجلد wwwroot
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
