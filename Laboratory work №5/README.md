# Лабораторная работа №5 Приостановка и возобновление потока

**Цель работы**: Научиться приостанавливать и возобновлять работу потока.

## Задание

**Вариант №6**: Напишите программу, которая создаёт два потока, а затем дожидается их завершения и печатает на экран
значение глобальной переменной `y`. Первый делает 100 вычислений *y = x + 2sin(x)*, а второй *x = cos(y)*. Каждый из
потоков перед вычислением ждёт 40 миллисекунд, а после вычисления возобновляет работу другого потока и 
приостанавливает свою. Первым вычисление производит второй поток. Приостановка и возобновление работы производится 
средствами класса `System.Threading.Thread`. Потоки знают друг о друге и значениях `x` и `y` через поля класса.

## Теоретическая часть

Потоки могут приостанавливать или продолжать выполнение других потоков. Приостановленному потоку не выделяется 
процессорное время. Для приостановки работы потока используется метод `Suspend()`, а для возобновления работы 
`Resume()`.

Приостановка и возобновление поток является устаревшим механизмом управления потоками. Вместо них рекомендуется 
использовать другие инструменты управления потоками, такие как *Monitor*, *Mutex*, *Event* или *Semaphor*. После 
приостановки потока невозможно узнать какой код он выполнял в момент приостановки.

```csharp
using System;
using System.Threading;

class Program
{
    public static bool firstReady = false;
    public static bool secondReady = false;
    public static Thread firstThread, secondThread;
    
    public static void FirstThreadMethod()
    {
        Console.WriteLine("Остановка первого потока.");
        firstReady = true;
        Thread.CurrentThread.Suspend();
        
        Console.WriteLine("Возобновление первого потока.");
        secondThread.Resume();
    }
    
    public static void SecondThreadMethod()
    {
        Console.WriteLine("Остановка второго потока.");
        secondReady = true;
        Thread.CurrentThread.Suspend();
        
        Console.WriteLine("Возобновление второго потока.");
        firstThread.Resume();
    }
    
    public static void Main()
    {
        firstThread = new Thread(FirstThreadMethod);
        secondThread = new Thread(SecondThreadMethod);
        
        firstThread.Start();
        secondThread.Start();
        
        while (!firstReady || !secondReady) {}
        
        firstThread.Resume();
        Console.ReadKey();
    }
}
```