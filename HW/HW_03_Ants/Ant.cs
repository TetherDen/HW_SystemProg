using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_03_Ants
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
        public int X { get; set; }
        public int Y { get; set; }
        public int Energy { get; set; }
        public int Age { get; set; }
        public AntNest Nest { get; set; }
        public AntRole Role { get; set; }
        private Thread _thread;
        private Random _random = new();
        public bool _Alive = true;

        public Ant(AntRole role)
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

        private void SearchFood()  // metod ant farmer
        {
            int chance = _random.Next(0, 101);
            if (chance >= 50)
            {
                Nest.FoodInNest += 20;
            }
        }

        public void Eat()
        {
            if (Nest.FoodInNest >= 20)
            {
                this.Energy += 50;
                Nest.FoodInNest -= 20;
            }
        }

        private void Working()  // метод worker
        {
            int chance = _random.Next(0, 101);
            if (chance >= 50)
            {
                this.Energy -= 5;
            }
        }

        private void Move() 
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
                Energy -= 10;
                if (Energy <= 0)
                {
                    _Alive = false;
                    Nest.RemoveAnt(this);
                }

                switch (this.Role)
                {
                    case AntRole.Farmer:
                        SearchFood();
                        break;
                    case AntRole.Warrior:
                        //TODO:
                        break;
                    case AntRole.Worker:
                        Working();
                        break;
                }
                if (Energy <= 20)
                {
                    Eat();
                }

                Thread.Sleep(1000);
            }
        }
    }
}
