using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace hw_01_sys
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        //
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);
        //
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        static void Main(string[] args)
        {
            //Task main
            //  Разработайте приложение по типу «Console Application», 
            //  которое умеет запускать дочерний процесс и ожидать его завершения. 
            //  Когда дочерний процесс завершен, родительское приложение должно отобразить код завершения.

            //Process process = new Process();
            ////process.StartInfo.FileName = "calc.exe";
            //process.StartInfo.FileName = "notepad.exe";

            //process.Start();
            //process.WaitForExit();

            //Console.WriteLine($"The process has been completed with code: {process.ExitCode}");



            // Task 1
            //Разработайте приложение по типу «Console Application», которое использует атрибут DllImport.
            //    Вам необходимо использовать функции Beep и MessageBeep из Windows API. 
            //    С помощью импортированных функций сгенерируйте набор звуковых сигналов через определенные промежутки времени.

            //for (int i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(new Random().Next(100, 300));
            //    Beep(500, 500); // Звук частотой 1000 Гц длительностью 500 мс
            //    Thread.Sleep(new Random().Next(75, 200));
            //    MessageBeep(0x00000040);
            //}


            //task 2
            //Разработайте приложение, которое предоставляет пользователю возможность запуска других приложений. Пользователь может запустить:
            //■ Блокнот; 
            //■ Калькулятор; 
            //■ Paint; 
            //■ Своё собственное другое приложение.

            //Menu();

            //static void Menu()
            //{
            //    while (true)
            //    {
            //        Console.WriteLine("Пользователь может запустить:");
            //        Console.WriteLine("1 - Блокнот; ");
            //        Console.WriteLine("2 - Калькулятор");
            //        Console.WriteLine("3 - Paint");
            //        Console.WriteLine("4 - Своё собственное другое приложение");
            //        Console.WriteLine("0 - break");

            //        Console.WriteLine("\nMake ur choice");
            //        string switch_on = Console.ReadLine();
            //        switch (switch_on)
            //        {
            //            case "1":
            //                InitProcess("notepad.exe");
            //                break;
            //            case "2":
            //                InitProcess("calc.exe");
            //                break;
            //            case "3":
            //                InitProcess("mspaint.exe");
            //                break;
            //            case "4":
            //                Console.WriteLine("Enter ur path to process: ");
            //                string userProcess = Console.ReadLine();
            //                InitProcess(userProcess);
            //                break;
            //            case "0":
            //                return;
            //            default:
            //                Console.WriteLine("try again");
            //                break;
            //        }
            //    }
            //}

            //static void InitProcess(string str)
            //{
            //    try
            //    {
            //        Process.Start(str);
            //        Console.WriteLine($"{str} starter");
            //    }
            //    catch(Exception err)
            //    {
            //        Console.WriteLine(err);
            //    }
            //}

            // task
            //Разработайте приложение по типу «Console Application», которое использует атрибут DllImport.
            //Вам необходимо использовать функцию MessageBox из Windows API. 
            //Пользователь загадывает число в диапазоне от 0 до 100.Компьютер угадывает.Предусмотрите возможность повторной игры.

            int number;
            int max = 20;
            int min = 0;
            List<int> list = new List<int>();
            Random rand = new Random();

            MessageBox(IntPtr.Zero, $"guess a number from {min} to {max} in console.", "Enter your number", 0);
            Console.Write("Write ur number here: ");
            //int number = int.Parse(Console.ReadLine());
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out number) && number >= min && number <= max)
                {
                    MessageBox(IntPtr.Zero, $"You Entered {number}.", "successful", 0);
                    break;
                }
                else
                {
                    MessageBox(IntPtr.Zero, $"Enter a number from {min} to {max}", "Invalid Input", 0);
                }
            }
            int i = 0;
            while (true)
            {
                int guessedNumber;
                do
                {
                    guessedNumber = rand.Next(min, max + 1);
                } while (list.Contains(guessedNumber));

                MessageBox(IntPtr.Zero, $"PC guesses the number {guessedNumber}", $"PC try {i}", 0);
                if (guessedNumber == number)
                {
                    MessageBox(IntPtr.Zero, $"Ur number is {guessedNumber}", $"PC won at try {i}", 0);
                    break;
                }
                else
                {
                    list.Add(guessedNumber);
                    i++;
                    Thread.Sleep(10);
                }
            }


            //task
            //Разработайте приложение, которое умеет запускать дочерний процесс. 
            //В зависимости от выбора пользователя родительское приложение должно ожидать завершения 
            //дочернего процесса и отображать код завершения либо принудительно завершать работу дочернего процесса.

            //Process process = new Process();
            //process.StartInfo.FileName = "notepad.exe";

            //Console.WriteLine("1 - ожидать завершения дочернего процесса.");
            //Console.WriteLine("2 - завершать работу дочернего процесса. ");
            //Console.WriteLine("User choice: ");
            //string choice = Console.ReadLine(); 

            //process.Start();

            //if(choice == "1")
            //{
            //    process.WaitForExit();
            //    Console.WriteLine($"The process has been completed with code: {process.ExitCode}");
            //}
            //else if (choice == "2")
            //{
            //    process.Kill();
            //    Console.WriteLine(" process.Kill()");
            //}
            //else
            //{
            //    Console.WriteLine("Invalid choice.");
            //}













        }
    }
}
