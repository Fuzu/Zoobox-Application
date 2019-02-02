using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.Adoptions
{
    public class Adoption
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime AdoptionDate { get; set; }

        public int Animal { get; set; }

        [ForeignKey("Animal")]
        [Display(Name ="Nome do Animal")]
        public virtual Animal AnimalName { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Display(Name = "Utilizador")]
        public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("AdoptionTypeName")]
        public int AdoptionType { get; set; }

        [Display(Name = "Tipo adoção")]
        public virtual AdoptionType AdoptionTypeName{ get; set; }

    }
}
