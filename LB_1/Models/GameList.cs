using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LB_1
{
    public partial class GameList
    {
        public int Id { get; set; }

        [Display(Name = "Гравець")]
        public int PlayerId { get; set; }

        [Display(Name = "Номер гри")]
        public int GameId { get; set; }

        [Display(Name = "Прибуток")]
        public int DeltaMoney { get; set; }

        public virtual Game Game { get; set; }
        public virtual Players Player { get; set; }
    }
}
