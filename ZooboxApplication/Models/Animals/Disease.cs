using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.Animals
{
    public class Disease
    {
        public int Id { get; set; }

        [Display(Name = "Nome da doença")]
        public String DiseaseName { get; set; }
    }
}
