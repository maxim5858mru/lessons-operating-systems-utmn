namespace LW4
{
    internal class Program
    {
        /// <summary>
        /// Путь к файлу для вывода таблицы.
        /// </summary>
        private static readonly string file = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\tread1.dat";

        public static void Main()
        {
            var tableParams = new TableParams();
            var firstThread = new Thread(new ParameterizedThreadStart(PrintTable));
            var secondThread = new Thread(delegate (object? thread)
            {
                var writer = thread as Thread;
                var reader = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                try
                {
                    // Ожидание окончания пользовательского ввода и очистки файла
                    Thread.Sleep(10);
                    while (writer.ThreadState == ThreadState.WaitSleepJoin) Thread.Sleep(1);
                    
                    // Чтение файла
                    while (!reader.EndOfStream || writer.ThreadState != ThreadState.Stopped)
                    {
                        //Console.WriteLine(reader.ReadLine());
                        if (!reader.EndOfStream) Console.WriteLine(reader.ReadLine());
                        Thread.Sleep(500);
                    }
                }
                finally
                {
                    reader?.Close();
                }
            });

            // Получение первых 3-х параметров
            Console.Write("От (X1): ");
            tableParams.From = double.Parse(Console.ReadLine());

            Console.Write("Шаг (S1): ");
            tableParams.Step = double.Parse(Console.ReadLine());

            Console.Write("Задержка (D1): ");
            tableParams.Delay = int.Parse(Console.ReadLine());

            // Запуск потоков
            firstThread.Start(tableParams);             // Поток выполняющий запись в файл
            secondThread.Start(firstThread);            // Поток считывающий данные из файла и выводящий их на консоль
            firstThread.Join();
            secondThread.Join();
        }

        /// <summary>
        /// Метод для реализации потока с использованием статического метода класса.
        /// Выполняет создание и запись таблицы в файл.
        /// </summary>
        /// <param name="data">Параметры таблицы в виде объекта класса TableParams</param>
        private static void PrintTable(object data)
        {
            var tableParams = data as TableParams;
            var writer = new StreamWriter(file, false);

            try
            {
                // Поток созданный из метода экземпляра
                var thirdThread = new Thread(new ThreadStart(tableParams.GetCount));
                thirdThread.Start();
                thirdThread.Join();

                // Создание и запись таблицы
                for (int i = 0; i < tableParams.Count; i++)
                {
                    var date = DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss:fff");
                    var x = tableParams.From + i * tableParams.Step;
                    var y = Math.Sin(x);

                    writer.WriteLine("{0} «Значение x: {1}» «Значение y: {2}»", date, x, y);
                    writer.Flush();
                    
                    Thread.Sleep(tableParams.Delay);
                }

            }
            finally
            {
                writer?.Close();
            }
        }
    }

    /// <summary>
    /// Класс для хранения и передачи потоку параметров таблицы
    /// </summary>
    class TableParams
    {
        public int Count { get; set; }
        public double From { get; set; }
        public double Step { get; set; }
        public int Delay { get; set; }
        
        public void GetCount()
        {
            Console.Write("Количество (N1): ");
            Count = int.Parse(Console.ReadLine());
            Console.WriteLine();
        }
    }
}
