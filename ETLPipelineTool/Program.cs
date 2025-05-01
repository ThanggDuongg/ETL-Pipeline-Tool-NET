var builder = WebApplication.CreateBuilder(args);

// KestrelServerOptions
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

EnvironmentsHelper.SetupSerilog(builder);

builder.Logging.AddSerilogService();
builder.Services.AddAntiforgerySupport().AddControllers();
builder
    .Services.AddAppSettingsConfiguration(builder.Configuration)
    .AddProblemDetails()
    .AddHttpContextAccessor()
    //.AddInfrastructureServices(config)
    .AddCorsPolicy()
    .AddEndpointsApiExplorer()
    .AddApiVersioningSupport()
    .AddApiDocumentSupport()
    .AddApplicationHealthChecks()
    .AddApplicationServices()
    .AddMiniProfilerSupport()
    .AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    })
    .AddDbContextConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApplicationMiddlewares(app.Environment);
app.UseSerilogRequestLogging();

app.MapControllers();
app.MapApplicationHealthChecks();

await app.RunAsync(new CancellationToken());
