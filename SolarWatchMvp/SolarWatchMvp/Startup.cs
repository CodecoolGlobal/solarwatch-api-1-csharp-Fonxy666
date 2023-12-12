using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolarWatchMvp.Controllers;
using SolarWatchMvp.Data;
using SolarWatchMvp.Repository;
using SolarWatchMvp.Services.Authentication;

namespace SolarWatchMvp;

public class Startup
{
    private readonly IConfiguration _configuration;
    
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        services.AddSingleton<IWeatherDataProvider, OpenWeatherMapApi>();
        services.AddSingleton<IJsonProcessor, JsonProcessor>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ISunTimeRepository, SunTimeRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        
        var connection = _configuration["ConnectionString"];
        /*var connection = Environment.GetEnvironmentVariable("CONNECTION_STRING");*/
        var iS = _configuration["IssueSign"];
        /*var iS = Environment.GetEnvironmentVariable("SOMETHING_SECRET");*/
        var iA = _configuration["IssueAudience"];
        /*var iA = Environment.GetEnvironmentVariable("ISSUE_AUDIANCE");*/
        
        services.AddDbContext<WeatherApiContext>(options => options.UseSqlServer(connection));
        services.AddDbContext<UsersContext>(options => options.UseSqlServer(connection));
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                if (iS != null)
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = iA,
                        ValidAudience = iA,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iS))
                    };
            });
        
        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UsersContext>();
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, WeatherApiContext dbContext)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 200;
            }
            else
            {
                await next();
            }
        });

        app.UseAuthentication();
        app.UseAuthorization();
        
        if (env.IsEnvironment("Test"))
        {
            await dbContext.Database.EnsureCreatedAsync();
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        await AddRolesAndAdmin(app);
    }

    async Task AddRolesAndAdmin(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var roleList = new List<string> { "User", "Admin" };
        
        foreach (var s in roleList)
        {
            await roleManager.CreateAsync(new IdentityRole(s));
        }

        await CreateAdminIfNotExist(userManager);
    }

    private async Task CreateAdminIfNotExist(UserManager<IdentityUser> userManager)
    {
        var adminInDb = await userManager.FindByEmailAsync("adminGod@windowslive.com");
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = "Admin", Email = "adminGod@windowslive.com" };
            var adminCreated = await userManager.CreateAsync(admin, "asdASDasd123666");

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}