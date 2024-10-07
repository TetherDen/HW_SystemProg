using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq.Expressions;
using System.Text;

namespace HW_09
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Создайте консольное приложение, которое позволит пользователю решать арифметические выражения.
            //Приложение может иметь следующий вид:

            //Install - Package Microsoft.CodeAnalysis.CSharp.Scripting  // ставим package CSharp.Scripting

            StringBuilder expressionBuilder = new StringBuilder();
            while (true)
            {
                Console.WriteLine($"Write arithmetic expressions (or press 'escp'):");
                expressionBuilder.Clear();

                while (true)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }

                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (expressionBuilder.Length > 0)
                        {
                            expressionBuilder.Length--;
                            Console.Write("\b \b");
                            continue;
                        }
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }

                    expressionBuilder.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }

                string expression = expressionBuilder.ToString();
                if (string.IsNullOrWhiteSpace(expression)) // чтобы пустую строку не обрабатывал резалт
                    continue;

                try
                {
                    var result = await CSharpScript.EvaluateAsync(expression, ScriptOptions.Default.WithImports("System.Math"));
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message}");
                }
            }

            //====================================================================================================

            //Используйте CSharpScript, чтобы позволить пользователю выполнять некоторые операции с текстом:
            //подсчитывать количество слов или символов.
            //Саму переменную с текстом, определить на уровне класса Program(использовать как глобальное значение).

            var res = await CSharpScript.EvaluateAsync<int>("str.Length", globals: new GlobalValues());
            Console.WriteLine(res);

            var res2 = await CSharpScript.EvaluateAsync<int>("str.Split(' ').Length", globals: new GlobalValues());
            Console.WriteLine(res2);

        }
    }
}

public class GlobalValues
{
    public string str = "Lorem ipsum dolor sit amet";

}