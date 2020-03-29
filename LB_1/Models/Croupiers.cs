using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LB_1
{
    public partial class Croupiers
    {
        public Croupiers()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }

        [Display(Name="Максимальна ставка")]
        public int MaxBet { get; set; }

        [Display(Name = "Здібності")]
        public int Skill { get; set; }

        [Display(Name = "П.І.П")]
        public string Name { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
