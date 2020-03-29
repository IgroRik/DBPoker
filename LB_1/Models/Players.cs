using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LB_1
{
    public partial class Players
    {
        public Players()
        {
            GameList = new HashSet<GameList>();
        }

        [Display(Name = "Номер гравця")]
        public int Id { get; set; }

        [Display(Name = "П.І.П.")]
        public string Login { get; set; }

        [Display(Name = "Початковый капітал")]
        public decimal StartCapital { get; set; }

        [Display(Name = "Дата народження")]
        public DateTime Birth { get; set; }

        public virtual ICollection<GameList> GameList { get; set; }
    }
}
