using ETLPipelineTool.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder
    .Services
    //.AddInfrastructureServices(config)
    .AddCorsPolicy()
    .AddEndpointsApiExplorer()
    .AddApiVersioningSupport()
    .AddApiDocumentSupport()
    .AddApplicationHealthChecks()
    .AddApplicationServices();

//.AddAppDbContext(config);

var app = builder.Build();

app.UseApplicationMiddlewares(app.Environment);
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.MapControllers();
app.MapApplicationHealthChecks();

await app.RunAsync(new CancellationToken());
