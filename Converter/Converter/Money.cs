using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class Money
    {
        public Money()
        {
            currency = "PLN";
            amount = 0.00;
        }

        public Money(string name)
        {
            if (name != "PLN" && name != "EUR")
            {
                throw new FormatException();
            }
            currency = name;
            amount = 0.00;
        }

        public Money(string name, double howMuch)
        {      
            if (name != "PLN" && name != "EUR")
            {
                throw new FormatException();
            }
            currency = name;
            amount = howMuch;
        }

        public void ConvertEURIntoPLN()
        {
            if (this.currency != "EUR")
            {
                throw new FormatException();
            }
            this.amount = Math.Floor(this.amount * 4.15);
            this.currency = "PLN";
        }

        public void ConvertPLNIntoEUR(Money currency)
        {
            if (this.currency != "PLN")
            {
                throw new FormatException();
            }
            this.amount = Math.Floor(this.amount / 4.15);
            this.currency = "EUR";
        }

        public string currency { get; set; }

        public double amount { get; set; }
    }
}
