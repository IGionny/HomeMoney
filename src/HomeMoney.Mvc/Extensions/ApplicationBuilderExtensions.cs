using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HomeMoney.Mvc.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static void AddSecurityCountermeasures(this IApplicationBuilder app)
    {
      //https://github.com/damienbod/AspNetCoreHybridFlowWithApi/blob/master/WebMVCClient/Startup.cs
      //Registered before static files to always set header
      app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
      app.UseXContentTypeOptions();
      app.UseReferrerPolicy(opts => opts.NoReferrer());
      app.UseXXssProtection(options => options.EnabledWithBlockMode());
      app.UseXfo(options => options.Deny());

      app.UseCsp(opts => opts
          .BlockAllMixedContent()
          // .StyleSources(s => s.Self())
          // .StyleSources(s => s.UnsafeInline())
          //.FontSources(s => s.Self())
          .FormActions(s => s.Self())
          .FrameAncestors(s => s.Self())
        //.ImageSources(s => s.Self())
        //  .ScriptSources(s => s.Self())
      );
    }

    public static void AddSwagger(this IApplicationBuilder app)
    {
      //Enable swagger
      app.UseSwagger(c =>
        //RouteTemplate cannot start with a '/' !
        c.RouteTemplate = "api-swagger/{documentName}/swagger.json");

      //Enable Swagger UI
      app.UseSwaggerUI(c =>
      {
        c.RoutePrefix = "api-swagger";
        c.SwaggerEndpoint("/api-swagger/v1/swagger.json", "Ngs.Web API V1");
      });
    }
    
    public static void UsePageNotFound(this IApplicationBuilder application, string notFoundUrl = "/Error/PageNotFound")
    {
      application.UseStatusCodePages(async context =>
      {
        if (context.HttpContext.Response.StatusCode != 404 ||
            (HttpRequestExtensions.IsAjaxRequest(context.HttpContext.Request)
              ? 1
              : (HttpRequestExtensions.IsApiRequest(context.HttpContext.Request) ? 1 : 0)) != 0 ||
            context.HttpContext.Request.IsStaticContentRequest())
          return;
        var originalPath = context.HttpContext.Request.Path;
        var originalQueryString = context.HttpContext.Request.QueryString;
        context.HttpContext.Features.Set((IStatusCodeReExecuteFeature) new StatusCodeReExecuteFeature()
        {
          OriginalPathBase = context.HttpContext.Request.PathBase.Value,
          OriginalPath = originalPath.Value,
          OriginalQueryString = (originalQueryString.HasValue ? originalQueryString.Value : (string) null)
        });
        context.HttpContext.Request.Path = (PathString) notFoundUrl;
        context.HttpContext.Request.QueryString = QueryString.Empty;
        try
        {
          await context.Next(context.HttpContext).ConfigureAwait(false);
        }
        finally
        {
          context.HttpContext.Request.QueryString = originalQueryString;
          context.HttpContext.Request.Path = originalPath;
          context.HttpContext.Features.Set((IStatusCodeReExecuteFeature) null);
        }
      });
    }

    public static void UseMvcWithDefaultAreaRoutes(this IApplicationBuilder application)
    {
      application.UseMvc((Action<IRouteBuilder>) (routes =>
      {
        routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
      }));
    }
  }
}