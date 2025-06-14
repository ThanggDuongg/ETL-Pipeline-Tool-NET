using Hangfire.Dashboard;

namespace ETLPipelineTool.Api.Configurations
{
  public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
  {
    public bool Authorize(DashboardContext context)
    {
      // TODO: Implement proper authorization
      return true;
    }
  }
}
