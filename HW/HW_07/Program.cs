using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace HW_07
{
    internal class Program
    {
        internal class Users
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }

        static async Task<List<Users>> GetUsersFromDbAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));   // 5 сек
            CancellationToken token = cts.Token;

            using (ApplicationContext db = new ApplicationContext())
            {
                Thread.Sleep(7000);     // Cимуляция зависания сервера //
                List<Users> users = await db.Users.ToListAsync(token);
                return users;
            }
        }

        //=================================================================
        // 2
        static async Task ThrowIfDuplicates(string[] args)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < args.Length; i++)
                {
                    for (int j = i + 1; j < args.Length; j++)
                    {
                        if (args[i] == args[j])
                        {
                            throw new Exception($"Exception thrown: duplicate string found - '{args[i]}' at index {i} and {j}.");
                        }
                    }
                }
                foreach (string arg in args)
                {
                    Console.WriteLine(arg);
                }
            });
        }


        static async Task Main(string[] args)
        {
            // Необходимо загрузить данные о пользователе с сервера или локальной базы данных, используя асинхронный метод.
            //  Если в процессе загрузки возникает ошибка(например, сервер не отвечает), необходимо обработать эту ошибку и сообщить пользователю о проблеме.
            //  Выполните симуляцию зависания сервера и отмените операцию, если запрос к серверу выполняется дольше 10 секунд.

            ////CancellationTokenSource cts = new CancellationTokenSource(5000);
            ////CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            ////CancellationToken token = cts.Token;

            List<Users> users = new();

            try
            {
                users = await GetUsersFromDbAsync();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Operation canceled due to timeout...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            foreach (var item in users)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Login);
                Console.WriteLine(item.Password);
            }

            //====================================================================

            //  Обработать сразу несколько исключений в вашей программе.
            //  Создать асинхронный метод, для принятия массива типа string.
            //    Вызвать данный метод, минимум 3 раза, передавая разные имена людей. 
            //    В методе, в случае наличия одинаковых имен, генерировать ошибку, если нет повторяющихся имен, выводить их в консоль.
            //    После выполнения всех методов, вывести полученные ошибки в консоль.

            string[] names1 = new string[] { "John", "Joe", "Joe" }; 
            string[] names2 = new string[] { "John", "Joe", "James" };
            string[] names3 = new string[] { "Jack", "J", "Jack" };


            Task allTasks = null;
            try
            {
                Task t1 = Task.Run(() => ThrowIfDuplicates(names1));
                Task t2 = Task.Run(() => ThrowIfDuplicates(names2));
                Task t3 = Task.Run(() => ThrowIfDuplicates(names1));
                Task t4 = Task.Run(() => ThrowIfDuplicates(names3));
                allTasks = Task.WhenAll(t1, t2, t3, t4);
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptions:");          
                Console.WriteLine($"Target Site: {allTasks.Exception.InnerException.TargetSite}");
                Console.WriteLine(allTasks.Exception.InnerExceptions);
                foreach (var inx in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Internal Exception: " + inx.Message);                  
                }
            }

            //====================================================================

            //Создать программу, переписывающую в текстовый файл g содержимое файла f, 
            //исключая пустые строки, а остальные дополнить справа пробелами или ограничить до n символов.

            string pathIn = @"C:\Users\admin\source\repos\System_Programming\HW_sys\HW\HW_07\Lorem.txt";
            string pathOut = @"C:\Users\admin\source\repos\System_Programming\HW_sys\HW\HW_07\NewFileG.txt";
            List<string> result = new();

            using (StreamReader sr = new StreamReader(pathIn))
            {
                string line;
                while (sr.Peek() != -1)
                {
                    line = sr.ReadLine();
                    if (line != null && line.Length > 0 && line != " ")
                    {
                        line += " ";
                        result.Add(line);
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                foreach (string line in result)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}




