# Лабораторная работа №4 Создание потока

**Цель работы**: Научиться создавать потоки различными средствами.

## Задание

Требуется создать три потока. Первый поток должен быть создан с использованием статического метода класса, второй - с
использованием метода экземпляра класса, а третий - с использованием анонимного делегата.

**Вариант №6**: Напишите программу, которая создаёт два потока, а затем дожидается их завершения. Первый поток
запрашивает необходимое количество значений *n1* и строит таблицу значений функции *sin(x)*, начиная с точки *x1* с
шагом *s1* с задержкой каждого шага *d1*. Второй поток проверяет каждые 0,5 секунды количество строк в файле, созданном
первым потоком, и в случае его изменения выводит его на экран. Таблица выводится в файл `поток1.dat`. Вывод
осуществляется по формату `dd.mm.yyyy hh:nn:ss.zzz «значение x:8.4» «значение y:8.4»`. Значения *x1*, *s1* и *d1*
запрашиваются до создания потоков и передаются им при создании.

## Теоретическая часть

Для работы с потоками используется класс `Thread` из пространства имён `System.Threading`. Класс `Thread` имеет четыре
конструктора.

Пример создания потока с помощью непараметризированного делегата:

```charp
using System;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        var thread = new Thread(new ThreadStart(Work.Do));
        thread.Start();
        
        do
        {
            Console.WriteLine("Relaxing...");
            Thread.Sleep(2000);
        } while (true);
    }
}

public class Work
{
    Work() { }

    public static void Do()
    {
        do
        {
            Console.WriteLine("Working...");
            Thread.Sleep(5000);
        } while (true);
    }
}
```

Пример создания потока с помощью параметризированного делегата:

```charp
using System;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        var thread = new Thread(new ParameterizedThreadStart(Work.Do));
        thread.Start("Hello World!");
        
        do
        {
            Console.WriteLine("Relaxing...");
            Thread.Sleep(2000);
        } while (true);
    }
}

public class Work
{
    Work() { }

    public static void Do(object data)
    {
        do
        {
            Console.WriteLine(data.ToString());
            Thread.Sleep(5000);
        } while (true);
    }
}
```

Аргументом конструктора может быть как статический метод, так и метод экземпляра класса. Также можно использовать
анонимные делегаты. Например:

```charp
using System;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        var thread = new Thread(delegate() 
        {
            do
            {
                Console.WriteLine("Working...");
                Thread.Sleep(5000);
            } while (true);
        });
        thread.Start();
        
        do
        {
            Console.WriteLine("Relaxing...");
            Thread.Sleep(2000);
        } while (true);
    }
}
```

Вызывающий поток может использовать метод `Join`, чтобы дождаться завершения рабочего потока. При ожидании вызывающий
поток блокируется. Формы метода `Join`:

- `Join()` - блокирует выполнение вызывающего потока до окончания рабочего;
- `Join(int milliseconds)` - блокирует вызывающий поток на указанное время, если рабочий поток завершится, то вызывающий
  поток будет разблокирован досрочно, а метод вернёт `true`;
- `Join(TimeSpan timeout)` - аналогичен второй форме.

Пример №1, создание потока с использованием статического метода класса:

```charp
using System;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        var thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        thread.Join();
    }
    
    public static void ThreadMethod()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Процедура потока: {0}", i);
            Thread.Sleep(0);        // Отдаём системе остаток выделенного времени
        }
    }
}
```

Пример №2, создания потока с использованием метода экземпляра класса:

```charp
using System;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        var program = new Program();
        program.RunThread();
    }
    
    public void ThreadMethod()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Процедура экземпляра потока: {0}", i);
            Thread.Sleep(0);        // Отдаём системе остаток выделенного времени
        }
    }
    
    public void RunThread()
    {
        var thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        thread.Join();
    }
}
```

Пример №3, создание потока с использованием анонимного делегата:

```charp
using System;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        var program = new Program();
        program.RunThread();
    }
    
    public void ThreadMethod()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Процедура экземпляра потока: {0}", i);
            Thread.Sleep(0);        // Отдаём системе остаток выделенного времени
        }
    }
    
    public void RunThread()
    {
        var thread = new Thread(delegate()
        {
            ThreadMethod();
        });
        thread.Start();
        thread.Join();
    }
}
```