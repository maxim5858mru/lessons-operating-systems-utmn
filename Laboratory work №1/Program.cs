using System.Diagnostics;

namespace LW1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 0)
                {
                    throw new ArgumentException("Для работы программы требуется хотя бы один аргумент.", nameof(args));
                }
                else if (!CheckFileName(ref args[0]))
                {
                    throw new FileNotFoundException("Указанный файл не существует.", args[0]);
                }
                else
                {
                    var arguments = new string[args.Length - 1];
                    for (int i = 1; i < args.Length; i++)
                    {
                        arguments[i - 1] = args[i];
                    }

                    Run(args[0], arguments);
                }
            }
            catch (ArgumentException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(exception.Message);
                Console.Error.WriteLine("Стек вызовов:\n\r" + exception.StackTrace);
                Environment.Exit(0xA0);
            }
            catch (FileNotFoundException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(exception.Message);
                Console.Error.WriteLine("Стек вызовов:\n\r" + exception.StackTrace);
                Environment.Exit(0x50);
            }
        }

        /// <summary>
        /// Проверка существования файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Существует ли файл</returns>
        private static bool CheckFileName(ref string fileName)
        {
            var correctExtension = fileName[^4..] == ".vbs";
            if (correctExtension && File.Exists(fileName))
            {
                return true;
            }
            else if (File.Exists(fileName + ".vbs"))
            {
                fileName += ".vbs";
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Запуск дочернего процесса
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="arguments">Параметры передаваемые процессу</param>
        /// <param name="interpreter">Название интерпретатора</param>
        private static void Run(string fileName, string[] arguments, string interpreter = "cscript")
        {
            var process = new Process();
            var argumentsLine = '"' + fileName + "\" ";

            // Формирование строки аргументов
            foreach (var arg in arguments)
            {
                argumentsLine += '"' + arg + '"' + " ";
            }

            // Настройка процесса
            process.StartInfo.FileName = interpreter;
            process.StartInfo.Arguments = argumentsLine;

            // Запуск процесса
            Console.WriteLine($"Запуск интерпретатора VBScript. Количество аргументов: {arguments.Length}");
            process.Start();
            Console.ForegroundColor = ConsoleColor.Blue;
            process.WaitForExit();
            Console.ResetColor();
            Console.WriteLine("Процесс успешно был запущен и завершился через " +
                $"{process.ExitTime - process.StartTime}, c кодом возврата {process.ExitCode}.");
        }
    }
}