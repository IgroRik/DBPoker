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
    public class CroupiersController : Controller
    {
        private readonly PokerDBContext _context;

        public CroupiersController(PokerDBContext context)
        {
            _context = context;
        }

        // GET: Croupiers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Croupiers.ToListAsync());
        }

        // GET: Croupiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var croupiers = await _context.Croupiers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (croupiers == null)
            {
                return NotFound();
            }

            //return View(croupiers);
            return RedirectToAction("Index", "Games", new { field = "Croupier", param = new int[3]{ croupiers.Id, croupiers.MaxBet, croupiers.Skill }, name = croupiers.Name });
        }

        // GET: Croupiers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Croupiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaxBet,Skill,Name")] Croupiers croupiers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(croupiers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(croupiers);
        }

        // GET: Croupiers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var croupiers = await _context.Croupiers.FindAsync(id);
            if (croupiers == null)
            {
                return NotFound();
            }
            return View(croupiers);
        }

        // POST: Croupiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaxBet,Skill,Name")] Croupiers croupiers)
        {
            if (id != croupiers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(croupiers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CroupiersExists(croupiers.Id))
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
            return View(croupiers);
        }

        // GET: Croupiers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var croupiers = await _context.Croupiers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (croupiers == null)
            {
                return NotFound();
            }

            return View(croupiers);
        }

        // POST: Croupiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var croupiers = await _context.Croupiers.FindAsync(id);
            _context.Croupiers.Remove(croupiers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CroupiersExists(int id)
        {
            return _context.Croupiers.Any(e => e.Id == id);
        }
    }
}
