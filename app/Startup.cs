
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TaskManagerApp.AutoMapper;
using TaskManagerApp.Middleware;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Validator;
using TaskManagerApp.Repository.Companies;
using TaskManagerApp.Repository.SubTasksRepo;
using TaskManagerApp.Repository.Tabs;
using TaskManagerApp.Repository.Task;
using TaskManagerApp.Repository.WorkTable;
using TaskManagerApp.Repository.WorkTables;
using TaskManagerApp.Services.AuthenticationService;
using TaskManagerApp.Services.Companies;
using TaskManagerApp.Services.SubTasks;
using TaskManagerApp.Services.Tabs;
using TaskManagerApp.Services.Task;
using TaskManagerApp.Services.TokenService;
using TaskManagerApp.Services.Users;
using TaskManagerApp.Services.WorkTables;


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
        // Konfiguracja serializacji JSON, żeby ignorować cykliczne referencje
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<UpdateUserDTOValidator>();

        services.AddCors();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(AutoMapperProfile));

        AddSwagger(services);
        AddAuthentication(services);
        AddIdentity(services);

        var connectionString = Configuration["ConnectionString"];
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ISubTaskRepo, SubTaskRepo>();
        services.AddScoped<ISubTaskService, SubTaskService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkTableRepository, WorkTableRepository>();
        services.AddScoped<IWorkTableService, WorkTableService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITabRepository, TabRepository>();
        services.AddScoped<ITabService,  TabService>();
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
        app.UseGlobalExceptionHandling();
        AddRoles(app);
    }

    private void AddAuthentication(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var validIssuer = config["Authentication:ValidIssuer"];
        var validAudience = config["Authentication:ValidAudience"];
        var issuerSigningKey = config["Authentication:IssuerSigningKey"]; 

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
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
            .AddEntityFrameworkStores<DatabaseContext>();
    }

    private void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskManagerApp",
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
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            option.IncludeXmlComments(xmlPath);
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