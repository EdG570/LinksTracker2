using LinksTracker2.Models;
using LinksTracker2.Repositories;
using LinksTracker2.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace LinksTracker2.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        private readonly CourseRepository _courseRepo;
        private readonly StatsRepository _statsRepo;

        public PlayController()
        {
            _courseRepo = new CourseRepository();
            _statsRepo = new StatsRepository();
        }

        // GET: Play
        public ActionResult SelectCourse()
        {
            var vm = new SelectCourseViewModel
            {
                Courses = _courseRepo.GetDropdown()
            };

            return View(vm);
        }

        public JsonResult Course(int id)
        {
            var result = JsonConvert.SerializeObject(_courseRepo.FindOne(id), Formatting.Indented,
                       new JsonSerializerSettings
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Stats(Stats record)
        {
            record.CreatedAt = DateTime.Now;
            record.UserId = User.Identity.GetUserId();
            var recordFromRepo = _statsRepo.Create(record);
            var newRecord = _statsRepo.FindOne(recordFromRepo.Id);

            var result = JsonConvert.SerializeObject(newRecord, Formatting.Indented,
                       new JsonSerializerSettings
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}