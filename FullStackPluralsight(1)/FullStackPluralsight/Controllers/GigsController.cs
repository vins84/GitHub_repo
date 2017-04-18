using FullStackPluralsight.Models;
using FullStackPluralsight.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;                   // Added becasue Include in Attending() wont work

namespace FullStackPluralsight.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;     //Apparantely Resharper cired about it not being a readonly 
        
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => 
                    g.ArtistId == userId &&             //give me this artist 
                    g.DateTime > DateTime.Now &&        //gigs in the future
                    !g.IsCanceled)                      //and are not canceled
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            
            var gigsAttending = _context.Attendances
                .Where(u => u.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigsAttending,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I am going to"
            };

            return View("Gigs", viewModel);
        }

        

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]              //we only want that to be called by the post method
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.GigFormViewModel viewModel)
        {
            //var artistId = ;                       //This is OK as it is possible becasue the compiler can convert it to the sql query 
            ////var artist = _context.Users.Single(u => u.Id == User.Identity.GetUserId();    //This will throw the error now so above line must be used
            
            //var artist = _context.Users.Single(u => u.Id == artistId);
                ///* picks the indentity of the user currently loggedin */
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),    //Time of teh gig must be in 20:00.
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            //so now that the gig object is created it can be added to the _context.
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //now we are redirecting to the home page which temporarly gonna show all gigs
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId
                
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]              //we only want that to be called by the post method
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            //gig.Venue = viewModel.Venue;
            //gig.DateTime = viewModel.GetDateTime();         //this combines date and time and reutnrs them as an object 
            //gig.GenreId = viewModel.Genre;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

    }
}