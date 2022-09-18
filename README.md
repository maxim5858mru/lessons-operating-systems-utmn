# Лабораторные работы по предмету «Операционные системы»

## Лабораторная работа №1 Создание процесса

**Вариант №6** Используя класс `Process` (`System.Diagnostics`) и его свойство `StartInfo`, выполните указанный первым
параметром `.vbs`-файл, передав ему все последующие параметры, после чего выведите сообщение об окончании исполнения
задания.

## Лабораторная работа №2 Создание процесса с перенаправленным вводом и выводом

**Общее задание** ННапишите программу запрашивающую выбор функции по номеру, расчётный диапазон (*Xмин*, *Xмакс*,
*шаг*), и выводящую на консоль значение выбранной функции в этом интервале с шагом.

Номера функций:

1. *sin(x)*;
2. *cos(x)*;
3. *x^2*;
4. *e^x*;
5. *ln(x)*;
6. *Выход*.

**Вариант №6**: Напишите программу, которая строит график функции *tan(x)* в заданном интервале с заданным шагом
используя общую программу.

## Лабораторная работа №3 Изменение переменных окружения

**Общее задание** Напишите программу получающую из переменных окружения номер требуемой функции, расчётный диапазон (*
Xмин*, *Xмакс*, *шаг*), и выводящую на консоль значение выбранной функции в этом интервале с указанным шагом.

Номера функций:

1. *sin(x)*;
2. *cos(x)*;
3. *x^2*;
4. *e^x*;
5. *ln(x)*;
6. *Выход*.

**Вариант №6** Напишите программу, которая строит график функции *tan(x)* в заданном интервале с заданным шагом
используя общую программу.

## Лабораторная работа №4 Создание потока

Требуется создать три потока. Первый поток должен быть создан с использованием статического метода класса, второй - с
использованием метода экземпляра класса, а третий - с использованием анонимного делегата.

**Вариант №6**: Напишите программу, которая создаёт два потока, а затем дожидается их завершения. Первый поток
запрашивает необходимое количество значений *n1* и строит таблицу значений функции *sin(x)*, начиная с точки *x1* с
шагом *s1* с задержкой каждого шага *d1*. Второй поток проверяет каждые 0,5 секунды количество строк в файле, созданном
первым потоком, и в случае его изменения выводит его на экран. Таблица выводится в файл `поток1.dat`. Вывод
осуществляется по формату `dd.mm.yyyy hh:nn:ss.zzz «значение x:8.4» «значение y:8.4»`. Значения *x1*, *s1* и *d1*
запрашиваются до создания потоков и передаются им при создании.

## Лабораторная работа №5 Приостановка и возобновление потока

**Вариант №6**: Напишите программу, которая создаёт два потока, а затем дожидается их завершения и печатает на экран
значение глобальной переменной `y`. Первый делает 100 вычислений *y = x + 2sin(x)*, а второй *x = cos(y)*. Каждый из
потоков, перед вычислением ждёт 40 миллисекунд, а после вычисления возобновляет работу другого потока и 
приостанавливает свою. Первым вычисление производит второй поток. Приостановка и возобновление работы производится 
средствами класса `System.Threading.Thread`. Потоки знают друг о друге и значениях `x` и `y` через поля класса.

## Лабораторная работа №6 Синхронизация потоков с помощью событий

Данная работа предполагает решение одной из классических задач синхронизации в операционной среде:

1. Реализация задачи «поставщик - потребитель»;
2. Реализация задачи «читатели - писатели».

Требуется реализовать приложение - поставщик и приложение - потребитель или многопоточное приложение с потоком
«поставщик» и потоком «потребитель». В некоторых случаях потребуется реализовать несколько потоков «поставщиков» или
«потребителей». Для синхронизации потоков использовать объект ожидания – событие.

**Вариант №6**: Напишите программу, в которой первый поток в бесконечном цикле генерирует числа в диапазоне от 0 до 100
и выводит их на экран. Второй поток делит каждое число на 2 и дописывает полученные значения в текстовый файл. Для
передачи данных от первого ко второму потоку использовать буфер в виде массива, размером 10 элементов.

## Лабораторная работа №7 Синхронизация потоков с помощью мьютексов

Данная работа предполагает решение задачи организации взаимоисключающего доступа к разделяемому ресурсу с помощью 
мьютексов.

**Вариант №6**: Напишите программу, в которой два потока работая параллельно, генерируют в цикле (100 итераций) 
некоторую последовательность целых чисел. Полученные данные накапливаются в общем массиве размером 10 элементов. 
Причём, в один и тот же момент времени в массиве могут находиться данные, принадлежащие одному потоку. Всякий раз, 
после заполнения массива, поток, чьи данные в нем находятся, должен выгрузить содержимое массива в общий текстовый 
файл и очистить массив.