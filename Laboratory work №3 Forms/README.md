# Лабораторная работа №3 Изменение переменных окружения

**Цель работы**: Освоить способы изменения блока переменных окружения дочернего процесса.

## Задание

### Основная программа

Напишите программу получающую из переменных окружения номер требуемой функции, расчётный диапазон (*Xмин*, *Xмакс*, 
*шаг*), и выводящую на консоль значение выбранной функции в этом интервале с указанным шагом.

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

Каждый процесс имеет свой блок окружения. В этом блоке хранятся переменные окружения, а также их значения. Работа с
блоком окружения осуществляется с использованием свойства `StartInfo.EnvironmentVariables`.

Установка переменной окружения при `UseShellExecute = true` при вызове метода `Start()` вызывает
исключение `InvalidOperationException`. В таком случае требуется менять переменные окружения для текущего процесса,
через `Environment.SetEnvironmentVariable(string variable, string value)`. Дочерний процесс наследует от родительского
процесса блок переменных окружения. Считывание переменных окружения происходит
через `Process.GetCurrentProcess().StartInfo.EnvironmentVariables[string variable]`
или `Environment.GetEnvironmentVariable(string variable)`.

Перегруженный метод `SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)` позволяет
указать область видимости переменной окружения.

- `EnvironmentVariableTarget.Process` - переменная окружения видна только в текущем процессе;
- `EnvironmentVariableTarget.User` - переменная окружения видна только в текущем пользователе;
- `EnvironmentVariableTarget.Machine` - переменная окружения видна всем пользователям на компьютере.

Пример:

```csharp
using System;
using System.Diagnostics;
using System.Collections.Specialized;

public class SecondlyProgram
{
    public static void Main(string[] args)
    {
        try
        {
            Process process = Process.GetCurrentProcess();
            StringDictionary envVars = process.StartInfo.EnvironmentVariables;

            // Вывод всех переменных окружения
            foreach (string envVarKey in envVars.Keys)
            {
                Console.WriteLine("{0} = {1}", envVarKey, envVars[envVarKey]);
            }
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.Message);
        }
    }
}
```

```csharp
using System;
using System.Diagnostics;
using System.IO;

public class MainProgram
{
    public static void Main(string[] args)
    {
        Process process = new Process();
        
        try
        {
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.EnvionmentVariables["VARIABLE"] = "value";
            process.StartInfo.FileName = "SecondProgram.exe";
            process.Start();

            process.WaitForExit();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.Message);
        }
    }
}
```