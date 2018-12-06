using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models
{
    public class Specie
    {
        public int ID { get; set; }
        [Display(Name = "Nome da especie animal")]
        public String SpecieName { get; set; }

    }
}
