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

        private void bttnCompile_Click(object sender, EventArgs e)
        {
            toolStripLoadCompile.Text = "Compiles: Yes";
        }

        private void bttnRunTestCase_Click(object sender, EventArgs e)
        {
            toolStripLoadRuns.Text = "Runs: Yes";
        }

        private void bttnCommentScanner_Click(object sender, EventArgs e)
        {
            toolStripLoadCommentScore.Text = "Comment Score: 60 %";
        }

        private void bttnLoadTest_Click(object sender, EventArgs e)
        {
            toolStripLoad.Text = "Loads: Yes";
        }

        private void bttnGenReport_Click(object sender, EventArgs e)
        {
            GenerateReportForms grf = new GenerateReportForms();
            grf.Show();
        }
    }
}
