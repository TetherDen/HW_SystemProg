namespace HW_08_add
{
    internal class Program
    {
        unsafe static void Main(string[] args)
        {
            // Напишите программу, в которой объявляется переменная типа double.
            // В область памяти, выделенную под эту переменную, запишите такие значения:
            //   в первый байт запишите значение 1, 
            //   в следующие два байта запишите символ ƍAƍ,
            //   в следующие четыре байта запишите значение 2
            //   и в оставшийся восьмой байт запишите значение 3.

            double num;
            byte* ptr = (byte*)&num; // указатель на первый байт

            for (int i = 0; i < sizeof(double); i++)
            {
                Console.WriteLine($"Byte {i}: {ptr[i]:X2}");
            }

            ptr[0] = 1;  // 1Б
            *((ushort*)(ptr + 1)) = 'A'; // 2Б  utf-16
            *((int*)(ptr + 3)) = 2;   // 4Б    ( при условии инт == 4Б )
            (ptr[7]) = (byte)3;  // 1Б


            Console.WriteLine();
            for (int i = 0; i <= 7; i++)
            {
                //Console.Write(ptr[i] + " ");
                Console.WriteLine($"Byte {i}: {ptr[i]:X2}");
            }
            Console.WriteLine($"num: {num}");
        }
    }
}
