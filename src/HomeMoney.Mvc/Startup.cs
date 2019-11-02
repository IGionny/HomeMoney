using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HomeMoney.Core.Domain;
using HomeMoney.Core.Services;
using HomeMoney.Mvc.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HomeMoney.Mvc
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews(config =>
        {
          var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
          config.Filters.Add(new AuthorizeFilter(policy));
        })
        .AddJsonOptions(options =>
          {
            //This reset the PropertyNamingPolicy: in this way it will respect the name of the properties..
            //The pre-set policy is CamelCase: in this way a property 'Name' became 'name'
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
          }
        );
      /*.RemovePlainFormatter()
      .SetAuthorizePage();*/
      //  .SetJsonFormatter();


      services.AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
          options =>
          {
            options.LoginPath = new PathString("/user/login");
            options.LogoutPath = new PathString("/user/logout");
            options.AccessDeniedPath = new PathString("/user/denied");
          })
        .AddGoogle(googleOptions =>
        {
          var googleAuthNSection = Configuration.GetSection("GoogleAuthentication");
          googleOptions.ClientId = googleAuthNSection["ClientId"];
          googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
        });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo() {Title = "HomeMoney Web API", Version = "v1"});
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
      
      //Add IoC configuration:

      //For now is SINGLETON to maintain in memory data
      services.AddSingleton<AccountCrudService, AccountCrudService>();
      services.AddSingleton<AccountBalanceCrudService, AccountBalanceCrudService>();
      services.AddSingleton<CategoryCrudService, CategoryCrudService>();
      services.AddSingleton<TransactionCrudService, TransactionCrudService>();
      services.AddSingleton<UserTag, UserTag>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //https://medium.com/@nicolastakashi/asp-net-core-api-behind-the-nginx-reverse-proxy-with-docker-72eeccfb5063
      //this: route incoming headers through the request that arrived at Nginx for our ASP.NET service.
      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });


      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        // https://github.com/aspnet/AspNetCore/issues/12890
        // Webpack initialization with hot-reload.
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true,
          HotModuleReplacementClientOptions = new Dictionary<string, string>
          {
            {"reload", "true"}, {"aggregateTimeout", "300"}, {"poll", "1000"}
          }
        });

        app.UseStaticFiles(new StaticFileOptions
        {
          FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
          RequestPath = "/node_modules"
        });
      }
      else
      {
        app.UseExceptionHandler("/Error/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      app.UseStaticFiles();

      app.UseRouting();

      app.AddSecurityCountermeasures();

      app.UseCookiePolicy();
      //404
      app.UsePageNotFound();

      app.AddSwagger();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseMvcWithDefaultAreaRoutes();
    }
  }
}