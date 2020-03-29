using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LB_1;

namespace LB_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
            private readonly PokerDBContext _context;

            public ValueController(PokerDBContext context)
            {
                _context = context;
            }

        [HttpGet("JsonData")]
        public JsonResult JsonData(int id)
        {
            var game = _context.GameList.Include(b => b.Player).ToList();
            
            //if (IsId(game))
            {
                int a = 0, b = 0;
                var GameConfig = new List<object>
                {
                    new[] { "Фішки", "Кількість" }
                };
                GameConfig.Add(new object[] { "Виграно", a });
                GameConfig.Add(new object[] { "Програно", b });

                foreach (var c in game)
                {
                    if (c.DeltaMoney > 0)
                    {
                        a += c.DeltaMoney;
                        GameConfig.RemoveAt(1);
                        GameConfig.Insert(1, new object[] { "Виграно", a });
                    }
                    else
                    {
                        b += c.DeltaMoney;
                        GameConfig.RemoveAt(2);
                        GameConfig.Add(new object[] { "Програно", Math.Abs(b) });
                    }
                }
                return new JsonResult(GameConfig);
            }
            //else return null;
        }

        bool IsId(List<GameList> game)
        {
            var gid = game[0].PlayerId;
            
            foreach (var g in game)
            {
                if (g.PlayerId != gid) return false;
            }
            return true;
        }
    }
}
