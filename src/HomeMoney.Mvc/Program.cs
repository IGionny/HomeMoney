using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HomeMoney.Mvc
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IConfigurationRoot MakeConfigurationByAppSettingsJson()
    {
      
      return new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "Configurations"))
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile(
          $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
          true, true)
        .AddJsonFile("appsettings.custom.json", true, true)
        .AddEnvironmentVariables()
        .Build();
    }


    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseConfiguration(MakeConfigurationByAppSettingsJson())
        .ConfigureKestrel((context, options) =>
        {
          options.AddServerHeader = false;
          /*
           * To permit upload of BIG file you have to edit also the web.config:<requestLimits maxAllowedContentLength="1073741824" />
           * tnx to: https://stackoverflow.com/questions/38698350/increase-upload-file-size-in-asp-net-core
           *
           */

          options.Limits.MaxRequestBodySize = null;
        })
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog();
  }
}