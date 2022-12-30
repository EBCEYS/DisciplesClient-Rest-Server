using DataBase.DataBaseAdapters.UsersDataBaseAdapter;
using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ClientDataBaseLibrary.DataBase;
using DisciplesClient_Update_Service.LogicLayer;
using DisciplesClient_Update_Service.LogicLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace DisciplesClient_Update_Service
{
    //Я бы не хотел сюда добавлять лишние комментарии, но visual studio ругается :(
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The program base path.
        /// </summary>
        public static string BasePath { get; private set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// The service secret key.
        /// </summary>
        public static byte[]? SecretKey { get; set; }
        /// <summary>
        /// The JWT issuer.
        /// </summary>
        public static string Issuer => issuer;
        private static string issuer;
        /// <summary>
        /// The JWT audience.
        /// </summary>
        public static string Audience => audience;
        private static string audience;
        /// <summary>
        /// The users data base connection string.
        /// </summary>
        public static string UsersDBConnectionString { get; private set; }
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
            string pth = Path.Combine(BasePath, "nlog.config");
            Logger logger = NLogBuilder.ConfigureNLog(pth).GetCurrentClassLogger();
            LogManager.Configuration.Variables["logDir"] = BasePath;

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigurationManager config = builder.Configuration;

            UsersDBConnectionString = config.GetConnectionString("UsersDBConnection") ?? throw new Exception("Can not find connection string 'UsersDBConnection'!");

            // Add services to the container.
            ConfigureJWTParams(logger, config);

            ConfigureServices(logger, builder);

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            ConfigureApp(app);
        }

        private static void ConfigureJWTParams(Logger logger, ConfigurationManager config)
        {
            issuer = config["JwtAuth:Issuer"] ?? throw new Exception("Can not load jwt issuer!");
            audience = config["JwtAuth:Audience"] ?? throw new Exception("Can not load jwt audience!");
            SecretKey = GetSecretKey(Path.Combine(BasePath, "secret.key")) ?? throw new FileLoadException("Can not load secret key!");
            logger.Info("Secret key is loaded successfully! Secret key length is {length}", SecretKey.Length);
        }

        private static byte[] GetSecretKey(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Secret key at path {path} is not found!");
            }
            return File.ReadAllBytes(path);
        }

        private static void ConfigureServices(Logger logger, WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<Disciples2ClientDBConnext>();
            builder.Services.AddSingleton(logger);
            builder.Services.AddSingleton<IDataServer, DataServer>();
            builder.Services.AddSingleton<IUsersDBAdapter, UsersDBAdapter>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }); ;

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthorization(opt => 
            {
                opt.AddPolicy("IsAdmin", p => { p.RequireRole("adm", "administrator", "admin", "user", "usr"); });
                opt.AddPolicy("IsUser", p => { p.RequireRole("user", "usr"); });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.Services.AddCors();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Disciples2 client WEB API", Version = "v1" });
                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        }
        private static void ConfigureApp(WebApplication app)
        {
            app.UseStaticFiles();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}