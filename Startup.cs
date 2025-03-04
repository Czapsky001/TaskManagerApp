
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TaskManagerApp.AutoMapper;
using TaskManagerApp.DatabaseConnector;
using TaskManagerApp.Model;
using TaskManagerApp.Repository;
using TaskManagerApp.Services.AuthenticationService;
using TaskManagerApp.Services.Task;
using TaskManagerApp.Services.TokenService;


namespace TaskManagerApp;
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddControllers();
        services.AddCors();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(AutoMapperProfile));

        AddSwagger(services);

        AddAuthentication(services);
        AddIdentity(services);
        var connectionString = Configuration["ConnectionString"];
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(o => o
            .SetIsOriginAllowed(origin => true)
            //.AllowCredentials()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        AddRoles(app);
    }

    private void AddAuthentication(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var validIssuer = config["Authentication:ValidIssuer"];
        var validAudience = config["Authentication:ValidAudience"];
        var issuerSigningKey = Configuration["Authentication:IssuerSigningKey"];

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(issuerSigningKey)
                    )
                };
            });
    }

    private void AddIdentity(IServiceCollection services)
    {
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UserContext>();
    }

    private void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Warehouse",
                Version = "v1"
            });

            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    }, new string[]{ }
                }
        });
        });
    }

    void AddRoles(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var tAdmin = CreateRole(roleManager, "Admin");
        tAdmin.Wait();

        var tEmployee = CreateRole(roleManager, "Employee");
        tEmployee.Wait();


    }

    private async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var adminRoleExists = await roleManager.RoleExistsAsync(roleName);
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}