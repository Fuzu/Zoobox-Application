using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Morada")]
        public string address { get; set; }

        
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Número Inválido")]
        [Display(Name = "Telefone")]
        public override string PhoneNumber { get; set; }

        [Display(Name = "Informação adicional")]
        public string additionInformation { get; set; }


        [Display(Name = "Tipo de Utilizador")]
        public string Role { get; set; }
    }
}
