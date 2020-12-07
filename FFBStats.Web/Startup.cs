using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Infrastructure;

namespace FFBStats
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.Configure<YahooConfiguration>((IConfiguration)this.Configuration.GetSection("YahooConfiguration"));

            //Add Services for YahooFantasyWrapper Package
            services.AddSingleton<IRequestFactory, RequestFactory>();
            services.AddTransient<IYahooAuthClient, YahooAuthClient>();
            services.AddSingleton<IYahooFantasyClient, YahooFantasyClient>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRouting();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddYahoo(options =>
            {
                options.ClientId = Configuration["YahooConfiguration:ClientId"];
                options.ClientSecret = Configuration["YahooConfiguration:ClientSecret"];
                options.SaveTokens = true;
            })
            .AddCookie(options =>
            {
                //handled by authentication controller, middleware takes over requests to these urls
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            });
        }

        private void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton<IYahooFantasyClient, YahooFantasyClient>();
            services.AddSingleton<IRequestFactory, RequestFactory>();
            services.AddTransient<IYahooAuthClient, YahooAuthClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHsts();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseForwardedHeaders();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
