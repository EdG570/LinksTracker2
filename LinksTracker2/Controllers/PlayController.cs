using System.Web.Mvc;

namespace LinksTracker2.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        public ActionResult SelectCourse()
        {
            return View();
        }
    }
}