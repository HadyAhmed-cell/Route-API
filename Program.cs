using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.DAL.Data;
using Route.DAL.Entities.Identity;
using Route.DAL.Identity;
using RouteApi.Extentions;
using RouteApi.Middlewares;
using StackExchange.Redis;

namespace RouteApi
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
            builder.Services.AddSwaggerDocuentation();

            builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("testConnection")));
            builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


            //Redis

            builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
            {
                var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(connection);
            });


            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            SeedData();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            async void SeedData()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                    try
                    {
                        var context = serviceProvider.GetRequiredService<StoreContext>();
                        await context.Database.MigrateAsync();

                        await StoreContextSeed.SeedAsync(context, loggerFactory);

                        var IdentityContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
                        await IdentityContext.Database.MigrateAsync();

                        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                        await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                    }
                    catch (Exception ex)
                    {

                        var logger = loggerFactory.CreateLogger<Program>();
                        logger.LogError(ex.Message);
                    }
                }
            }
        }
    }
}