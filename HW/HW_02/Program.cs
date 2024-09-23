namespace HW_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Напишите программу, которая создает несколько потоков, 
            //каждый из которых моделирует датчик температуры в отдельной комнате.
            //Каждый поток должен периодически генерировать и выводить случайные значения температуры для своей комнаты.
            //Программа должна остановить все потоки через заданное время.

            int timeToStop = 5;

            List<Room> roomList = new List<Room>();
            for (int i = 1; i <= 3; i++)
            {
                roomList.Add(new Room(i));
                roomList[i - 1].Start();
            }

            Thread.Sleep(timeToStop * 1000);
            foreach (Room room in roomList)
            {
                room.Stop();
            }


            //=============================================================

            //  Используя потоки создать метод, который находит в строке количество слов, 
            //  которые начинаются и оканчиваются на одну и ту же букву.

            string str = "dwa awd da wa aa dawfesgga ее gg tt";
            Thread thread = new Thread(() =>
            {
                int num = StringWordCounter(str);
                Console.WriteLine($"result: {num}");
            });
            thread.Start();
            thread.Join();

            static int StringWordCounter(string str)
            {
                return str.Split(new char[] { ' ', '?', '!', '.', ',', ':', ';', '"' })
                    .Count(el => el.Length > 1 && char.ToLower(el[0]) == char.ToLower(el[^1]));
            }

            //=============================================================

            //Напишите программу, которая создает несколько касс(потоков), и каждый поток будет обрабатывать очередь покупателей.
            //Каждый покупатель будет обрабатываться за случайное время(от 1 до 3 секунд).
            //Программа должна выводить на экран, когда покупатель начинает и заканчивает обслуживание на каждой кассе.

            //1) Создайте класс Customer, который будет представлять покупателя.
            //2) Создайте класс CashRegister, который будет представлять кассу и будет работать в отдельном потоке.
            //3) Используйте класс Thread для моделирования работы касс.
            //4) Сгенерируйте очередь из 20 покупателей и распределите покупателей между кассами.

            CashRegister cashRegister1 = new CashRegister("FirstCashRegister");
            CashRegister cashRegister2 = new CashRegister("SecondCashRegister");

            for (int i = 1; i < 21; i++)
            {
                if (i % 2 == 0)
                    cashRegister1.addCustomer(new Customer(i.ToString()));
                else
                    cashRegister2.addCustomer(new Customer(i.ToString()));
            }

            cashRegister1.Start();
            Thread.Sleep(4000);
            cashRegister2.Start();

            Thread.Sleep(15000);  // Магазин закроеться через 15 секунд
            cashRegister1.Stop();
            cashRegister2.Stop();

        }

        public class Customer
        {
            public string Name { get; set; }
            public Customer(string name) { Name = name; }
        }

        public class CashRegister
        {
            public string Name { get; set; }
            private List<Customer> _customerList;
            private bool _IsRunning = false;
            private Thread _thread;
            private static Random _random = new Random();

            public CashRegister(string name)
            {
                Name = name;
                _customerList = new List<Customer>();
                _thread = new Thread(CashRegisterWorking);
            }

            public void addCustomer(Customer customer)
            {
                _customerList.Add(customer);
            }
            public void Start()
            {
                _IsRunning = true;
                _thread.Start();
            }

            public void Stop()
            {
                _IsRunning = false;
            }
            private void CashRegisterWorking()
            {
                while(_IsRunning)
                {                   
                    if(_customerList.Count > 0)
                    {
                        var customer = _customerList[0];
                        _customerList.RemoveAt(0);
                        Thread.Sleep(_random.Next(1, 3) * 1000);
                        Console.WriteLine($"{this.Name} - served customer: {customer.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"Касса Свободна! {this.Name}");
                        Thread.Sleep(3000);
                    }
                }
                Console.WriteLine($"{this.Name} is Closed()");
            }
        }
    }
}
