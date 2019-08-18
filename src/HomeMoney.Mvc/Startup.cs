﻿using System;
using System.IO;
using System.Reflection;
using HomeMoney.Mvc.Extensions;
using HomeMoney.Mvc.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Swagger;

namespace HomeMoney.Mvc
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<FormOptions>(x => { x.MultipartBodyLengthLimit = int.MaxValue; });

      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddMvcCore()
        .SetJsonFormatter();
      services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
        //.EnableAuthentication()
        .RemovePlainFormatter()
        .SetAuthorizePage();

      //.AddFluentValidation();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info {Title = "HomeMoney Web API", Version = "v1"});
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

        // Needed by VueJS Reporting
        // Webpack initialization with hot-reload.
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true
        });

        //Needed by VueJS reporting to serve local VueJs un-minified version
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
        app.UseHsts();
      }

      app.AddSecurityCountermeasures();
      app.UseHttpsRedirection();
      app.UseCookiePolicy();
      //404
      app.UsePageNotFound();
      //Add support for Swagger
      app.AddSwagger();

      app.UseMvcWithDefaultAreaRoutes();
    }
  }
}