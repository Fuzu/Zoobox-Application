using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.Animals
{
    public class Story
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Titulo")]
        public String Title { get; set; }
        [Display(Name = "Descrição")]
        public String Description { get; set; }
        [Display(Name = "Criado")]
        public DateTime Created { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Criador")]
        public string CreatedByID { get; set; }

        public virtual int AnimalId { get; set; }
        
        [Display(Name = "Criador")]
        public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}
