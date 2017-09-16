namespace PROG3240___Assignment_1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPieChart = new System.Windows.Forms.TabPage();
            this.tabLineChart = new System.Windows.Forms.TabPage();
            this.tabControlChart = new System.Windows.Forms.TabPage();
            this.tabParetoDiagram = new System.Windows.Forms.TabPage();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tabPieChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPieChart);
            this.tabControl.Controls.Add(this.tabLineChart);
            this.tabControl.Controls.Add(this.tabControlChart);
            this.tabControl.Controls.Add(this.tabParetoDiagram);
            this.tabControl.Location = new System.Drawing.Point(13, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(395, 384);
            this.tabControl.TabIndex = 0;
            // 
            // tabPieChart
            // 
            this.tabPieChart.Controls.Add(this.pieChart);
            this.tabPieChart.Location = new System.Drawing.Point(4, 22);
            this.tabPieChart.Name = "tabPieChart";
            this.tabPieChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPieChart.Size = new System.Drawing.Size(387, 358);
            this.tabPieChart.TabIndex = 0;
            this.tabPieChart.Text = "Pie Chart";
            this.tabPieChart.UseVisualStyleBackColor = true;
            // 
            // tabLineChart
            // 
            this.tabLineChart.Location = new System.Drawing.Point(4, 22);
            this.tabLineChart.Name = "tabLineChart";
            this.tabLineChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabLineChart.Size = new System.Drawing.Size(387, 358);
            this.tabLineChart.TabIndex = 1;
            this.tabLineChart.Text = "Line Chart";
            this.tabLineChart.UseVisualStyleBackColor = true;
            // 
            // tabControlChart
            // 
            this.tabControlChart.Location = new System.Drawing.Point(4, 22);
            this.tabControlChart.Name = "tabControlChart";
            this.tabControlChart.Size = new System.Drawing.Size(387, 358);
            this.tabControlChart.TabIndex = 2;
            this.tabControlChart.Text = "Control Chart";
            this.tabControlChart.UseVisualStyleBackColor = true;
            // 
            // tabParetoDiagram
            // 
            this.tabParetoDiagram.Location = new System.Drawing.Point(4, 22);
            this.tabParetoDiagram.Name = "tabParetoDiagram";
            this.tabParetoDiagram.Size = new System.Drawing.Size(387, 358);
            this.tabParetoDiagram.TabIndex = 3;
            this.tabParetoDiagram.Text = "Pareto Diagram";
            this.tabParetoDiagram.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(543, 389);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pieChart
            // 
            chartArea1.Name = "ChartArea1";
            this.pieChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.pieChart.Legends.Add(legend1);
            this.pieChart.Location = new System.Drawing.Point(0, 1);
            this.pieChart.Name = "pieChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.pieChart.Series.Add(series1);
            this.pieChart.Size = new System.Drawing.Size(383, 354);
            this.pieChart.TabIndex = 0;
            title1.Name = "pieTitle";
            title1.Text = "People vs Cookies";
            title1.Visible = false;
            this.pieChart.Titles.Add(title1);
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(PROG3240___Assignment_1.Program);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(PROG3240___Assignment_1.Form1);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(414, 35);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(286, 338);
            this.dataGridView.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 414);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.tabPieChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pieChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPieChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart;
        private System.Windows.Forms.TabPage tabLineChart;
        private System.Windows.Forms.TabPage tabControlChart;
        private System.Windows.Forms.TabPage tabParetoDiagram;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.BindingSource programBindingSource;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}

