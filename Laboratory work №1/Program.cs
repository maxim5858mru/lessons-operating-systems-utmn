using System.Diagnostics;
using System.IO;

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
                    throw new ArgumentException("Для работы программы требуется хотя бы один аргумент.", "args");
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

        private static bool CheckFileName(ref string fileName)
        {
            bool correctExtension = (fileName.Substring(fileName.Length - 4) == ".vbs");
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

        private static void Run(string fileName, string[] arguments, string interpreter = "cscript")
        {
            var process = new Process();
            string argumentsLine = '"' + fileName + "\" ";

            foreach (var arg in arguments)
            {
                argumentsLine += '"' + arg + '"' + " ";
            }

            process.StartInfo.FileName = interpreter;
            process.StartInfo.Arguments = argumentsLine;

            Console.WriteLine($"Запуск интерпретатора VBScript. Количество аргументов: {arguments.Length}");
            process.Start();
            process.WaitForExit();
            Console.WriteLine("Процесс успешно был запущен и завершился через " +
                $"{process.ExitTime - process.StartTime}, c кодом возврата {process.ExitCode}.");
        }
    }
}