using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Models
{
    public class Animal 
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public String Name { get; set; }

        [ForeignKey("Race")]
        [Display(Name = "Raça")]
        public virtual Race RaceName { get; set; }

        public virtual int Race { get; set; }

        [ForeignKey("Disease")]
        [Display(Name = "Doença")]
        public virtual DiseaseAnimal DiseaseName { get; set; }

        public virtual int Disease { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EntranceDay { get; set; }

        [Display(Name = "Localização no Canil")]
        public String Location { get; set; }





    }
}
