using Microsoft.Net.Http.Headers;

namespace ETLPipelineTool.Api.Extensions
{
    public static class SecurityHeadersExtension
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(
            this IApplicationBuilder applicationBuilder
        )
        {
            var policy = new HeaderPolicyCollection()
                .AddFrameOptionsSameOrigin()
                .AddXssProtectionBlock()
                .AddContentTypeOptionsNoSniff()
                .AddStrictTransportSecurityMaxAgeIncludeSubDomains()
                .AddReferrerPolicyNoReferrer()
                .RemoveServerHeader()
                .AddCrossOriginOpenerPolicy(x => x.SameOrigin())
                .AddCrossOriginEmbedderPolicy(x => x.RequireCorp())
                .AddContentSecurityPolicy(builder =>
                {
                    builder.AddDefaultSrc().Self();
                    builder.AddScriptSrc().Self().WithNonce();
                    builder.AddStyleSrc().Self();
                    builder.AddImgSrc().Self();
                    builder.AddFontSrc().Self();
                    builder.AddObjectSrc().None();
                    builder.AddFrameAncestors().None();
                });
            applicationBuilder.UseSecurityHeaders(policy);

            return applicationBuilder;
        }

        public static IApplicationBuilder UseCacheControlHeaderMiddleware(
            this IApplicationBuilder applicationBuilder
        )
        {
            applicationBuilder.Use(
                async (context, next) =>
                {
                    context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                    {
                        NoStore = true,
                        NoCache = true,
                    };
                    await next();
                }
            );

            return applicationBuilder;
        }
    }
}
