namespace HW_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
