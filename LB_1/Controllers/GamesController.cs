using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LB_1;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;

namespace LB_1.Controllers
{
    public class GamesController : Controller
    {
        private readonly PokerDBContext _context;
        
        public GamesController(PokerDBContext context)
        {
            _context = context;
        }

        // GET: Games

        public async Task<IActionResult> Index(string field, int[] param, string croupierName)
        {
            if (field == null)
            {
                var pokerDBContext = _context.Game.Include(g => g.Config).Include(g => g.Croupier).Include(g => g.Table);
                return View(await pokerDBContext.ToListAsync());
            }
            else
            {
                if (field == "Table")
                {
                    ViewBag.GameId = param[0];
                    ViewBag.GameCapacity = param[1];
                    ViewBag.GameMaxBet = param[2];
                    var gameByTables = _context.Game.Where(b => b.TableId == param[0]).Include(b => b.Table).Include(g => g.Config).Include(g => g.Croupier);
                    //var pokerDBContext = _context.Game.Include(g => g.Config).Include(g => g.Croupier).Include(g => g.Table);
                    return View(await gameByTables.ToListAsync());
                } else
                if (field == "Config")
                {
                    ViewBag.GameId = param[0];
                    ViewBag.GameMinBet = param[1];
                    ViewBag.GameRoundTime = param[2];
                    var configByTables = _context.Game.Where(b => b.ConfigId == param[0]).Include(b => b.Table).Include(g => g.Config).Include(g => g.Croupier);
                    //var pokerDBContext = _context.Game.Include(g => g.Config).Include(g => g.Croupier).Include(g => g.Table);
                    return View(await configByTables.ToListAsync());
                }
                else
                if (field == "Croupier")
                {
                    ViewBag.GameId = param[0];
                    ViewBag.GameMaxBet = param[1];
                    ViewBag.GameName = croupierName;
                    ViewBag.GameSkill = param[2];
                    var configByTables = _context.Game.Where(b => b.CroupierId == param[0]).Include(b => b.Table).Include(g => g.Config).Include(g => g.Croupier);
                    //var pokerDBContext = _context.Game.Include(g => g.Config).Include(g => g.Croupier).Include(g => g.Table);
                    return View(await configByTables.ToListAsync());
                }else
                { return null; }
            }
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Config)
                .Include(g => g.Croupier)
                .Include(g => g.Table)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            //return View(game);
            return RedirectToAction("Index", "GameLists", new { field = "Game", game.Id });
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["ConfigId"] = new SelectList(_context.Config, "Id", "Id");
            ViewData["CroupierId"] = new SelectList(_context.Croupiers, "Id", "Name");
            ViewData["TableId"] = new SelectList(_context.Tables, "Id", "Id");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CroupierId,TableId,ConfigId,Date")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConfigId"] = new SelectList(_context.Config, "Id", "Id", game.ConfigId);
            ViewData["CroupierId"] = new SelectList(_context.Croupiers, "Id", "Name", game.CroupierId);
            ViewData["TableId"] = new SelectList(_context.Tables, "Id", "Id", game.TableId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["ConfigId"] = new SelectList(_context.Config, "Id", "Id", game.ConfigId);
            ViewData["CroupierId"] = new SelectList(_context.Croupiers, "Id", "Name", game.CroupierId);
            ViewData["TableId"] = new SelectList(_context.Tables, "Id", "Id", game.TableId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CroupierId,TableId,ConfigId,Date")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["ConfigId"] = new SelectList(_context.Config, "Id", "Id", game.ConfigId);
            ViewData["CroupierId"] = new SelectList(_context.Croupiers, "Id", "Name", game.CroupierId);
            ViewData["TableId"] = new SelectList(_context.Tables, "Id", "Id", game.TableId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Config)
                .Include(g => g.Croupier)
                .Include(g => g.Table)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            //перегляд усіх листів (в даному випадку категорій)
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
                                Config newcat;
                                var c = (from cat in _context.Config
                                         where cat.Name.Contains(worksheet.Name)
                                         select cat).ToList();
                                if (c.Count > 0)
                                {
                                    newcat = c[0];
                                }
                                else
                                {
                                    newcat = new Config();
                                    newcat.Name = worksheet.Name;
                                    newcat.MinBet = -1;
                                    newcat.RoundTime = -1;
                                    // newcat.Info = "from EXCEL";
                                    //додати в контекст
                                    _context.Config.Add(newcat);
                                }
                                //перегляд усіх рядкі в                    
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Game book = new Game();
                                        row.Cell(1).TryGetValue(out DateTime value);
                                        book.Date = value;
                                        row.Cell(2).TryGetValue(out int value1);
                                        book.CroupierId = value1;
                                        row.Cell(3).TryGetValue(out int value2);
                                        book.TableId = value2;

                                        book.Config = newcat;
                                        _context.Game.Add(book);
                                        //у разі наявності автора знайти його, у разі відсутності - додати
                                        if (row.Cell(2).Value.ToString().Length > 0)
                                        {
                                            Croupiers author;

                                            var a = (from aut in _context.Croupiers
                                                     where aut.Name.Contains(row.Cell(2).Value.ToString())
                                                     select aut).ToList();
                                            if (a.Count > 0)
                                            {
                                                author = a[0];
                                            }
                                            else
                                            {
                                                author = new Croupiers();
                                                author.Name = row.Cell(2).Value.ToString();
                                                author.MaxBet = -1;
                                                author.Skill = -1;
                                                //додати в контекст
                                                _context.Add(author);
                                            }
                                        }
                                        if (row.Cell(3).Value.ToString().Length > 0)
                                        {
                                            Tables author;

                                            var a = (from aut in _context.Tables
                                                     where aut.Id.ToString().Contains(row.Cell(3).Value.ToString())
                                                     select aut).ToList();
                                            if (a.Count > 0)
                                            {
                                                author = a[0];
                                            }
                                            else
                                            {
                                                author = new Tables();
                                                row.Cell(3).TryGetValue(out int value3);
                                                author.Id = value3;
                                                author.Capacity = -1;
                                                author.MaxBet = -1;
                                                //додати в контекст
                                                _context.Add(author);
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        //logging самостійно :)

                                    }
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var categories = _context.Config.Include(b => b.Game).ToList();
                foreach (var c in categories)
                {
                    var worksheet = workbook.Worksheets.Add(c.Name);

                    worksheet.Cell("A1").Value = "DateTime";
                    worksheet.Cell("B1").Value = "CroupierId";
                    worksheet.Cell("C1").Value = "TableId";
                    var books = c.Game.ToList();

                    for (int i = 0; i < books.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = books[i].Date.ToString();
                        worksheet.Cell(i + 2, 2).Value = books[i].CroupierId;
                        worksheet.Cell(i + 2, 3).Value = books[i].TableId;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Configs_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

    }
}
