using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TV_Show_Ratings_MVC.Data;
using TV_Show_Ratings_MVC.Models;

namespace TV_Show_Ratings_MVC.Controllers
{
    public class RatingsController : Controller
    {
        private readonly TV_Show_Ratings_DbContext _context;

        public RatingsController(TV_Show_Ratings_DbContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var tV_Show_Ratings_DbContext = _context.Rating.Include(r => r.Subscriber).Include(r => r.TVShow);
            return View(await tV_Show_Ratings_DbContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Subscriber)
                .Include(r => r.TVShow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }
        [Authorize]
        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["SubscriberId"] = new SelectList(_context.Subscriber, "Id", "Id");
            ViewData["TVShowId"] = new SelectList(_context.Set<TVShow>(), "Id", "Id");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubscriberId,TVShowId,RatingValue")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriberId"] = new SelectList(_context.Subscriber, "Id", "Id", rating.SubscriberId);
            ViewData["TVShowId"] = new SelectList(_context.Set<TVShow>(), "Id", "Id", rating.TVShowId);
            return View(rating);
        }
        [Authorize]
        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["SubscriberId"] = new SelectList(_context.Subscriber, "Id", "Id", rating.SubscriberId);
            ViewData["TVShowId"] = new SelectList(_context.Set<TVShow>(), "Id", "Id", rating.TVShowId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubscriberId,TVShowId,RatingValue")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriberId"] = new SelectList(_context.Subscriber, "Id", "Id", rating.SubscriberId);
            ViewData["TVShowId"] = new SelectList(_context.Set<TVShow>(), "Id", "Id", rating.TVShowId);
            return View(rating);
        }
        [Authorize]
        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Subscriber)
                .Include(r => r.TVShow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}
