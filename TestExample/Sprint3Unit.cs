using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Animals;
using ZooboxApplication.Models.Donations;
using ZooboxApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TestZoobox
{

    [TestClass]
    public class Sprint3Unit
    {

        private TestHelper helper;

        public Sprint3Unit()
        {
            TestHelper _helper = new TestHelper(true);
            helper = _helper;
        }

        [TestMethod]
        public void CreateAnimals()
        {

            var animal = new Animal();
            animal.Name = "teste Animal";
            animal.Location = "localização";
            animal.Race = 1;
            animal.State = 1;
            animal.Disease = 1;
            animal.EntranceDay = DateTime.Now;
            var response = helper.Context.Add(animal);
            Assert.AreEqual(response.State, EntityState.Added);

        }
        [TestMethod]
        public void UpdateAnimals()
        {

            #region add animal
            var animal = new Animal();
            animal.Name = "teste Animal";
            animal.Location = "localização";
            animal.Race = 1;
            animal.State = 1;
            animal.Disease = 1;
            animal.EntranceDay = DateTime.Now;
            helper.Context.Add(animal);
            #endregion
            var animalUpdate = helper.Context.Animal.Find(1);
            animalUpdate.Name = "teste Animal - update";
            var response = helper.Context.Update(animalUpdate);
            Assert.AreEqual(response.Entity.Name, animalUpdate.Name);
        }

        [TestMethod]
        public void DetailsAnimals()
        {
            #region add animal
            var animal = new Animal();
            animal.Name = "teste Animal";
            animal.Location = "localização";
            animal.Race = 1;
            animal.State = 1;
            animal.Disease = 1;
            animal.EntranceDay = DateTime.Now;
            helper.Context.Add(animal);
            #endregion
            var animalDetails = helper.Context.Animal.Find(1);
            Assert.AreEqual(animal.Id, 1);
        }

        [TestMethod]
        public void DeleteAnimals()
        {
            #region add animal
            var animal = new Animal();
            animal.Name = "teste Animal";
            animal.Location = "localização";
            animal.Race = 1;
            animal.State = 1;
            animal.Disease = 1;
            animal.EntranceDay = DateTime.Now;
            helper.Context.Add(animal);
            #endregion
            var animalDeleted = helper.Context.Animal.Find(1);
            var response = helper.Context.Remove(animalDeleted);
            Assert.AreEqual(response.State, EntityState.Detached);

        }

        [TestMethod]
        public void CreateUser()
        {
            var user = new ApplicationUser()
            {
                Name = "Teste User",
                UserName = "teste.user",
                Email = "teste@teste.pt",
            };

            var response = helper.UserManager.CreateAsync(user, "Teste_123");
            Assert.AreEqual(response.Result.Succeeded, true);
        }

        [TestMethod]
        public void UpdateUser()
        {
            #region add user
            var user = new ApplicationUser()
            {
                Name = "Teste User",
                UserName = "teste.user",
                Email = "teste@teste.pt",
            };
            helper.UserManager.CreateAsync(user, "Teste_123");
            #endregion

            var findUser = helper.UserManager.FindByEmailAsync("teste@teste.pt").Result;
            findUser.UserName = "teste.user.update";
            var response = helper.Context.Update(findUser);
            Assert.AreEqual(response.Entity.UserName, findUser.UserName);
        }

        [TestMethod]
        public void DetailsUser()
        {
            #region add user
            var user = new ApplicationUser()
            {
                Name = "Teste User",
                UserName = "teste.user",
                Email = "teste@teste.pt",
            };
            helper.UserManager.CreateAsync(user, "Teste_123");
            #endregion

            var findUser = helper.UserManager.FindByEmailAsync("teste@teste.pt").Result;
            Assert.AreEqual(findUser.Email, "teste@teste.pt");
        }

        [TestMethod]
        public void DeleteUser()
        {
            #region add user
            var user = new ApplicationUser()
            {
                Name = "Teste User",
                UserName = "teste.user",
                Email = "teste@teste.pt",
            };
            helper.UserManager.CreateAsync(user, "Teste_123");
            #endregion

            var findUser = helper.UserManager.FindByEmailAsync("teste@teste.pt").Result;
            var response = helper.Context.Remove(findUser);
            Assert.AreEqual(response.State, EntityState.Deleted);
        }

        [TestMethod]
        public void CreateDisease()
        {
            var disease = new DiseaseAnimal()
            {
                DiseaseName = "Doença Teste",
            };
            var response = helper.Context.Add(disease);
            Assert.AreEqual(response.State, EntityState.Added);

        }

        [TestMethod]
        public void CreateRace()
        {
            var race = new Race()
            {
                RaceName = "Raça Teste"
            };
            var response = helper.Context.Add(race);
            Assert.AreEqual(response.State, EntityState.Added);

        }


        [TestMethod]
        public void CreateSpecie()
        {
            var species = new Specie()
            {
                SpecieName = "Especie Teste"
            };
            var response = helper.Context.Add(species);
            Assert.AreEqual(response.State, EntityState.Added);
        }


        [TestMethod]
        public void CreateState()
        {
            var state = new State()
            {
                StateName = "Activo Teste"
            };
            var response = helper.Context.Add(state);
            Assert.AreEqual(response.State, EntityState.Added);
        }


        [TestMethod]
        public void CreateJob()
        {
            #region add user
            var user = new ApplicationUser()
            {
                Name = "Teste User",
                UserName = "teste.user",
                Email = "teste@teste.pt",
            };
            helper.UserManager.CreateAsync(user, "Teste_123");
            #endregion

            var job = new Job
            {
                Abbreviation = "TESTE",
                Description = "Descrição",
                BeginDay = DateTime.Now,
                EndDay = DateTime.Now,
                UserId = helper.UserManager.FindByEmailAsync("teste@teste.pt").Result.Id,
                State = "pending"
            };
            var response = helper.Context.Add(job);
            Assert.AreEqual(response.State, EntityState.Added);
        }


        [TestMethod]
        public void CreateDonationType()
        {
            var type = new DonationType
            {
                DonationTypeName = "TESTE Dinheiro",
            };
            var response = helper.Context.Add(type);
            Assert.AreEqual(response.State, EntityState.Added);
        }

        [TestMethod]
        public Donation CreateDonation()
        {
            #region add user
            var user = new ApplicationUser()
            {
                Name = "Teste User",
                UserName = "teste.user",
                Email = "teste@teste.pt",
            };
            helper.UserManager.CreateAsync(user, "Teste_123");
            #endregion
            #region add Type
            var type = new DonationType
            {
                DonationTypeName = "TESTE",
            };
            var typeResponse = helper.Context.Add(type);
            #endregion

            var donation = new Donation()
            {
                Description = "Doação",
                Quantity = "15",
                DonationType = 1,
                Status = "pending",
                UserId = helper.UserManager.FindByEmailAsync("teste@teste.pt").Result.Id,
            };
            var response = helper.Context.Add(donation);
            Assert.AreEqual(response.State, EntityState.Added);

            return response.Entity;
        }

        [TestMethod]
        public void UpdateDonation()
        {
            var donation = this.CreateDonation();

            donation.Status = "success";
            var response = helper.Context.Update(donation);

            Assert.AreEqual(response.Entity.Status, donation.Status);
        }

        [TestMethod]
        public void DeleteDonation()
        {
            var donation = this.CreateDonation();

            var delete = helper.Context.Donation.Find(donation.Id);
            var response = helper.Context.Remove(delete);
            Assert.AreEqual(response.State, EntityState.Detached);
        }
    }
}
