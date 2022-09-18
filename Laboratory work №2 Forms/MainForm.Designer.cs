using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace LW2_Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.labelStep = new System.Windows.Forms.Label();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.buttonChartBuild = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea2.Name = "chartArea";
            this.chart.ChartAreas.Add(chartArea2);
            legend2.Name = "legend";
            this.chart.Legends.Add(legend2);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(0);
            this.chart.Name = "chart";
            series2.ChartArea = "chartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "legend";
            series2.Name = "tan(x)";
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(835, 460);
            this.chart.TabIndex = 0;
            this.chart.Text = "Chart";
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(733, 47);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(89, 20);
            this.textBoxMin.TabIndex = 1;
            this.textBoxMin.Text = "-1,2";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(700, 50);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(23, 13);
            this.labelMin.TabIndex = 0;
            this.labelMin.BackColor = Color.White;
            this.labelMin.Text = "От:";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(700, 76);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(25, 13);
            this.labelMax.TabIndex = 0;
            this.labelMax.BackColor = Color.White;
            this.labelMax.Text = "До:";
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(733, 73);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(89, 20);
            this.textBoxMax.TabIndex = 2;
            this.textBoxMax.Text = "1,2";
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Location = new System.Drawing.Point(700, 102);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(30, 13);
            this.labelStep.TabIndex = 0;
            this.labelStep.BackColor = Color.White;
            this.labelStep.Text = "Шаг:";
            // 
            // textBoxStep
            // 
            this.textBoxStep.Location = new System.Drawing.Point(733, 99);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(89, 20);
            this.textBoxStep.TabIndex = 3;
            this.textBoxStep.Text = "0,025";
            // 
            // buttonChartBuild
            // 
            this.buttonChartBuild.Location = new System.Drawing.Point(703, 125);
            this.buttonChartBuild.Name = "buttonChartBuild";
            this.buttonChartBuild.Size = new System.Drawing.Size(119, 23);
            this.buttonChartBuild.TabIndex = 4;
            this.buttonChartBuild.Text = "Построить";
            this.buttonChartBuild.UseVisualStyleBackColor = true;
            this.buttonChartBuild.Click += new System.EventHandler(this.ButtonChartBuild_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.buttonChartBuild);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.textBoxStep);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.chart);
            this.Name = "MainForm";
            this.Text = "LW2 Forms";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        public Chart chart;
        public System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
        public System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label labelStep;
        public System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.Button buttonChartBuild;
    }
}

