using System;
using System.Threading;

namespace LW5
{
    internal class Program
    {
        private static double variableY;
        private static double variableX;
        private static Thread firstThread;
        private static Thread secondThread;

        /// <summary>
        /// Ожидание необходимое для нормальной работы программы при использовании 
        /// приостановки и возобновления работы потоков
        /// </summary>
        private const int delay = 40;

        static void Main(string[] args)
        {
            firstThread = new Thread(delegate ()
            {                
                for (int i = 0; i < 100; i++)
                {
                    Thread.CurrentThread.Suspend();
                    variableY = variableX + 2 * Math.Sin(variableX);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Первый поток: i = " + i + "; y = " + variableY);
                    Thread.Sleep(delay);
                    if (i != 99) secondThread.Resume();
                }
            });
            secondThread = new Thread(delegate ()
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.CurrentThread.Suspend();
                    variableX = Math.Cos(variableY);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Второй поток: i = " + i + "; x = " + variableX);
                    Thread.Sleep(delay);
                    firstThread.Resume();
                }
            });

            // Получение начального параметра
            Console.Write("Y: ");
            variableY = int.Parse(Console.ReadLine());
            Console.WriteLine();

            // Запуск и ожидяние подготовки потоков к работе
            firstThread.Start();
            secondThread.Start();
            while (firstThread.ThreadState != ThreadState.Suspended &&
                   secondThread.ThreadState != ThreadState.Suspended) ;
            
            // Запуск вычислений
            Thread.Sleep(delay);
            secondThread.Resume();

            firstThread.Join();
            secondThread.Join();

            // Вывод результата
            Console.ResetColor();
            Console.WriteLine("\n\rX: " + variableX);
            Console.ReadKey();
        }
    }
}