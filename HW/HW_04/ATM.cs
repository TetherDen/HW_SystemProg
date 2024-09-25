using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HW_04.Program;

namespace HW_04
{
    public class ATM
    {
        public decimal WithdrawFromBank(Bank bank, decimal value)
        {
            return bank.Withdraw(value);
        }

        public void DepositToBank(Bank bank, decimal value)
        {
            bank.Deposit(value);
        }

    }
}
