using LinksTracker2.Models;
using LinksTracker2.Repositories;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LinksTracker2.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly StatsRepository _statsRepo;
        private readonly ApplicationDbContext _db;

        public StatsController()
        {
            _statsRepo = new StatsRepository();  
            _db = new ApplicationDbContext(); 
        }

        // GET: Stats
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var allStats = _statsRepo.GetAll().Where(x => x.UserId == userId);

            var user = _db.Users.Include(u => u.Stats).FirstOrDefault(u => u.Id == userId);
            
            return View();
        }
    }
}