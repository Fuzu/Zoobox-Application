using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZooboxApplication.Controllers.Animals;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Animals;
using ZooboxApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Controllers;
using ZooboxApplication.Controllers.Donations;
using ZooboxApplication.Models.Donations;

namespace TestZoobox
{

    [TestClass]
    public class Sprint3UnitController
    {

        private TestHelper helper;

        public Sprint3UnitController()
        {
            TestHelper _helper = new TestHelper(true);
            helper = _helper;
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateAnimalsAsync()
        {

            var animal = new Animal();
            animal.Name = "teste Animal";
            animal.Location = "localização";
            animal.Race = 1;
            animal.State = 1;
            animal.Disease = 1;
            animal.EntranceDay = DateTime.Now;
            var controller = new AnimalsController(helper.Context, null, null);
            var result = await controller.Create(animal);
            Assert.AreNotEqual(result.ToString(), null);

        }
        [TestMethod]
        public async System.Threading.Tasks.Task UpdateAnimalsAsync()
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
            var controller = new AnimalsController(helper.Context, null, null);
            var result = await controller.Edit(1, animalUpdate);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DetailsAnimalsAsync()
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
            var controller = new AnimalsController(helper.Context, null, null);
            var result = await controller.Details(1);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteAnimalsAsync()
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
            var controller = new AnimalsController(helper.Context, null, null);
            var result = await controller.Delete(1);
            Assert.AreNotEqual(result.ToString(), null);

        }


        [TestMethod]
        public async System.Threading.Tasks.Task UpdateUserAsync()
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
            var controller = new UsersController(helper.UserManager, helper.Context, helper.RoleManager);
            var result = await controller.Edit(findUser.Id, findUser);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DetailsUserAsync()
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
            var controller = new UsersController(helper.UserManager, helper.Context, helper.RoleManager);
            var result = await controller.Details(findUser.Id);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteUserAsync()
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
            var controller = new UsersController(helper.UserManager, helper.Context, helper.RoleManager);
            var result = await controller.Delete(findUser.Id);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateDiseaseAsync()
        {
            var disease = new DiseaseAnimal()
            {
                DiseaseName = "Doença Teste",
            };
            var controller = new DiseaseAnimalsController(helper.Context);
            var result = await controller.Create(disease);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateRaceAsync()
        {
            var race = new Race()
            {
                RaceName = "Raça Teste"
            };
            var controller = new RacesController(helper.Context);
            var result = await controller.Create(race);
            Assert.AreNotEqual(result.ToString(), null);

        }


        [TestMethod]
        public async System.Threading.Tasks.Task CreateSpecieAsync()
        {
            var species = new Specie()
            {
                SpecieName = "Especie Teste"
            };
            var controller = new SpeciesController(helper.Context);
            var result = await controller.Create(species);
            Assert.AreNotEqual(result.ToString(), null);
        }


        [TestMethod]
        public async System.Threading.Tasks.Task CreateStateAsync()
        {
            var state = new State()
            {
                StateName = "Activo Teste"
            };
            var controller = new StatesController(helper.Context);
            var result = await controller.Create(state);
            Assert.AreNotEqual(result.ToString(), null);
        }


        [TestMethod]
        public async System.Threading.Tasks.Task CreateJobAsync()
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
            var controller = new JobsController(helper.Context, helper.IEmailSender);
            var result = await controller.Create(job);
            Assert.AreNotEqual(result.ToString(), null);
        }


        [TestMethod]
        public async System.Threading.Tasks.Task CreateDonationTypeAsync()
        {
            var type = new DonationType
            {
                DonationTypeName = "TESTE Dinheiro",
            };
            var controller = new DonationTypesController(helper.Context, helper.IEmailSender);
            var result = await controller.Create(type);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateDonationAsync()
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
            var controller = new DonationsController(helper.Context);
            var result = await controller.Create(donation);
            Assert.AreNotEqual(result.ToString(), null);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task UpdateDonationAsync()
        {
            var sprint = new Sprint3Unit();
            var donation = sprint.CreateDonation();
            donation.Status = "success";

            var controller = new DonationsController(helper.Context);
            var result = await controller.Create(donation);
            Assert.AreNotEqual(result.ToString(), null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteDonationAsync()
        {
            var donation = this.CreateDonationAsync();

            var delete = helper.Context.Donation.Find(donation.Id);
            var controller = new DonationsController(helper.Context);
            var result = await controller.Delete(donation.Id);
            Assert.AreNotEqual(result.ToString(), null);
        }
    }
}
