using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Serilog;

namespace Movie.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add configuration Dbcontext
            builder.Services.AddDbContext<MovieDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieAppDb")));

            //Add serilog to the container
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Logging.AddSerilog();
            //builder.Logging.ClearProviders().AddConsole().AddDebug();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAuthenticationConfiguration(builder.Configuration);
            builder.Services.AddServices();


            // Cors Configuration
            var corsname = "MoviesCORS";
            builder.Services.AddCors(options => options.AddPolicy(corsname, policy =>
            {
                // Allow all origins
                policy.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();

            }));

            builder.Services.AddIdentity<User, Role>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = true;
            })
              .AddEntityFrameworkStores<MovieDbContext>()
              .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;

                // Thiết lập về đăng nhập
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Thiết lập về user
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            builder.Services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            //Add SendEmail
            var sendmail = builder.Configuration.GetSection("SendEmail");
            builder.Services.Configure<SendEmail>(sendmail);
            builder.Services.AddSingleton<ISendEmail, SendEmailServices>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(corsname);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Content/Images")),
                RequestPath = "/Content/Images"
            });
            app.UseAuthorization();

            //app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                //enpoints.MapGet("/", () => "Hello World!");
                endpoints?.MapGet("api/testEndpoints",
                    context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWT:Secret")!));
            });

            app.MapControllers();

            app.Run();
        }
    }
}
