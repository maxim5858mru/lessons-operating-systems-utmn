# Лабораторная работа №1 Создание процесса

**Цель работы**: Научиться создавать процессы средствами .NET.

## Задание

**Вариант №6**: Используя класс `Process` (`System.Diagnostics`) и его свойство `StartInfo`, выполните указанный первым
параметром `.vbs`-файл, передав ему все последующие параметры, после чего выведите сообщение об окончании исполнения
задания.

## Теоретическая часть

Для создания процесса используется класс `Process` из пространства имён `System.Diagnostics`. Процесс создаётся при
вызове метода `Start`, который имеет 5 перегруженных статических вариантов и 1 вариант метода экземпляра.

Свойства `StartInfo`:

- `UseShellExecute` - позволяет «запускать» не бинарные файлы, такие как `doc`, `xls`, `js`, `vbs`, `cmd` и другие;
- `FileName` - определяет имя бинарного файла для запуска при `UseShellExecute = false` либо имя открываемого файла при
  `UseShellExecute = true`;
- `Arguments` - задаёт дополнительные параметры запускаемому процессу;
- `UserName` и `Password` - позволяют выполнить запуск от имени другого пользователя;
- `WorkingDirectory` - указывает рабочую папку. Если указанны `UserName` и `Password`, то рекомендуется задать этому
  свойству значение, иначе будет указано значение по умолчанию - `%SYSTEMROOT%\system32`.

Если процесс успешно создан, то метод экземпляра возвращает значение `true` и заполняет необходимые поля экземпляра.
Статические методы в случае успеха, возвращают новый экземпляр класса `Process` с заполненными полями. В противном
случае они возвращают значение `null`.

Пример:

```csharp
using System;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        var process = new Process();
        try
        {
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = "test.txt";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            
            process.WaitForExit();
            Console.WriteLine("Процесс успешно был запущен и завершился через " +
                $"{process.ExitTime - process.StartTime}, c кодом возврата {process.ExitCode}.");
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }
}
```