using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HW_04.Program;

namespace HW_04
{
    public class ProgressBar
    {
        public static List<ProgressBar> bars = new();
        private readonly object _lock = new object();
        private Random _random = new Random();
        private Thread _thread;
        private static Thread _threadDisplay;
        private int _counter;
        public static int barsCreated = 0;

        public ProgressBar()
        {
            barsCreated++;
            _thread = new Thread(Go) { Name = barsCreated.ToString() };
            bars.Add(this);
            if (_threadDisplay == null)
            {
                _threadDisplay = new Thread(Display);
                _threadDisplay.Start();
            }
        }
        public void Start()
        {
            _thread.Start();
        }
        public void Join()
        {
            _thread.Join();
        }
        private ConsoleColor GetRandomColor()
        {
            return ConsoleColors.Colors[_random.Next(ConsoleColors.Colors.Length)];
        }

        public void Go()
        {
            int sleep;
            while (_counter <= 50)
            {
                _counter += _random.Next(1, 5);
                sleep = _random.Next(150, 300);
                Thread.Sleep(sleep);
            }
        }

        //public void Display()
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        foreach (var bar in bars)
        //        {
        //            Console.Write(bar._thread.Name);
        //            Console.ForegroundColor = GetRandomColor();
        //            Console.WriteLine($"  {new string('=', bar._counter)}");
        //            Console.ResetColor();
        //        }
        //        Thread.Sleep(300);
        //        if (bars.TrueForAll(b => b._counter >= 50))
        //            break;
        //    }
        //}

        public void Display()
        {
            while (true)
            {
                Console.Clear();
                foreach (var bar in bars)
                {
                    Console.Write(bar._thread.Name);
                    for (int i = 0; i < bar._counter; i++)
                    {
                        Console.ForegroundColor = GetRandomColor();
                        Console.Write("=");
                        Console.ResetColor();
                    }
                    Console.WriteLine($" ");
                }
                Thread.Sleep(200);
                if (bars.TrueForAll(b => b._counter >= 50))
                    break;
            }
        }
    }
}
