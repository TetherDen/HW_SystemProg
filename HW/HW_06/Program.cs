using System.Net;

namespace HW_06
{
    internal class Program
    {
        // 1
        static async Task<string> GetTextFromFileAsync(string path)
        {
            using(StreamReader reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        //========================================================================
        // 2
        static async Task DownloadFileAsync(string url, string pathToSave)
        {
            using(WebClient wc = new WebClient())
            {
                await wc.DownloadFileTaskAsync(url, pathToSave);
            }
        }

        //=======================================================================
        // 3
        static bool IsPrime(int num)
        {
            if (num <2 ) return false;
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if(num % i == 0) return false;
            }
            return true;
        }

        static async Task<List<int>> PrimeNumbersGenerationAsync(int count)
        {
            return await Task.Run(() =>
            {
                List<int> list = new List<int>();
                int i = 2;
                while(list.Count <count)
                {
                    if(IsPrime(i))
                    {
                        list.Add(i);
                    }
                    i++;
                }
                return list;
            });
        }


        static async Task Main(string[] args)
        {
            //Используя асинхронный метод, считать текст из файла, определить количество символов, результат вывести на экран.

            string filePath = "www.google.com.txt";
            Task<string> task = GetTextFromFileAsync(filePath);
            string context = task.Result;
            Console.WriteLine($"Char nunber in file: {context.Length}");

            //===============================================================================================

            // Одной из задач, которую можно реализовать с помощью класса Task в консольном приложении C#, является загрузка большого файла из Интернета. 
            // Эта задача может занять значительное количество времени и при синхронном выполнении может заблокировать поток пользовательского интерфейса. 
            //Ваша задача, использовать класс Task для асинхронного выполнения загрузки любого файл.

            string urlToDownload = "https://fsx1.itstep.org/api/v1/files/gKadk048Zih_F7Ni3u_TF5MWNnXN3528";
            string pathToSaveFile = "SavedFile.pdf";

            DownloadFileAsync(urlToDownload, pathToSaveFile);
            Console.ReadLine();

            //===============================================================================================

            //Реализуйте асинхронную генерацию списка простых чисел.
            Console.WriteLine("How many Prime numbers to generate ?");
            int count  = int.Parse(Console.ReadLine());

            Task<List<int>> taskResult = PrimeNumbersGenerationAsync(count);
            List<int> primes = taskResult.Result;

            foreach (int i in primes)
            {
                Console.WriteLine(i);
            }


        }
    }
}
