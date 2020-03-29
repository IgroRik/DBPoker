using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LB_1
{
    public partial class Config
    {
        public Config()
        {
            Game = new HashSet<Game>();
        }

        [Display(Name = "Номер конфігурації")]
        public int Id { get; set; }

        [Display(Name = "Час на раунд")]
        public int RoundTime { get; set; }

        [Display(Name = "Початкова ставка")]
        public int MinBet { get; set; }


        [Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
