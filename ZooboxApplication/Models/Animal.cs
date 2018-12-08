﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models
{
    public class Animal 
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        public String Name { get; set; }

        [Display(Name = "Raça")]
        public IEnumerable<Race> Race { get; set; }

        [Display(Name = "Localização no Canil")]
        public String Location { get; set; }

        [Display(Name = "Doença")]
        public Disease Disease { get; set; }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public DateTime EntranceDay { get; set; }



    }
}
