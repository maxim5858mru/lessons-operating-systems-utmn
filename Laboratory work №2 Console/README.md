# Лабораторная работа №2 Создание процесса с перенаправленным вводом и выводом

**Цель работы**: Освоить технологию перенаправления стандартного ввода и вывода дочернего процесса.

## Задание

### Основная программа

Напишите программу, запрашивающую выбор функции по номеру, расчётный диапазон (*Xмин*, *Xмакс*, *шаг*), и выводяющую на
консоль значение выборанной функции в этом интервале с шагом.

Номера функций:

1. *sin(x)*;
2. *cos(x)*;
3. *x^2*;
4. *e^x*;
5. *ln(x)*;
6. *Выход*.

### Дополнительная программа

**Вариант №6**: Напишите программу, которая строит график функции *tan(x)* в заданном интервале с заданным шагом
используя общую программу.

## Теоретическая часть

При создании процесса, родительский процесс может перенаправить стандартные подоки дочернего процесса (ввод, вывод,
ошибки). Для перенаправления перед вызовом метода `Start` необходимо установить значения свойств `RedirectStandardInput`
, `RedirectStandardOutput` или `RedirectStandardError` в значение `true`.

Свойство `StandardInput` представляет собой поток для записи на входной поток дочернего процесса.
Свойство `StandardOutput` и `StandartError` представлеяют собой потоки для чтения данных поступающих на стандартный
поток, дочернего процесса, выввода или ошибок.

Пример:

```csharp
using System;
using System.Diagnostics;

public class SecondlyProgram
{
    public static void Main(string[] args)
    {
        try
        {
            string input;
            while ((input = Console.ReadLine()) != "")
            {
                Console.WriteLine(input);
            }
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```

```csharp
using System;
using System.Diagnostics
using System.IO;

public class MainProgram
{
    public static void Main(string[] args)
    {
        try
        {
            Process process = new Process();
            process.StartInfo.FileName = "SecondaryProgram.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            
            process.StandardInput.WriteLine("Hello World!");
            process.StandardInput.Close();
            
            Console.WriteLine(process.StandardOutput.ReadLine());
            Console.WriteLine(process.StandardError.ReadLine());
            
            process.WaitForExit();
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```