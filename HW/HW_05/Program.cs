using System;
using System.Diagnostics;

namespace HW_05
{
    internal class Program
    {
        private static object _locker = new object();

        public class UrlContent
        {
            public string Url { get; set; }
            public string Content { get; set; }
            public override string ToString()
            {
                return $"Url: {Url} \n{Content}";
            }
        }

        static UrlContent FetchUrl(string url, CancellationToken token)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = httpClient.GetAsync(url, token).Result;
                    response.EnsureSuccessStatusCode();
                    //Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");  // Для тестов, убрать
                    //Thread.Sleep(3000);       // Для тестов, убрать
                    var content = response.Content.ReadAsStringAsync().Result;
                    return new UrlContent { Url = url, Content = content };
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine($"Fetch() Canceled at {url}:  {e.Message}");
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error FetchUrl() {url}: {e.Message}");
                    return null;
                }
            }
        }

        static List<UrlContent> ReadUrls(List<UrlContent> list, CancellationToken token, params string[] urls)
        {
            Parallel.ForEach(urls, url =>
            {
                UrlContent content = FetchUrl(url, token);
                lock (_locker)
                {
                    if (content != null)
                    {
                        list.Add(content);
                    }
                }
            });
            return list;
        }

        static void WriteUrlToFile(ICollection<UrlContent> urlContents)
        {
            foreach (var el in urlContents)
            {
                var uri = new Uri(el.Url);  // извлекает имя из URL
                string fileName = $"{uri.Host}.txt";
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(el.Content);
                }
                Console.WriteLine($"{el.Url} writed to {fileName}");
            }
        }

        static void Main(string[] args)
        {
            //  У вас есть список URL - адресов, которые нужно загрузить параллельно
            //    (загрузить содержимое страницы), используя несколько потоков на C#. 
            //    В программе должна быть возможность отменить операцию, если она занимает 
            //    слишком много времени или если пользователь решит ее отменить. 
            //    Напишите программу, которая загружает файлы, используя TPL и CancellationToken.

            string[] urls =
            {
                "https://www.google.com/",
                "https://dou.ua/",
                "https://www.w3schools.com/",
                "https://bitcoinmarket.global/en/"

            };
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            List<UrlContent> resultList = new();

            Console.WriteLine("Started to read urls...");
            Task task = Task.Run(() => ReadUrls(resultList, token, urls), token);

            //  отмена через '1'
            Task.Run(() =>
            {
                Console.WriteLine(@"Enter '1' to Cancel");
                string check = Console.ReadLine();
                if (check == "1")
                {
                    cts.Cancel();
                }
            });

            task.Wait();
            if (task.IsCompleted)
            {
                Console.WriteLine($"Task {task.Id} is completed.");
            }

            Task task2 = Task.Run(() =>
            {
                WriteUrlToFile(resultList);
            });
            task2.Wait();

            //for (int i = 0; i < resultList.Count; i++)
            //{
            //    Console.Write(resultList[i]);
            //}


            //========================================================================

            //  У вас есть массив целых чисел, и вам необходимо вычислить сумму всех элементов массива.
            //  Напишите параллельную программу для вычисления суммы с использованием нескольких объектов класса Task.

            int sum=0;
            int arrSize = 1000000;
            int[] arr = new int[arrSize];
            int taskCount = 10;
            Task<int>[] tasks = new Task<int>[taskCount];
            int arrParts = arrSize / taskCount;

            for (int i = 0; i < arrSize; i++)
            {
                arr[i] = i;
            }

            for (int i = 0; i < taskCount ; i++)
            {
                int start = i * arrParts;
                int end = (i + 1) * arrParts;

                tasks[i] = Task.Run(() =>
                {
                    int localSUm = 0;
                    for (int j = start; j < end; j++)
                    {
                        localSUm += arr[j];
                    }
                    return localSUm;
                });
            }

            Task.WaitAll(tasks);

            for (int i = 0; i < taskCount; i++)
            {
                sum += tasks[i].Result;
            }
            Console.WriteLine($"\nResult sum: {sum}");


            //========================================================================


            //Используя класс «Array», выполнить 3 сортировки 3 - ех массивов по 50000000 значений.Замерить время выполнения.
            //    После, разбить задачу сортировки на 3 отдельных Task, замерить время.Сравнить полученные результаты.

            int size = 50000000;
            int[] arr1 = new int[size];
            int[] arr2 = new int[size];
            int[] arr3 = new int[size];
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                arr1[i] = rand.Next(1, 99999);
                arr2[i] = rand.Next(1, 99999);
                arr3[i] = rand.Next(1, 99999);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Array.Sort(arr1);
            Array.Sort(arr2);
            Array.Sort(arr3);
            sw.Stop();
            Console.WriteLine($"Sorting time: {sw.Elapsed}");

            for (int i = 0; i < size; i++)
            {
                arr1[i] = rand.Next(1, 99999);
                arr2[i] = rand.Next(1, 99999);
                arr3[i] = rand.Next(1, 99999);
            }

            sw.Restart();
            Task task11 = Task.Run(() =>  Array.Sort(arr1));
            Task task22 = Task.Run(() =>  Array.Sort(arr2));
            Task task33= Task.Run(() =>  Array.Sort(arr3));
            Task.WaitAll(task11,task22,task33);
            sw.Stop();
            Console.WriteLine($"Sorting Tasks: {sw.Elapsed}");
        }
    }
}
