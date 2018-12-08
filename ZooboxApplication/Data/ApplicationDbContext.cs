using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
    }
}
