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
    public class TVShowsController : Controller
    {
        private readonly TV_Show_Ratings_DbContext _context;

        public TVShowsController(TV_Show_Ratings_DbContext context)
        {
            _context = context;
        }

        // GET: TVShows
        public async Task<IActionResult> Index()
        {
            var tV_Show_Ratings_DbContext = _context.TVShow.Include(t => t.TvChannel);
            return View(await tV_Show_Ratings_DbContext.ToListAsync());
        }

        // GET: TVShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVShow = await _context.TVShow
                .Include(t => t.TvChannel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tVShow == null)
            {
                return NotFound();
            }

            return View(tVShow);
        }
        [Authorize]
        // GET: TVShows/Create
        public IActionResult Create()
        {
            ViewData["TVChannelId"] = new SelectList(_context.TVChannel, "Id", "Id");
            return View();
        }

        // POST: TVShows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TVChannelId,ShowName")] TVShow tVShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tVShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TVChannelId"] = new SelectList(_context.TVChannel, "Id", "Id", tVShow.TVChannelId);
            return View(tVShow);
        }
        [Authorize]
        // GET: TVShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVShow = await _context.TVShow.FindAsync(id);
            if (tVShow == null)
            {
                return NotFound();
            }
            ViewData["TVChannelId"] = new SelectList(_context.TVChannel, "Id", "Id", tVShow.TVChannelId);
            return View(tVShow);
        }

        // POST: TVShows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TVChannelId,ShowName")] TVShow tVShow)
        {
            if (id != tVShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tVShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TVShowExists(tVShow.Id))
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
            ViewData["TVChannelId"] = new SelectList(_context.TVChannel, "Id", "Id", tVShow.TVChannelId);
            return View(tVShow);
        }
        [Authorize]
        // GET: TVShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVShow = await _context.TVShow
                .Include(t => t.TvChannel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tVShow == null)
            {
                return NotFound();
            }

            return View(tVShow);
        }

        // POST: TVShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tVShow = await _context.TVShow.FindAsync(id);
            _context.TVShow.Remove(tVShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TVShowExists(int id)
        {
            return _context.TVShow.Any(e => e.Id == id);
        }
    }
}
