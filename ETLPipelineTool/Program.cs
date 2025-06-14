var builder = WebApplication.CreateBuilder(args);

// KestrelServerOptions
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

EnvironmentsHelper.SetupSerilog(builder);

builder.Logging.AddSerilogService();
builder.Services.AddAntiforgerySupport();
builder.Services.AddControllersWithViews();
builder
  .Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });

builder
  .Services.AddValidationConfiguration()
  .AddAppSettingsConfiguration(builder.Configuration)
  .AddProblemDetails()
  .AddHttpContextAccessor()
  .AddInfrastructureServices()
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
  .AddDbContextConfiguration(builder.Configuration)
  .AddHangfireServices(builder.Configuration);

var app = builder.Build();

app.UseApplicationMiddlewares(app.Environment);
app.UseSerilogRequestLogging();

app.MapControllers();
app.MapApplicationHealthChecks();

await app.RunAsync(new CancellationToken());
