namespace LW7
{
    internal class Program
    {
        /// <summary>
        /// Массив для промежуточного хранения данных
        /// </summary>
        private static int?[] data = new int?[10];

        /// <summary>
        /// Поток для записи данных в файл
        /// </summary>
        private static StreamWriter writer;
        
        private static Mutex mutex = new();
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

            for (int i = 0; i < 100; i++)
            {
                mutex.WaitOne();

                // Генерация значений
                for (int y = 0; y < data.Length; y++, i++)
                {
                    if (data[i % data.Length] == null) data[i % data.Length] = random.Next();
                    else i--;
                }

                // Запись значений
                for (int j = 0; j < data.Length; j++)
                {
                    writer.WriteLine($"{Thread.CurrentThread.Name}[{(i / 10 - 1) * 10 + j}]: {data[j]}");
                    data[j] = null;
                }

                writer.WriteLine(new string('-', 50));
                writer.Flush();
                mutex.ReleaseMutex();
            }
        }
    }
}