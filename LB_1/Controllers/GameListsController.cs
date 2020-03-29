using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LB_1;

namespace LB_1.Controllers
{
    public class GameListsController : Controller
    {
        private readonly PokerDBContext _context;

        public GameListsController(PokerDBContext context)
        {
            _context = context;
        }

        // GET: GameLists
        public async Task<IActionResult> Index(string field, int? id)
        {
            if (id == null)
            {
                var pokerDBContext = _context.GameList.Include(g => g.Game).Include(g => g.Player);
                return View(await pokerDBContext.ToListAsync());
            }
            else
            if (field == "Game")
            {
                ViewBag.GameId = id;
                var gameToList = _context.GameList.Where(g => g.GameId == id).Include(g => g.Game).Include(g => g.Player);
                return View(await gameToList.ToListAsync());
            }
            else
            {
                ViewBag.PlayerId = id;

                var playerToList = _context.GameList.Where(g => g.PlayerId == id).Include(g => g.Game).Include(g => g.Player);
                return View(await playerToList.ToListAsync());
            }
        }

        // GET: GameLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameList
                .Include(g => g.Game)
                .Include(g => g.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameList == null)
            {
                return NotFound();
            }

            return View(gameList);
        }

        // GET: GameLists/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id");
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Login");
            return View();
        }

        // POST: GameLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,GameId,DeltaMoney")] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameList.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Login", gameList.PlayerId);
            return View(gameList);
        }

        // GET: GameLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameList.FindAsync(id);
            if (gameList == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameList.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Login", gameList.PlayerId);
            return View(gameList);
        }

        // POST: GameLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,GameId,DeltaMoney")] GameList gameList)
        {
            if (id != gameList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameListExists(gameList.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameList.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Login", gameList.PlayerId);
            return View(gameList);
        }

        // GET: GameLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameList
                .Include(g => g.Game)
                .Include(g => g.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameList == null)
            {
                return NotFound();
            }

            return View(gameList);
        }

        // POST: GameLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameList = await _context.GameList.FindAsync(id);
            _context.GameList.Remove(gameList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameListExists(int id)
        {
            return _context.GameList.Any(e => e.Id == id);
        }
    }
}
