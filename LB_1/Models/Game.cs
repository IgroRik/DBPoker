using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LB_1
{
    public partial class Game
    {
        public Game()
        {
            GameList = new HashSet<GameList>();
        }

        [Display(Name = "Номер гри")]
        public int Id { get; set; }

        [Display(Name="Круп\'є")]
        public int CroupierId { get; set; }

        [Display(Name = "Номер столу")]
        public int TableId { get; set; }


        [Display(Name = "Конфігурація")]
        public int ConfigId { get; set; }


        [Display(Name = "Дата гри")]
        public DateTime Date { get; set; }

        public virtual Config Config { get; set; }
        public virtual Croupiers Croupier { get; set; }
        public virtual Tables Table { get; set; }
        public virtual ICollection<GameList> GameList { get; set; }
    }
}
