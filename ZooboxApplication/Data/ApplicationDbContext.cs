using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Animals;
using ZooboxApplication.Models.ViewModel;
using ZooboxApplication.Models.Donations;
using ZooboxApplication.Models.Adoptions;

namespace ZooboxApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ZooboxApplication.Models.Animal> Animal { get; set; }
        public DbSet<ZooboxApplication.Models.Race> Race { get; set; }
        public DbSet<ZooboxApplication.Models.Specie> Specie { get; set; }
        public DbSet<ZooboxApplication.Models.State> State { get; set; }
        public DbSet<ZooboxApplication.Models.Animals.DiseaseAnimal> DiseaseAnimal { get; set; }
        public DbSet<ZooboxApplication.Models.Job> Job{ get; set; }
        public DbSet<ZooboxApplication.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ZooboxApplication.Models.ViewModel.UserViewModel> UserViewModel { get; set; }
        public DbSet<ZooboxApplication.Models.Animals.DonationType> DonationType { get; set; }
        public DbSet<ZooboxApplication.Models.Donations.Donation> Donation { get; set; }
        public DbSet<ZooboxApplication.Models.Adoptions.AdoptionType> AdoptionType { get; set; }
        public DbSet<ZooboxApplication.Models.Adoptions.Adoption> Adoption { get; set; }
        //public DbSet<ZooboxApplication.Models.UserCustom> UserList { get; set; }
    }
}
