using Microsoft.AspNetCore.Identity;
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
    public class Job : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Aberviação")]
        [Required(ErrorMessage = "Não pode estar vazia")]
        public String Abbreviation { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Não pode estar vazia")]
        public String Description { get; set; }

        [Range(typeof(DateTime), "1/1/1966", "1/1/3000")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Entrada")]
        public DateTime BeginDay { get; set; }

        [Range(typeof(DateTime), "1/1/1966", "1/1/3000")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Fim")]
        [Required(ErrorMessage = "Não pode estar vazia")]
        public DateTime EndDay { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Não pode estar vazia")]
        public String State { get; set; }
       

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Display(Name = "Utilizador")]
          public virtual ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (BeginDay > EndDay)
            {
                yield return
                  new ValidationResult("Data Final tem de ser maior que a inicial", new[] { "EndDay" });
            }
            if(BeginDay.Year > DateTime.Now.Year)
            {
                yield return
                 new ValidationResult("Data Inicial tem de ser igual ao ano em vigor");
            }
            if (EndDay.Year > DateTime.Now.Year)
            {
                yield return
                 new ValidationResult("Data Final tem de ser igual ao ano em vigor");
            }
        }
    }
}
