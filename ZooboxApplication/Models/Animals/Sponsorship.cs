using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.Animals
{
    public class Sponsorship
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Display(Name = "Utilizador")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Animal")]
        public int AnimalId { get; set; }

        [Display(Name = "Utilizador")]
        public virtual Animal Animal { get; set; }
    }
}
