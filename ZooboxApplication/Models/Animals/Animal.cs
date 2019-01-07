using Microsoft.AspNetCore.Http;
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

        [Display(Name = "Raça")]
        public int Race { get; set; }

        [ForeignKey("Race")]
        [Display(Name = "Raça")]
        public virtual Race RaceName { get; set; }

        [Display(Name = "Doença")]
        public int Disease { get; set; }

        [ForeignKey("Disease")]
        [Display(Name = "Doença")]
        public virtual DiseaseAnimal DiseaseName { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Data Entrada")]
        public DateTime EntranceDay { get; set; }

        [Display(Name = "Localização no Canil")]
        public String Location { get; set; }

        [ForeignKey("State")]
        [Display(Name = "Estado")]
        public virtual State Statename {get;set;}

        [Display(Name = "Estado")]
        public int State { get; set; }

        [Display(Name = "Imagem")]
        public String ImageFile { get; set; }

        [NotMapped]
        [Display(Name = "Imagem")]
        public  IFormFile Image { get; set; }






    }
}
