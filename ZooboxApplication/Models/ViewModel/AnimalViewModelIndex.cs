using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.ViewModel
{
    public class AnimalViewModelIndex
    {
        public int Id { get; set; }

        public List<Animal> AnimalList { get; set; }


        [Display(Name = "Raça")]
        public List<SelectListItem> Race { get; set; }

        [Display(Name = "Localização no Canil")]
        public String Location { get; set; }

        [Display(Name = "Doença")]
        public List<SelectListItem> Disease { get; set; }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public DateTime EntranceDay { get; set; }
    }
}
