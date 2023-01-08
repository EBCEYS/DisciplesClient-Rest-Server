using DataBase.DataBaseAdapters.UsersDataBaseAdapter;
using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ClientDataBaseLibrary.DataBase;
using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter;
using DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter.Interfaces;
using DisciplesClient_Update_Service.JWTExtensions;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.DeleteModsService;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.UpdateModsService;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Collections.Concurrent;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
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
        public static string D2DBConnectionString { get; private set; }
        /// <summary>
        /// The mods base path.
        /// </summary>
        public static string ModsDirBasePath { get; private set; }
        /// <summary>
        /// The remove queue file path. (Store the queue in json).
        /// </summary>
        public static string RemoveQueueFilePath { get; private set; }

        private static ConcurrentDictionary<int, User> Users { get; } = new();
        /// <summary>
        /// The main.
        /// </summary>
        public static void Main()
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            //BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            BasePath = builder.Environment.ContentRootPath;
            builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
            builder.Configuration.SetBasePath(BasePath);
            string pth = Path.Combine(BasePath, "nlog.config");
            Logger logger = NLogBuilder.ConfigureNLog(pth).GetCurrentClassLogger();
            LogManager.Configuration.Variables["logDir"] = BasePath;
            ConfigurationManager config = builder.Configuration;

            logger.Info("Base path is {basePath}", BasePath);

            D2DBConnectionString = config.GetConnectionString("D2DBConnection") ?? throw new Exception($"Can not find connection string 'D2DBConnection'!\nBasePath:{BasePath}");

            builder.WebHost.UseKestrel(opt =>
            {
                opt.ListenAnyIP(5000);
            });

            //Configure the directories.
            ConfigurePaths(config);

            // Add services to the container.
            ConfigureJWTParams(logger, config);

            ConfigureServices(logger, builder);

            builder.Host.UseNLog();
            builder.Logging.AddNLogWeb();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            ConfigureApp(app);
        }

        private static void ConfigurePaths(ConfigurationManager config)
        {
            string removeQueueDirPath = Path.Combine(BasePath, "RemoveQueue");
            RemoveQueueFilePath = Path.Combine(removeQueueDirPath, config.GetValue<string>("RemoveQueueFilePath") ?? throw new Exception("Can not find 'RemoveQueueFilePath'!"));
            if (!Directory.Exists(removeQueueDirPath))
            {
                Directory.CreateDirectory(removeQueueDirPath);
            }
            ModsDirBasePath = Path.Combine(BasePath, "mods");
            if (!Directory.Exists(ModsDirBasePath))
            {
                Directory.CreateDirectory(ModsDirBasePath);
            }
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

            builder.Services.AddScoped<IUsersDataServer, UsersDataServer>();
            builder.Services.AddSingleton<IUsersDBAdapter, UsersDBAdapter>();

            builder.Services.AddScoped<IModsDataServer, ModsDataServer>();

            builder.Services.AddSingleton<IModsFSAdapter, ModsFSAdapter>();
            builder.Services.AddSingleton<IModsDBAdapter, ModsDBAdapter>();

            builder.Services.AddSingleton<ConcurrentQueue<string>>();
            builder.Services.AddHostedService<DeleteModsHostedService>();
            builder.Services.AddHostedService<UpdateModsHostedService>();

            builder.Services.AddSingleton(Users);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }); ;

            builder.Services.AddEndpointsApiExplorer();

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
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new RevokableJwtSecurityTokenHandler(Users));
                options.Validate();
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
            //app.UseStaticFiles();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

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