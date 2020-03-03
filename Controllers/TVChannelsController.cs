using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TV_Show_Ratings_MVC.Data;
using TV_Show_Ratings_MVC.Models;

namespace TV_Show_Ratings_MVC.Controllers
{
    public class TVChannelsController : Controller
    {
        private readonly TV_Show_Ratings_DbContext _context;

        public TVChannelsController(TV_Show_Ratings_DbContext context)
        {
            _context = context;
        }

        // GET: TVChannels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TVChannel.ToListAsync());
        }

        // GET: TVChannels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVChannel = await _context.TVChannel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tVChannel == null)
            {
                return NotFound();
            }

            return View(tVChannel);
        }
        [Authorize]
        // GET: TVChannels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TVChannels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChannelName,Established")] TVChannel tVChannel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tVChannel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tVChannel);
        }
        [Authorize]
        // GET: TVChannels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVChannel = await _context.TVChannel.FindAsync(id);
            if (tVChannel == null)
            {
                return NotFound();
            }
            return View(tVChannel);
        }

        // POST: TVChannels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChannelName,Established")] TVChannel tVChannel)
        {
            if (id != tVChannel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tVChannel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TVChannelExists(tVChannel.Id))
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
            return View(tVChannel);
        }
        [Authorize]
        // GET: TVChannels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVChannel = await _context.TVChannel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tVChannel == null)
            {
                return NotFound();
            }

            return View(tVChannel);
        }

        // POST: TVChannels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tVChannel = await _context.TVChannel.FindAsync(id);
            _context.TVChannel.Remove(tVChannel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TVChannelExists(int id)
        {
            return _context.TVChannel.Any(e => e.Id == id);
        }
    }
}
