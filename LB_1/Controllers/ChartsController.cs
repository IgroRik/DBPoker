using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LB_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly PokerDBContext _context;

        public ChartsController(PokerDBContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var game = _context.Config.Include(b => b.Game).ToList();
            List<object> GameConfig = new List<object>();
            GameConfig.Add(new[] { "Кофігурація", "Кількість ігор" });

            foreach (var c in game)
            {
                GameConfig.Add(new object[] { c.Name, c.Game.Count() });
            }
            return new JsonResult(GameConfig);
        }
    }
}