var builder = WebApplication.CreateBuilder(args);

// KestrelServerOptions
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

EnvironmentsHelper.SetupSerilog(builder);

builder.Logging.AddSerilogService();
builder.Services.AddAntiforgerySupport().AddControllers();
builder
    .Services.AddProblemDetails()
    //.AddInfrastructureServices(config)
    .AddCorsPolicy()
    .AddEndpointsApiExplorer()
    .AddApiVersioningSupport()
    .AddApiDocumentSupport()
    .AddApplicationHealthChecks()
    .AddApplicationServices()
    .AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

//.AddAppDbContext(config);

var app = builder.Build();

app.UseApplicationMiddlewares(app.Environment);
app.UseSerilogRequestLogging();

app.MapControllers();
app.MapApplicationHealthChecks();

await app.RunAsync(new CancellationToken());
