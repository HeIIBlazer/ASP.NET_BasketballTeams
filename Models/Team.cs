using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BasketBall_JPTV20.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Display(Name = "Название команды")]

        public string Name { get; set; }
        [Display(Name = "Фамилия тренера")]

        public string Coach { get; set; }

        public ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>(); 
        }
    }
}