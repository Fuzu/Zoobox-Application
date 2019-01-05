using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooboxApplication.Models.ViewModel
{
  
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Morada")]
        public string address { get; set; }


        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Informação adicional")]
        public string additionInformation { get; set; }


        [Display(Name = "Tipo de Utilizador")]
        public string Role { get; set; }
    }
}
