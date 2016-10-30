using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;

using NLog;

namespace FullWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static volatile int _mvcAppCount = 0;
        private static volatile int _appCount = 0;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public MvcApplication()
            : base()
        {
            _logger.Info("MVC Count = {0};", Interlocked.Increment(ref _mvcAppCount));
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _logger.Info("APP Count = {0};", Interlocked.Increment(ref _appCount));
        }

        protected void Application_End(object sender, System.EventArgs e)
        {
            _logger.Info("APP Count = {0};", Interlocked.Decrement(ref _appCount));
        }

        public override void Dispose()
        {
            _logger.Info("MVC Count = {0};", Interlocked.Decrement(ref _mvcAppCount));

            base.Dispose();
        }
    }
}
