using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Converter;

namespace ConverterTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod, ExpectedException(typeof ( FormatException ) )]
        public void BadCurrencyTest1()
        {
            new Money("CHF");
        }

        [TestMethod, ExpectedException(typeof ( FormatException ) )]
        public void BadCurrencyTest2()
        {
            new Money("CHF", 10);
        }

        [TestMethod]
        public void InitializeTest()
        {
            Converter.Money monies = new Money("PLN");
            monies = new Money("EUR", 10);
            Assert.IsInstanceOfType(monies.amount, typeof(double));
            Assert.IsInstanceOfType(monies.currency, typeof(string));
            Assert.IsNotNull(monies);  
        }

        [TestMethod]
        public void EURtoPLNTest()
        {
            Converter.Money monies = new Money("EUR", 100);
            monies.ConvertEURIntoPLN();
            Assert.AreEqual(monies.amount, 415.00);
            Assert.AreEqual(monies.currency, "PLN");
        }

        [TestMethod]
        public void PLNtoEURTest()
        {
            Converter.Money monies = new Money("PLN", 415);
            monies.ConvertEURIntoPLN();
            Assert.AreEqual(monies.amount, 100.00);
            Assert.AreEqual(monies.currency, "EUR");
        }



        
    }
}
