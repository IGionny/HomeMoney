using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HomeMoney.Mvc
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var configuration = MakeConfigurationByAppSettingsJson();
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        //.Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

      CreateWebHostBuilder(args, configuration).Build().Run();
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


    public static IHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot configuration) =>
      Host.CreateDefaultBuilder(args)
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseKestrel(options =>
            {
              // Set properties and call methods on options
              options.AddServerHeader = false;
              options.Limits.MaxRequestBodySize = null;
            })
            .UseConfiguration(configuration)
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseSerilog();
        });
  }
}