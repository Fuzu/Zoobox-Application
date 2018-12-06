using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models
{
    public class State
    {
        public int ID { get; set; }
        [Display(Name = "Estado do animal")]
        public String StateName { get; set; }
    }
}
