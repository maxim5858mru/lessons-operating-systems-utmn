namespace LW8
{
    internal class Program
    {
        /// <summary>
        /// Массив для промежуточного хранения данных
        /// </summary>
        private static int?[] data = new int?[5];

        /// <summary>
        /// Поток для записи данных в файл
        /// </summary>
        private static StreamWriter writer;

        private static Thread firstThread = new(ThreadMethod) { Name = "Первый поток" };
        private static Thread secondThread = new(ThreadMethod) { Name = "Второй поток" };

        public static void Main()
        {
            try
            {
                // Пересоздание файла и создание потока на его основе
                var file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt");
                if (file.Exists) file.Delete();
                writer = file.AppendText();

                firstThread.Start();
                secondThread.Start();

                firstThread.Join();
                secondThread.Join();

            }
            finally
            {
                writer?.Close();
            }

        }

        /// <summary>
        /// Метод выполняемый потоком. Выполняет генерацию 100 чисел с промежуточной 
        /// записью в массив. По заполнению массива перезаписывает их в файл.
        /// </summary>
        private static void ThreadMethod()
        {
            var random = new Random();

            for (int i = 0; i < 50; i++)
            {
                lock(typeof(Program))
                {
                    // Генерация значений
                    for (int y = 0; y < data.Length; y++, i++)
                    {
                        data[y] = random.Next();
                    }

                    // Запись значений
                    for (int j = 0; j < data.Length; j++)
                    {
#if DEBUG
                        Console.WriteLine($"{Thread.CurrentThread.Name}[{(i / 5 - 1) * 5 + j}]: {data[j]}");
#endif
                        writer.WriteLine($"{Thread.CurrentThread.Name}[{(i / 5 - 1) * 5 + j}]: {data[j]}");
                        data[j] = null;
                    }


#if DEBUG
                    Console.WriteLine(new string('-', 50));
#endif
                    writer.WriteLine(new string('-', 50));
                    writer.Flush();
                }

                Thread.Sleep(1);
            }
        }
    }
}