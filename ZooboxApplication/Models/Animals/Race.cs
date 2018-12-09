using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models
{
    public class Race
    {
        public int Id { get; set; }
        [Display(Name = "Nome da Raça")]
        public String RaceName { get; set; }

        public ICollection<Animal> Animals{ get; set; }

    }
}
