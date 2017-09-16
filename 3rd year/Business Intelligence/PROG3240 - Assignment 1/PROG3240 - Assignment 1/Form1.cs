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
    public partial class Form1 : Form
    {
        
        List<Person> peopleWithCoookies = new List<Person>();
        enum ChartStyle { Pie = 0, Line, Control, Pareto }

        public Form1()
        {
   
            InitializeComponent();
            gbLB.Visible = false;


            DataTable dTable = new DataTable();
            dTable.Columns.Add(dataGridView.Columns[0].DataPropertyName);
            dTable.Columns.Add(dataGridView.Columns[1].DataPropertyName);

            dTable.Rows.Add("Denys", 30);
            dTable.Rows.Add("Matt", 20);
            dTable.Rows.Add("Steven", 11);
            dTable.Rows.Add("Naween", 41);

            dataGridView.DataSource = dTable;
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
         
            pieChart.Series["Series1"].Points.DataBindXY(dataView, dTable.Columns[0].ColumnName, dataView, dTable.Columns[1].ColumnName);
            
            dataGridView.Refresh();
           


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
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
        private void LoadPieChart()
        {
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            pieChart.Series["Series1"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);
        }

        private void ClearPieChart()
        {
            pieChart.Series["Series1"].Points.Clear();
        }
        private void LoadLineChart()
        {
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            lineChart.Series["Amount"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);

            lineChart.ChartAreas["lineChartArea"].AxisX.Title = "People";
            lineChart.ChartAreas["lineChartArea"].AxisY.Title = "Cookies Amount";
        }
        private void LoadControlChart()
        {
            //clear the values
            clearControlChart();
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            DataView dataView = new DataView(dataTable);
            controlChart.Series["Amount"].Points.DataBindXY(dataView, dataTable.Columns[0].ColumnName, dataView, dataTable.Columns[1].ColumnName);
            double totalCookies = 0; 
            for(int i =0; i < controlChart.Series["Amount"].Points.Count; i++)
            {
               totalCookies += controlChart.Series["Amount"].Points[i].YValues[0];
            }
            totalCookies = totalCookies/ controlChart.Series["Amount"].Points.Count;
            foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in controlChart.Series["Amount"].Points)
            {
                controlChart.Series["Average"].Points.Add(totalCookies);
                controlChart.Series["UCL"].Points.Add(Convert.ToDouble(tbUCL.Text));
                controlChart.Series["LCL"].Points.Add(Convert.ToDouble(tbLCL.Text));
                controlChart.Series["UWL"].Points.Add(Convert.ToDouble(tbUWL.Text));
                controlChart.Series["LWL"].Points.Add(Convert.ToDouble(tbLWL.Text));
            }

        }
        private void clearControlChart()
        {
            controlChart.Series["Average"].Points.Clear();
            controlChart.Series["UCL"].Points.Clear();
            controlChart.Series["LCL"].Points.Clear();
            controlChart.Series["UWL"].Points.Clear();
            controlChart.Series["LWL"].Points.Clear();
            controlChart.Series["Amount"].Points.Clear();
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
        private void ClearPareto()
        {
            paretoChart.Series["Amount"].Points.Clear();
            paretoChart.Series["Percentage"].Points.Clear();
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
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
