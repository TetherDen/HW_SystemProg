using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_04
{
    public class Bank
    {
        private readonly object _lock = new object();
        public decimal Balance { get; private set; }

        public Bank(decimal balance)
        {
            Balance = balance;
        }

        public void Deposit(decimal value)
        {
            if (value > 0)
            {
                lock (_lock)
                {
                    Balance += value;
                }
            }
        }

        public decimal Withdraw(decimal value)
        {
            if (value > 0)
            {
                lock (_lock)
                {
                    if (value <= Balance)
                    {
                        Balance -= value;
                        return value;
                    }
                }
            }
            return 0;
        }
    }
}
