using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void GetBadIDTest()
        {
            Ticket t = new Ticket(-1);
            t.Get(-1);
        }

        [TestMethod]
        public void GetTest()
        {
            Ticket input = new Ticket("Gdansk-Przymorze", "Gdansk-Oliwa", 3, 3, 0);
            input.Insert();

            Ticket output = new Ticket();
            output.Get(input.id); //Test fails, if nothing were found
            input.Remove();
        }

        [TestMethod]
        public void GetAllTest()
        {
            Ticket input = new Ticket("Gdansk-Przymorze", "Gdansk-Oliwa", 3, 3, 0);
            input.Insert();

            List<Ticket> result = input.GetAll();
            if (result.Count == 0)
            {
                Assert.Fail("Nothing received");
            }
            input.Remove();
        }

        [TestMethod]
        public void AddTest()
        {
           Ticket input = new Ticket("Gdansk-Przymorze", "Gdansk-Oliwa", 3, 3, 0);
           input.Insert();
           Assert.IsNotNull(input);
           Assert.AreNotEqual(0, input.id);

           Ticket output = new Ticket();
           output.Get(input.id);

           Assert.AreEqual(input.id,                output.id);
           Assert.AreEqual(input.from,              output.from);
           Assert.AreEqual(input.destination,       output.destination);
           Assert.AreEqual(input.fromZone,          output.fromZone);
           Assert.AreEqual(input.destinationZone,   output.destinationZone);
           Assert.AreEqual(input.price,             output.price);

           input.Remove();
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void RemoveTicketTest()
        {
            Ticket input = new Ticket("Gdansk-Przymorze", "Gdansk-Oliwa", 3, 3, 0);
            input.Insert();
            Assert.IsNotNull(input);
            Assert.AreNotEqual(0, input.id);

            input.Remove();

            Ticket output = new Ticket();
            output.Get(input.id);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Ticket input = new Ticket("Gdansk-Przymorze", "Gdansk-Oliwa", 3, 3, 0);
            input.Insert();
            Assert.AreNotEqual(0, input.id);
            Ticket output = new Ticket("Gdynia-Orlowo", "Gdynia-Redlowo", 5, 5, 0);
            input.Update(output);
           // input.Get(input.id);
            Console.WriteLine(input.id);
            Assert.AreEqual(output.from, input.from);
            Assert.AreEqual(output.destination, input.destination);
            Assert.AreEqual(output.fromZone, input.fromZone);
            Assert.AreEqual(output.destinationZone, input.destinationZone);
            Assert.AreEqual(output.price, input.price);
        }
    }
}
