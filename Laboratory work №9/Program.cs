namespace LW9
{
    internal class Program
    {
        /// <summary>
        /// Счётчик сигарет
        /// </summary>
        private static int count = 0;

        // Семафоры
        private static readonly Semaphore removeSemaphore = new(0, 10);
        private static readonly Semaphore addSemaphore = new(10, 10);

        // Потоки
        private static readonly Thread addThread = new(AddThreadMethod);
        private static readonly Thread firstRemoveThread = new(RemoveThreadMethod) { Name = "First Thread" };
        private static readonly Thread secondRemoveThread = new(RemoveThreadMethod) { Name = "Second Thread" };
        private static readonly Thread thirdRemoveThread = new(RemoveThreadMethod) { Name = "Third Thread" };

        // Константы ожидания, чтобы можно было увидеть результат
        private static readonly int addTime = 1000;
        private static readonly int removeTime = 100;

        private static void Main()
        {
            firstRemoveThread.Start(ConsoleColor.Red);
            secondRemoveThread.Start(ConsoleColor.Blue);
            thirdRemoveThread.Start(ConsoleColor.Yellow);

            addThread.Start();
            addThread.Join();
        }

        /// <summary>
        /// Метод потока, выполняющий роль «слуги», который добавляет сигареты
        /// </summary>
        private static void AddThreadMethod()
        {
            while (true)
            {
                // Несём сигареты
                addSemaphore.WaitOne();
                Thread.Sleep(addTime);
                
                // Вывод на консоль
                lock (typeof(Program))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Слуга: {count}++");
                    Console.ResetColor();
                }
                
                count++;
                removeSemaphore.Release();
            }
        }

        /// <summary>
        /// Метод потока, выполняющего роль «курильщика», который курит сигареты
        /// </summary>
        /// <param name="obj">Цвет вывода текста консоли</param>
        private static void RemoveThreadMethod(object? obj)
        {
            var color = obj as ConsoleColor?;
            
            while (true)
            {
                // Докуриваем и призываем слугу
                removeSemaphore.WaitOne();
                Thread.Sleep(removeTime);

                // Вывод на консоль
                lock (typeof(Program))
                {
                    Console.ForegroundColor = color ?? ConsoleColor.White;
                    Console.WriteLine($"Хозяин \"{Thread.CurrentThread.Name}\": {count}--");
                    Console.ResetColor();
                }

                count--;
                addSemaphore.Release();
            }
        }
    }
}