using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZooboxApplication.Models;
using ZooboxApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TestZoobox
{

    [TestClass]
    public class Sprint3Unit
    {
        public string Site = "https://localhost:44381";

        public IConfiguration Configuration { get; }
        [TestMethod]
        public void CreateAnimals()
        {
            TestHelper helper = new TestHelper(true);

            var animal = new Animal();

            animal.Name = "teste Animal";
            animal.Location = "localização";
            animal.Race = 1;
            animal.State = 1;
            animal.Disease = 1;
            animal.EntranceDay = DateTime.Now;

             var response = helper.Context.Add(animal);
           

        }


       
     
}
}
