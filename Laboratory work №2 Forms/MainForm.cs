using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace LW2_Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {            
            InitializeComponent();
            GetChart(-1.2, 1.2, 0.025);
        }

        /// <summary>
        /// Построение графика
        /// </summary>
        /// <param name="from">От</param>
        /// <param name="to">До</param>
        /// <param name="step">Шаг</param>
        private void GetChart(double from, double to, double step)
        {
            var dataSin = GetData(from, to, step, 1);
            var dataCos = GetData(from, to, step, 2);

            foreach (var key in dataSin.Keys)
            {
                chart.Series[0].Points.AddXY(key, dataSin[key] / dataCos[key]);
            }
        }

        /// <summary>
        /// Получение данных для графика от дочернего процесса
        /// </summary>
        /// <param name="from">От</param>
        /// <param name="to">До</param>
        /// <param name="step">Шаг</param>
        /// <param name="function">Требуемая функция</param>
        /// <returns>Словарь с полученными значениями функции</returns>
        /// <exception cref="Exception">В случае если дочерний процесс неудачно завершил работу, будет вызвано исключение</exception>
        private Dictionary<double, double> GetData(double from, double to, double step, int function)
        {
            var process = new Process();
            Dictionary<double, double> result = new Dictionary<double, double>();

            // Запуск дочернего процесса
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            process.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
            process.StartInfo.FileName = "C:\\Users\\maxim\\Documents\\Проекты\\UTMN\\Lessons Operating Systems UTMN\\Laboratory work №2 Console\\bin\\Debug\\net7.0\\LW2_Console.exe";
            process.Start();

            // Передача параметров
            process.StandardInput.WriteLine(function);
            process.StandardInput.WriteLine(from);
            process.StandardInput.WriteLine(to);
            process.StandardInput.WriteLine(step);
            process.WaitForExit();

            // Очистка буфера от лишних строк
            for (int i = 0; i < 12; i++)
            {
                process.StandardOutput.ReadLine();
            }
            if (process.StandardOutput.ReadLine() != new string('-', 34)) throw new Exception();    // Проверка правильности введённых данных

            // Получение данных
            for (double i = from; i <= to; i += step)
            {
                string[] line = process.StandardOutput.ReadLine().Split('|');
                result.Add(Convert.ToDouble(line[1]), Convert.ToDouble(line[2]));
            }

            return result;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Построить"
        /// </summary>
        private void ButtonChartBuild_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            GetChart(double.Parse(textBoxMin.Text), double.Parse(textBoxMax.Text), double.Parse(textBoxStep.Text));
        }
    }
}
