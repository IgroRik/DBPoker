using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LB_1
{
    public partial class Tables
    {
        public Tables()
        {
            Game = new HashSet<Game>();
        }

        [Display(Name = "Номер столу")]
        public int Id { get; set; }

        [Display(Name = "Місткість")]
        public int Capacity { get; set; }

        [Display(Name = "Максимальна ставка")]
        public int MaxBet { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
