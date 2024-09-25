using System.Diagnostics.Metrics;

namespace HW_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Предположим, у вас есть консольное приложение, имитирующее банк с несколькими банкоматами.
            //    Каждый банкомат позволяет клиентам снимать деньги со своих счетов.
            //    Однако если два клиента попытаются снять деньги одновременно, существует риск того, что основной баланс в банке,
            //    станет отрицательным, в итоге выдача средств клиенту засчитается, но купюр он не получит.

            // Чтобы предотвратить это, вы можете использовать класс Monitor для синхронизации доступа к переменной баланса счета
            Bank bank = new(10000);
            Random random = new Random();
            decimal money = 10;
            List<ATM> atms = new List<ATM>
            {
                new ATM(),
                new ATM(),
                new ATM(),
                new ATM(),
                new ATM(),
            };

            List<Thread> threads = new List<Thread>();
            
            foreach(var atm in atms)
            {
                Thread thread = new Thread(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        atm.WithdrawFromBank(bank, money);
                        //atm.DepositToBank(bank, money);
                    }
                });
                threads.Add(thread);
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine($"Bank balance: {bank.Balance}");

            //=======================================

            //Создайте приложение «Танцующие прогресс-бары». 
            //Приложение отображает набор прогресс-баров.Их количество определяется пользователем.
            //По нажатию на кнопку прогресс - бары начинают заполняться(величины процесса заполнения и цвет определяются случайным образом). 
            //Используйте механизм многопоточности.

            Console.WriteLine("\nHow many Bars will Go?");
            int count = int.Parse(Console.ReadLine());
            List<ProgressBar> bars = new();

            for (int i = 0;i < count;i++)
            {
                bars.Add(new ProgressBar());
            }

            foreach (var bar in bars)
            {
                bar.Start();
            }
            foreach (var bar in bars)
            {
                bar.Join();
            }
        }
    }
}
