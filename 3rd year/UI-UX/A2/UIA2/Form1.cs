using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIA2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView.ExpandAll();

        }

        private void bttnClear_Click(object sender, EventArgs e)
        {
            textBox.Clear();
        }

  

 

        private void bttnGenReport_Click(object sender, EventArgs e)
        {
            GenerateReportForms grf = new GenerateReportForms();
            grf.Show();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
