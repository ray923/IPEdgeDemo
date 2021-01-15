using System.Linq;
using IPedgeProject.Data.AccessData;
using IPedgeProject.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace IPedgeProject
{
  public class Startup
  {
    private readonly IHostEnvironment _env;
    public IConfiguration Configuration { get; }
    public Startup(IHostEnvironment env)
    {
      // system settings by environmnet
      Configuration = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .Build();
        _env = env;
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // add http and https response compression and add svg type support
      services.AddResponseCompression(options =>
      {
        options.Providers.Add<GzipCompressionProvider>();
        options.Providers.Add<BrotliCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
        options.EnableForHttps = true;
      });
      // about HSTS https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio#http-strict-transport-security-protocol-hsts
      services.AddHsts(options =>
      {
        options.IncludeSubDomains = true;
        options.MaxAge = System.TimeSpan.FromDays(365);
      });

      // JsonResult settings
      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
      });

      // JsonResult settings
      //   services.AddMvc()
      //   .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
      //   .AddJsonOptions(options => {options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };});
      
      // cors policy
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
      });
      // Dbconnection
      string projectConnectString = Configuration.GetConnectionString("IPEdgeDataBase");
      //services.Configure<DbConfig>(config => config.ConnectionString = projectConnectString);

      services.AddDbContextPool<ProjectContext>(options =>
      {
        options.UseSqlServer(projectConnectString);
      });

      services.AddHttpContextAccessor();
      
      if (!_env.IsDevelopment())
			{
				services.AddHttpsRedirection(options =>
				{
					options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
					options.HttpsPort = 443;
				});
			}

      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
          configuration.RootPath = "ClientApp/build";
      });

      // TO DO add some third party useful function
      // services.Configure<ApiSettings>(Configuration);
      // services.Configure<AttachmentSetting>(options => Configuration.GetSection("AttachmentSetting").Bind(options));
      // services.Configure<EmailConfig>(options => Configuration.GetSection("Email").Bind(options));
      // services.Configure<EmailSetting>(options => Configuration.GetSection("EmailSetting").Bind(options));
      // services.Configure<GmailApiSetting>(options => Configuration.GetSection("GmailApiSetting").Bind(options));

      services.AddTransient<IEmpolyeeService, EmpolyeeService>().AddSingleton<ProjectConnection>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSpaStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                spa.UseReactDevelopmentServer(npmScript: "start");
            }
        });
    }
  }
}
