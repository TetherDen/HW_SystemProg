namespace HW_03_Ants
{
    internal class Program
    {
        public enum AntRole
        {
            Farmer,
            Worker,
            Warrior,
        }
        public class Ant
        {
            public static int antQuantity = 0;
            public int Id { get; private set; }
            public int X {  get; set; }
            public int Y { get; set; }
            public int Energy { get; set; } // public ?
            public int Age { get; set; }
            public AntNest Nest { get; set; }
            public AntRole Role { get; set; }
            private Thread _thread;
            private Random _random = new();
            public bool _Alive = true;
            
            public Ant ( AntRole role)
            {
                Id = ++antQuantity;
                X = 0;
                Y = 0;
                Energy = 100;
                Age = 0;
                Role = role;
                Nest = null;
            }
            public void Kill()
            {
                _Alive = false;
            }

            public void StartMoving()
            {
                _thread = new Thread(Move);
                _thread.Start();
            }

            private void Move()  // Доработать Выход Марвья и его жизнь
            {
                int newX, newY;
                while (_Alive)
                {
                    newX = X + _random.Next(-1, 2);
                    newY = Y + _random.Next(-1, 2);

                    if (newX >= 0 && newX < Nest.Size.GetLength(0) && newY >= 0 && newY < Nest.Size.GetLength(1))
                    {
                        // проверка есть ли в клетке другой ант
                        if (Nest.Ants.Find(a => a.X == newX && a.Y == newY) == null)
                        {
                            X = newX;
                            Y = newY;
                        }
                    }

                    Energy -= 15;
                    if (Energy <= 0)
                    {
                        _Alive = false;
                        Nest.RemoveAnt(this);
                    }
                    Thread.Sleep(1000);
                }
            }
        }


        public class AntNest
        {
            public int[,] Size {  get; private set; }
            public List<Ant> Ants { get;  set; } = new();
            private readonly object _lock = new object();  // для синхронизации доступа к общим ресурсам в многопоточной среде. гарантируя, что только один поток может войти в критическую секцию кода в один момент времени.

            public AntNest ( int width, int height)
            {
                Size = new int[width, height];
            }

            public void AddAnt(params Ant[] ants)
            {
                foreach (var ant in ants)
                {
                    ant.X = Size.GetLength(0) / 2;
                    ant.Y = Size.GetLength(1) / 2;
                    ant.Nest = this;    //ссылка на муравейник
                    Ants.Add(ant);
                }
            }

            public void RemoveAnt(Ant ant)
            {
                lock (_lock)
                {
                    if (Ants.Contains(ant))
                    {
                        Ants.Remove(ant);
                        Ant.antQuantity--;
                    }
                }
            }

            public void NestAwake()
            {
                foreach (Ant ant in Ants)
                {
                    ant.StartMoving();
                }
            }

            public void DisplayNest()
            {
                int centerX = Size.GetLength(0) / 2;
                int centerY = Size.GetLength(1) / 2;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Ants Alive: {Ant.antQuantity}");
                    for (int i = 0; i < Size.GetLength(0); i++)
                    {
                        for (int j = 0; j < Size.GetLength(1); j++)
                        {

                            // есть ли муравей в этой позиции
                            Ant antAtPosition = Ants?.Find(a => a.X == i && a.Y == j);

                            if (i == centerX && j == centerY)  // каждый раз будет проверять ето, надо фикс
                            {
                                Console.Write("O ");
                            }

                            //else if (antAtPosition != null && antAtPosition._Alive)
                            else if (antAtPosition != null)
                            {
                                // выводим роль муравья
                                char displayChar = antAtPosition.Role switch
                                {
                                    AntRole.Farmer => 'F',
                                    AntRole.Worker => 'W',
                                    AntRole.Warrior => 'S',
                                    _ => '.'   // => дефолт
                                };
                                Console.Write(displayChar + " ");
                            }
                            else
                            {
                                Console.Write(". "); // Или выводим .
                            }

                        }
                        Console.WriteLine();
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        static void Main(string[] args)
        {
            //Представим себе искусственный муравейник. В нем живут виртуальные муравьи, которые выполняют различные задачи:
            //добывают пищу, строят муравейник, ухаживают за маткой и т.д.
            //Каждый муравей имеет свои характеристики: энергия, возраст, роль в колонии.

            //Реализуйте симуляцию муравейника, используя многопоточность.

            //Функционал:
            //⦁	Мир: Создать двумерный массив, представляющий муравейник и окружающую среду.

            //⦁	Муравей:
            //⦁	Имеет координаты, энергию, возраст, роль.
            //⦁	Может перемещаться по миру, искать пищу, строить муравейник, ухаживать за маткой.
            //⦁	Имеет определенную логику поведения, основанную на его состоянии и окружающей среде.

            //⦁	Многопоточность:
            //⦁	Каждый муравей должен быть представлен отдельным потоком или задачей.

            //⦁	Визуализация:
            //⦁	Реализовать простую визуализацию муравейника, чтобы наблюдать за поведением муравьев.
            AntNest nest = new AntNest(10, 10);

            Ant ant1 = new Ant(AntRole.Worker);
            Ant ant2 = new Ant(AntRole.Worker);
            Ant ant3 = new Ant(AntRole.Farmer);
            Ant ant4 = new Ant(AntRole.Farmer);
            Ant ant5 = new Ant(AntRole.Warrior);

            nest.AddAnt(ant1, ant2, ant3, ant4, ant5);

            nest.NestAwake();
            nest.DisplayNest();

            //Console.WriteLine($"ant1: {ant1.Id}, ant2: {ant2.Id}");
            //Console.WriteLine($"Ants Quantity {Ant.antQuantity}");
        }
    }
}
