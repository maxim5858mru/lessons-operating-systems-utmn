namespace LW6
{
    internal class Program
    {
        private static int[] data = new int[100];
        private static AutoResetEvent evenFirstThread = new AutoResetEvent(true);
        private static AutoResetEvent evenSecondThread = new AutoResetEvent(false);

        public static void Main()
        {
            var firstThread = new Thread(FirstThreadMethod);
            var secondThread = new Thread(SecondThreadMethod);

            firstThread.Start();
            secondThread.Start();

            firstThread.Join();
            secondThread.Join();
        }

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

        private static void SecondThreadMethod()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt"))
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt");
            
            var stream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\data.txt");

            while (true)
            {
                evenSecondThread.WaitOne();

                for (int i = 0; i < data.Length; i++)
                {
#if false
Console.WriteLine($"#{i}:\t{data[i]} -> {data[i] / 2}");
#endif
                    stream.WriteLine($"{i}: {data[i]} -> {data[i] / 2}");
                    data[i] /= 2;
                }
                stream.WriteLine(new string('-', 80));
                stream.Flush();

                Console.ReadKey();
                evenFirstThread.Set();
            }
        }
    }
}