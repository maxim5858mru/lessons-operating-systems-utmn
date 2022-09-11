using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace LW3_Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            GetChart(-1.2, 1.2, 0.025);
        }

        private void GetChart(double from, double to, double step)
        {
            var dataSin = GetData(from, to, step, 1);
            var dataCos = GetData(from, to, step, 2);

            foreach (var key in dataSin.Keys)
            {
                chart.Series[0].Points.AddXY(key, dataSin[key] / dataCos[key]);
            }
        }

        private Dictionary<double, double> GetData(double from, double to, double step, int function)
        {
            var process = new Process();
            Dictionary<double, double> result = new Dictionary<double, double>();

            // Запуск дочернего процесса
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            process.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
            process.StartInfo.FileName =
                "C:\\Users\\maxim\\Documents\\Проекты\\UTMN\\Lessons Operating Systems UTMN\\Laboratory work №2 Console\\bin\\Debug\\net7.0\\LW2_Console.exe";

            // Передача параметров
            process.StartInfo.Environment.Add("LW_OS_FROM", from.ToString());
            process.StartInfo.Environment.Add("LW_OS_TO", to.ToString());
            process.StartInfo.Environment.Add("LW_OS_STEP", step.ToString());
            process.StartInfo.Environment.Add("LW_OS_FUNC", function.ToString());
            process.Start();
            process.WaitForExit();

            // Очистка буфера от лишних строк
            for (int i = 0; i < 2; i++)
            {
                process.StandardOutput.ReadLine();
            }

            if (process.StandardOutput.ReadLine() != new string('-', 34))
                throw new Exception(); // Проверка правильности введённых данных

            // Получение данных
            for (double i = from; i <= to; i += step)
            {
                string[] line = process.StandardOutput.ReadLine().Split('|');
                result.Add(Convert.ToDouble(line[1]), Convert.ToDouble(line[2]));
            }

            var temp = process.StandardOutput.ReadToEnd();
            return result;
        }

        private void buttonChartBilld_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            GetChart(double.Parse(textBoxMin.Text), double.Parse(textBoxMax.Text), double.Parse(textBoxStep.Text));
        }
    }
}