using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace HomeMoney.Mvc.Utilities
{
  public static class MvcInit
  {
    private static IConfigurationRoot _configuration;

    /*public static IConfigurationRoot MakeConfigurationByAppSettingsJson()
    {
      if (_configuration == null)
        _configuration = new ConfigurationBuilder()
          .AddJsonFile(SettingsHelper.CommonBaseAppSettingPath, false, true)
          .AddJsonFile(
            Path.Combine(SettingsHelper.CommonAppSettingFolder,
              "appsettings." + (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production") +
              ".json"), true, true).AddJsonFile(SettingsHelper.CommonCustomAppSettingPath, true, true)
          .AddEnvironmentVariables().Build();
      return RunMvc._configuration;
    }*/


    public static IConfigurationRoot GetConfigurationRoot()
    {
      return _configuration;
    }

    public static IMvcBuilder RemovePlainFormatter(this IMvcBuilder mvcBuilder)
    {
      mvcBuilder.AddMvcOptions(options =>
      {
        options.OutputFormatters.RemoveType<StringOutputFormatter>();
        options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
      });
      return mvcBuilder;
    }

    public static IMvcCoreBuilder SetJsonFormatter(
      this IMvcCoreBuilder mvcCoreBuilder)
    {
      /*mvcCoreBuilder.AddJsonOptions(options =>
          options.SerializerSettings.ContractResolver = (IContractResolver) new DefaultContractResolver())
        .AddJsonFormatters();*/
      return mvcCoreBuilder;
    }

    public static IMvcBuilder EnableAuthentication(this IMvcBuilder mvcBuilder)
    {
      mvcBuilder.AddMvcOptions(options =>
        options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder(Array.Empty<string>())
          .RequireAuthenticatedUser().Build())));
      return mvcBuilder;
    }

    public static IMvcBuilder SetAuthorizePage(
      this IMvcBuilder mvcBuilder,
      string authorizeUrl = "/user/Login")
    {
      mvcBuilder.AddRazorPagesOptions(
        options => options.Conventions.AuthorizePage(authorizeUrl));
      return mvcBuilder;
    }
  }
}