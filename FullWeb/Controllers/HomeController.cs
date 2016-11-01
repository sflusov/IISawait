using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

using NLog;

namespace FullWeb.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class HomeController : Controller
    {
        private const int _waitTimeout = 30000;
        private const string _beginFmt = "Begin method; Count = {0}; OperId = {1};";
        private const string _endFmt =   "End method  ; Count = {0}; OperId = {1};";

        private static volatile int _counter = 0;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetWithThreadSleep()
        {
            Guid operId = Guid.NewGuid();

            _logger.Info(_beginFmt, Interlocked.Increment(ref _counter), operId);

            Thread.Sleep(_waitTimeout);

            _logger.Info(_endFmt, Interlocked.Decrement(ref _counter), operId);

            return Json(null, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetWithTaskAwait()
        {
            Guid operId = Guid.NewGuid();

            _logger.Info(_beginFmt, Interlocked.Increment(ref _counter), operId);

            await Task.Delay(_waitTimeout);

            _logger.Info(_endFmt, Interlocked.Decrement(ref _counter), operId);

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetWithTaskAwaitConfigure()
        {
            Guid operId = Guid.NewGuid();

            _logger.Info(_beginFmt, Interlocked.Increment(ref _counter), operId);

            await Task.Delay(_waitTimeout).ConfigureAwait(false);

            _logger.Info(_endFmt, Interlocked.Decrement(ref _counter), operId);

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}