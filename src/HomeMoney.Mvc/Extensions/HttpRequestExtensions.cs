using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace HomeMoney.Mvc.Extensions
{
  public static class HttpRequestExtensions
  {
    public static bool IsAjaxRequest(HttpRequest request)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      return request.Headers["x-requested-with"] == "XMLHttpRequest";
    }

    public static bool IsApiRequest(HttpRequest request)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      return HttpRequestExtensions.IsApiRequest(request.Path);
    }

    public static bool IsApiRequest(PathString path)
    {
      if (path == (PathString) ((string) null))
        throw new ArgumentNullException(nameof (path));
      return path.StartsWithSegments(new PathString("/api"));
    }

    public static bool IsStaticContentRequest(this HttpRequest request)
    {
      string contentType;
      return new FileExtensionContentTypeProvider().TryGetContentType((string) request.Path, out contentType);
    }
  }
}