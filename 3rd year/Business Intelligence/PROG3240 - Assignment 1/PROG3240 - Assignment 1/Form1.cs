/*
 * File: Form1.cs
 * Project: Assignment 1 PROG3240
 * Programmer: William Pring
 * Date: 9/8/2017
 * Description: Practice creating charts on Windows Forms using Chart Control. 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG3240___Assignment_1
{
    /// <summary>
    /// The form class
    /// </summary>
    public partial class Form1 : Form
    {   
        enum ChartStyle { Pie = 0, Line, Control, Pareto }

        public Form1()
        {
   
            InitializeComponent();
            //hide the group box
            gbLB.Visible = false;

            DataTable dTable = new DataTable();
            //getting the colums names 
            dTable.Columns.Add(dataGridView.Columns[0].DataPropertyName);
            dTable.Columns.Add(dataGridView.Columns[1].DataPropertyName);
            //setting the intal values
            dTable.Rows.Add("Denys", 30);
            dTable.Rows.Add("Matt", 20);
            dTable.Rows.Add("Steven", 11);
            dTable.Rows.Add("Naween", 41);

            dataGridView.DataSource = dTable;
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            //setting values
            pieChart.Series["Series1"].Points.DataBindXY(dataView, dTable.Columns[0].ColumnName, dataView, dTable.Columns[1].ColumnName);
            
            dataGridView.Refresh();

        }
        /// <summary>
        /// Refresh the charts or changes charts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //refresh the chart and control the view for the combo box
            if (tabControl.SelectedIndex == 0)
            {
                gbLB.Visible = false;
                LoadPieChart();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                gbLB.Visible = false;
                LoadLineChart();
            }
            else if (tabControl.SelectedIndex == 2)
            {
                gbLB.Visible = true;
                LoadControlChart();
            }
            else
            {
                gbLB.Visible = false;
                LoadParetoDiagram();
            }

        }
        /// <summary>
        /// Loading Chart for the pie chart
        /// </summary>
        private void LoadPieChart()
        {
            ClearPieChart();
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            pieChart.Series["Series1"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);
        }
 
        /// <summary>
        /// Load Line Chart
        /// </summary>
        private void LoadLineChart()
        {
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            lineChart.Series["Amount"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);

            lineChart.ChartAreas["lineChartArea"].AxisX.Title = "People";
            lineChart.ChartAreas["lineChartArea"].AxisY.Title = "Cookies Amount";
        }
        /// <summary>
        /// Load Control Chart
        /// </summary>
        private void LoadControlChart()
        {
            //clear the values
            ClearControlChart();
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            controlChart.Series["Amount"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);
            double totalCookies = 0; 
            for(int i =0; i < controlChart.Series["Amount"].Points.Count; i++)
            {
               totalCookies += controlChart.Series["Amount"].Points[i].YValues[0];
            }
            //getting total 
            totalCookies = totalCookies/ controlChart.Series["Amount"].Points.Count;
            double ucl;
            double lcl;
            double uwl;
            double lwl;
            //getting the values and setting to 0 if there is an error
            ucl = isNumber(tbUCL.Text, "UCL");
            lcl = isNumber(tbLCL.Text, "LCL");
            uwl = isNumber(tbUWL.Text, "UWL");
            lwl = isNumber(tbLWL.Text, "LWL");

            foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in controlChart.Series["Amount"].Points)
            {
                controlChart.Series["Average"].Points.Add(totalCookies);
                controlChart.Series["UCL"].Points.Add(ucl);
                controlChart.Series["LCL"].Points.Add(lcl);
                controlChart.Series["UWL"].Points.Add(uwl);
                controlChart.Series["LWL"].Points.Add(lwl);
            }

        }
        /// <summary>
        /// Paring numbers 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="textbox"></param>
        /// <returns></returns>
        private double isNumber(string input, string textbox)
        {
            double temp = 0;
            bool isDouble = true;
            isDouble = double.TryParse(input, out temp);
            if ((isDouble==false) || (temp < 0))
            {
                temp = 0;
                if(textbox== "UCL")
                {
                    tbUCL.Text = "0";
                }
                else if (textbox == "LCL")
                {
                    tbLCL.Text = "0";
                }
                else if (textbox == "UWL")
                {
                    tbUWL.Text = "0";
                }
                else if(textbox == "LWL")
                {
                    tbLWL.Text = "0";
                }
            }


            return temp;
        }
        /// <summary>
        /// Clear line chart 
        /// </summary>
        private void ClearLineChart()
        {

            lineChart.Series["Amount"].Points.Clear();
        }

        /// <summary>
        /// Clearing the pie chart
        /// </summary>
        private void ClearPieChart()
        {
            pieChart.Series["Series1"].Points.Clear();
        }
        /// <summary>
        /// Clearning Control Chart
        /// </summary>
        private void ClearControlChart()
        {
            controlChart.Series["Average"].Points.Clear();
            controlChart.Series["UCL"].Points.Clear();
            controlChart.Series["LCL"].Points.Clear();
            controlChart.Series["UWL"].Points.Clear();
            controlChart.Series["LWL"].Points.Clear();
            controlChart.Series["Amount"].Points.Clear();
        }

        /// <summary>
        /// Clear Pareto Chart
        /// </summary>
        private void ClearPareto()
        {
            paretoChart.Series["Amount"].Points.Clear();
            paretoChart.Series["Percentage"].Points.Clear();
        }

        private void LoadParetoDiagram()
        {
            ClearPareto();
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            dataView.Sort = dataGridView.Columns[1].DataPropertyName + " DESC";
            paretoChart.Series["Amount"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);
            paretoChart.Series["Percentage"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);
        }

        /// <summary>
        /// Tab Control update change the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getting the tab and updating the chart
            if(tabControl.SelectedIndex == (int)ChartStyle.Pie)
            {
                gbLB.Visible = false;
                LoadPieChart();
            }
            else if (tabControl.SelectedIndex == (int)ChartStyle.Line)
            {
                gbLB.Visible = false;
                LoadLineChart();
            }
            else if (tabControl.SelectedIndex == (int)ChartStyle.Control)
            {
                gbLB.Visible = true;
                LoadControlChart();
            }
            else
            {
                gbLB.Visible = false;
                LoadParetoDiagram();
            }
        }

       
    }
}
