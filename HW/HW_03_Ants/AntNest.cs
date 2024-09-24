using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_03_Ants
{
    public class AntNest
    {
        public int[,] Size { get; private set; }
        public int FoodInNest { get; set; }
        public List<Ant> Ants { get; set; } = new();
        private readonly object _lock = new object();  // для синхронизации доступа к общим ресурсам в многопоточной среде.

        public AntNest(int width, int height)
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

        public void DisplayAntInfo()
        {
            Console.WriteLine("");
            foreach (Ant ant in Ants)
            {
                Console.WriteLine($"Ant {ant.Id}, Energy: {ant.Energy}, Role: {ant.Role}, Age: {ant.Age}");
            }
        }

        public void DisplayNest()
        {
            int centerX = Size.GetLength(0) / 2;
            int centerY = Size.GetLength(1) / 2;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Ants Alive: {Ant.antQuantity}   Food in Nest: {FoodInNest}");
                for (int i = 0; i < Size.GetLength(0); i++)
                {
                    for (int j = 0; j < Size.GetLength(1); j++)
                    {
                        // есть ли муравей в этой позиции
                        Ant antAtPosition = Ants?.Find(a => a.X == i && a.Y == j);
                        if (i == centerX && j == centerY)  // каждый раз будет проверять, плохо
                        {
                            Console.Write("O ");
                        }

                        else if (antAtPosition != null)
                        {
                            //роль муравья
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
                            Console.Write(". ");
                        }

                    }
                    Console.WriteLine();
                }
                DisplayAntInfo();

                Thread.Sleep(1000);
            }
        }
    }
}
