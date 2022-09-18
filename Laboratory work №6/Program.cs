namespace LW6
{
    internal class Program
    {
        private static int[] data = new int[10];
        private static readonly AutoResetEvent evenFirstThread = new(true);
        private static readonly AutoResetEvent evenSecondThread = new(false);

        public static void Main()
        {
            var firstThread = new Thread(FirstThreadMethod);
            var secondThread = new Thread(SecondThreadMethod);

            firstThread.Start();
            secondThread.Start();

            firstThread.Join();
            secondThread.Join();
        }

        /// <summary>
        /// Метод первого потока, выполняющий запись данных в массив
        /// </summary>
        private static void FirstThreadMethod()
        {
            var random = new Random();

            while (true)
            {
                evenFirstThread.WaitOne();

                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = random.Next(0, 100);
                    Console.WriteLine($"{i}:\t{data[i]}");
                }

                Console.WriteLine(new string('-', Console.WindowWidth));
                Console.WriteLine();

                evenSecondThread.Set();
            }
        }

        /// <summary>
        /// Метод второго потока, выполняющий чтение данных из массива и их запись в файл
        /// </summary>
        private static void SecondThreadMethod()
        {
            // Удаление файла для записи для дальнейшего его пересоздания
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt"))
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt");
            
            var stream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt");

            while (true)
            {
                evenSecondThread.WaitOne();

                for (int i = 0; i < data.Length; i++)
                {
                    stream.WriteLine($"{i}: {data[i]} -> {data[i] / 2}");
                    data[i] /= 2;
                }
                stream.WriteLine(new string('-', 80));
                stream.Flush();

                Console.ReadKey();              // Ожидание необходимое, чтобы можно было сделать проверку работы программы
                evenFirstThread.Set();
            }
        }
    }
}