namespace ETLPipelineTool.Api.Extensions
{
  public static class AntiforgeryExtension
  {
    public static IServiceCollection AddAntiforgerySupport(this IServiceCollection services)
    {
      services.AddAntiforgery(options =>
      {
        options.HeaderName = Auth.ANTIFORGERY_TOKEN_HEADER;
        options.SuppressXFrameOptionsHeader = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.HttpOnly = false;
        options.Cookie.SameSite = SameSiteMode.Strict;
      });

      return services;
    }
  }
}
