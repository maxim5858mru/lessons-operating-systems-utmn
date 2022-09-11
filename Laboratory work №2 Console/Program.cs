using System;
using System.Diagnostics;
using System.Collections;

namespace LW2_Console
{
    public class Program
    {
        private delegate double Calculate(double x);

        public static void Main(string[] args)
        {
            double from, to, step;
            int function;

            // Проверка и получение переменных окружения
            var envVars = Environment.GetEnvironmentVariables();
            if (envVars.Contains("LW_OS_FROM") &&
                envVars.Contains("LW_OS_TO") &&
                envVars.Contains("LW_OS_STEP") &&
                envVars.Contains("LW_OS_FUNC") &&
                args.Length < 4)
            {
                args = new string[]
                {
                    envVars["LW_OS_FUNC"].ToString(),
                    envVars["LW_OS_FROM"].ToString(),
                    envVars["LW_OS_TO"].ToString(),
                    envVars["LW_OS_STEP"].ToString()
                };
            }

            if (args.Length == 4)
            {
                // Проверка переданных аргументов программе, через консольную строку
                try
                {
                    function = int.Parse(args[0]);
                    if (function == 0) Environment.Exit(0x00);
                    else if (function < 0 || function > 6) throw new ArgumentException("Ошибка при выборе функции.", "function");

                    from = double.Parse(args[1]);

                    to = double.Parse(args[2]);
                    if (to < from) throw new ArgumentException("Конец диапазона меньше его начала.", "to");

                    step = double.Parse(args[3]);
                    if (step < 0) throw new ArgumentException("Указан шаг меньше нуля.", "step");
                }
                catch (FormatException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("\n\rОшибка при преобразовании аргумента в числовое значение.");
                    Console.Error.WriteLine("Стек вызовов:\n\r" + exception.StackTrace);
                    Environment.Exit(0xA0);                                     // TODO: Нужен код ошибки

                    return;
                }
                catch (ArgumentException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("\n\r" + exception.Message);
                    Console.Error.WriteLine("Стек вызовов:\n\r" + exception.StackTrace);
                    Environment.Exit(0xA0);                                     // TODO: Нужен код ошибки

                    return;
                }
            }
            else if (args.Length == 0)
            {
                do                          // В случае, если будет ошибка при вводе, то будет
                                            // заново начат запрос данных от пользователя
                {
                    // Получение значений переменных с консоли
                    try
                    {
                        Console.WriteLine(@"Доступные функции:
    1. Sin(x)
    2. Cos(x),
    3. x^2, 
    4. e^x, 
    5. Ln(x),
    0. ВЫХОД.");
                        Console.Write("\n\rФункция: ");
                        function = int.Parse(Console.ReadLine());
                        if (function == 0) Environment.Exit(0x00);
                        else if (function < 0 || function > 6) throw new ArgumentException("Ошибка при выборе функции.", "function");

                        Console.Write("От: ");
                        from = double.Parse(Console.ReadLine());

                        Console.Write("До: ");
                        to = double.Parse(Console.ReadLine());
                        if (to < from) throw new ArgumentException("Конец диапазона меньше его начала.", "to");

                        Console.Write("Шаг: ");
                        step = double.Parse(Console.ReadLine());
                        if (step < 0) throw new ArgumentException("Указан шаг меньше нуля.", "step");

                        Console.WriteLine();
                        break;
                    }
                    catch (FormatException exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.Write("\n\rОшибка при преобразовании введённой строки в числовое значение.");
                        Console.ReadKey();
                        Console.ResetColor();

                        Console.WriteLine("\n\r\n\r" + new string('#', 80));
                    }
                    catch (ArgumentException exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.Write("\n\r" + exception.Message);
                        Console.ReadKey();
                        Console.ResetColor();

                        Console.WriteLine("\n\r\n\r" + new string('#', 80));
                    }
                } while (true);

            }
            else
            {
                // Вывод ошибки, в случае если переданно неправильное количество аргументов программе
                try
                {
                    throw new ArgumentException("Программе требуется для работы 4 аргумента.", "args");
                }
                catch (ArgumentException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("\n\r" + exception.Message);
                    Console.Error.WriteLine("Стек вызовов:\n\r" + exception.StackTrace);
                    Environment.Exit(0xFF);                                     // TODO: Нужен код ошибки

                    return;
                }
            }

            PrintCalculation(from, to, step, function);
        }

        /// <summary>
        /// Подсчёт и вывод таблицы с значениями.
        /// </summary>
        /// <param name="from">Начало диапазона</param>
        /// <param name="to">Конец диапазона</param>
        /// <param name="step">Шаг</param>
        /// <param name="function">Номер функции</param>
        /// <exception cref="ArgumentException"></exception>
        private static void PrintCalculation(double from, double to, double step, int function)
        {
            Calculate calc;

            switch (function)
            {
                case 1:
                    calc = x => Math.Sin(x);
                    break;
                case 2:
                    calc = x => Math.Cos(x);
                    break;
                case 3:
                    calc = x => x * x;
                    break;
                case 4:
                    calc = x => Math.Exp(x);
                    break;
                case 5:
                    calc = x => Math.Log(x, Math.E);
                    break;
                default:
                    throw new ArgumentException("Неправильный номер функции.");
            }

            // Заголовок таблицы
            Console.WriteLine(new string('-', 34));
            Console.WriteLine("|\t      x |\t    f(x) |");
            Console.WriteLine(new string('-', 34));

            // Расчёт данных и вывод
            for (double i = from; i <= to; i += step)
            {
                Console.WriteLine(string.Format("|\t{0:000.000} |\t{2}{1:000.000} |", i, calc(i), (calc(i) < 0) ? "" : " "));
            }
            Console.WriteLine(new string('-', 34));
        }
    }
}