using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.Animals
{
    [Display(Name = "Doença")]
    public class Disease
    {
        public int Id { get; set; }

        [Display(Name = "Doença")]
        public String DiseaseName { get; set; }
    }
}
