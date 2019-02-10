using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Data
{
    public class ApplicationSeed
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
       

        public ApplicationSeed(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }




        public async Task Users()
        {
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser {
                Name = "Admin User",
                UserName = "admin@zoobox.pt",
                Email = "admin@zoobox.pt",
                ImageFile = "/images/upload/user.png",
                Role = "Administrator"
                },

                new ApplicationUser {
                Name = "Employe User",
                UserName = "employe@zoobox.pt",
                Email = "employe@zoobox.pt",
                ImageFile = "/images/upload/user.png",
                Role = "Employe"
                },

                new ApplicationUser {
                Name = "Voluntary User",
                UserName = "voluntary@zoobox.pt",
                Email = "voluntary@zoobox.pt",
                ImageFile = "/images/upload/user.png",
                Role = "Voluntary"
                },

                new ApplicationUser {
                Name = "Cliente User",
                UserName = "client@zoobox.pt",
                Email = "client@zoobox.pt",
                ImageFile = "/images/upload/user.png",
                Role = "Client"
                }
            };


            if (!_context.Users.Any())
            {
                foreach (var user in users)
                {
                   await _userManager.CreateAsync(user, "123456_Abc");
                    var existRole = await _roleManager.RoleExistsAsync(user.Role);
                    if (!existRole)
                    {
                        IdentityRole role = new IdentityRole();
                        role.Name = user.Role;
                        await _roleManager.CreateAsync(role);
                    }

                    await _userManager.AddToRoleAsync(user, user.Role);

                }
            }
  

        }

        private async Task Races()
        {
            List<Race> races = new List<Race>()
            {
                new Race { RaceName = "Desconhecido" },
                new Race { RaceName = "Akita" },
                new Race { RaceName = "Pit Bull" },
                new Race { RaceName = "Barbado da Terceira" },
                new Race { RaceName = "Basset Hound" },
                new Race { RaceName = "Beagle" },
                new Race { RaceName = "Bichon Maltês" },
                new Race { RaceName = "Border Collie" },
                new Race { RaceName = "Boston Terrier" },
                new Race { RaceName = "Boxer" },
                new Race { RaceName = "Braco Alemão" },
                new Race { RaceName = "Bull Terrier" },
                new Race { RaceName = "Bulldog Francês" },
                new Race { RaceName = "Bulldog Inglês" },
                new Race { RaceName = "Cane Corso" },
                new Race { RaceName = "Caniche" },
                new Race { RaceName = "Serra da Estrela" },
                new Race { RaceName = "Serra de Aires" },
                new Race { RaceName = "Cão de Água Português" },
                new Race { RaceName = "Cão de Gado Transmontano" },
                new Race { RaceName = "Chihuahua" },
                new Race { RaceName = "Dálmata" },
                new Race { RaceName = "Azul russo" },
                new Race { RaceName = "Persa" },
                new Race { RaceName = "Siamês" },
                new Race { RaceName = "Maine Coon" },
            };

            if (!_context.Race.Any())
            {
                foreach (var race in races)
                {
                     _context.Race.Add(race);
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task States()
        {
            List<State> states = new List<State>()
            {
                new State { StateName = "Saudável" },
                new State { StateName = "Quarentena" },
                new State { StateName = "Adoptado" },
                new State { StateName = "Falecido" },
                new State { StateName = "FAT" },
                new State { StateName = "Doente" },
                new State { StateName = "Desaparecido" },


            };

            if (!_context.State.Any())
            {
                foreach (var state in states)
                {
                     _context.State.Add(state);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task Diseases()
        {
            List<DiseaseAnimal> diseases = new List<DiseaseAnimal>()
            {
                new DiseaseAnimal { DiseaseName = "Nenhuma" },
                new DiseaseAnimal { DiseaseName = "Todas" },
                new DiseaseAnimal { DiseaseName = "Outras" },
            };

            if (!_context.DiseaseAnimal.Any())
            {
                foreach (var disease in diseases)
                {
                     _context.DiseaseAnimal.Add(disease);
                    await _context.SaveChangesAsync();
                }
            }
        }


        public async Task Animals()
        {
            if (!_context.Race.Any())
            {
                await Races();
            }
            if (!_context.State.Any())
            {
                await States();
            }
            if (!_context.DiseaseAnimal.Any())
            {
                await Diseases();
            }

            List<Animal> animals = new List<Animal>()
            {
                new Animal {
                    Name = "Escu",
                    Race = 1,
                    Disease = 1,
                    State = 1,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/escu.jpg",
                    Location = "C001"
                },
                new Animal {
                    Name = "Kita",
                    Race = 2,
                    Disease = 1,
                    State = 1,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/kita.jpg",
                    Location = "C002"
                },
                new Animal {
                    Name = "Bull",
                    Race = 3,
                    Disease = 1,
                    State = 1,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/bull.jpg",
                    Location = "C003"
                },
                new Animal {
                    Name = "Staff",
                    Race = 4,
                    Disease = 1,
                    State = 1,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/staff.jpg",
                    Location = "C004"
                },
                new Animal {
                    Name = "Niche",
                    Race = 5,
                    Disease = 1,
                    State = 3,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/golden.jpg",
                    Location = "C005"
                },
                new Animal {
                    Name = "Braco",
                    Race = 6,
                    Disease = 1,
                    State = 1,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/braco.jpg",
                    Location = "C006"
                },
                new Animal {
                    Name = "Gado",
                    Race = 7,
                    Disease = 1,
                    State = 6,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/gado.jpg",
                    Location = "C007"
                },
                new Animal {
                    Name = "Corso",
                    Race = 8,
                    Disease = 1,
                    State = 1,
                    EntranceDay = DateTime.Now,
                    ImageFile = "/images/upload/corso.jpg",
                    Location = "C008"
                },


            };

            if (!_context.Animal.Any())
            {
                foreach (var animal in animals)
                {
                     _context.Animal.Add(animal);
                    await _context.SaveChangesAsync();
                }
            }
        }


        }
}
