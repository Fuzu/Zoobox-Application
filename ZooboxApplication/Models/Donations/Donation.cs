using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Models.Donations
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tipo de doação")]
        public int DonationType { get; set; }

        [ForeignKey("DonationType")]
        [Display(Name = "Tipo de doação")]
        public virtual DonationType DonationTypeName{ get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Não pode estar vazia")]
        public String Quantity { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Display(Name = "Utilizador")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
