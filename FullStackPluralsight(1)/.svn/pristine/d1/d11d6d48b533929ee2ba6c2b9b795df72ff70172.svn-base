using FullStackPluralsight.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace FullStackPluralsight.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artistFollowing = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();

            return View(artistFollowing);
        }
    }
}