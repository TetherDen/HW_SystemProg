using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_02
{
    public class Room
    {
        private int _roomNumber;
        private bool _temp = true;
        private Thread _thread;
        private static Random _random = new();

        public Room(int number)
        {
            _roomNumber = number;
            _thread = new Thread(TempMonitoring);
        }
        public void Start()
        {
            _thread.Start();
        }
        public void Stop()
        {
            _temp = false;
        }

        private void TempMonitoring()
        {
            while (_temp)
            {
                Thread.Sleep(_random.Next(0, 5) * 400);
                Console.WriteLine($"Room {_roomNumber}, temp - {_random.Next(0, 30)}");
            }
        }
    }
}
