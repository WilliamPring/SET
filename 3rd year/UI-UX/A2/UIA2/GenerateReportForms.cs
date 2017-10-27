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
    public partial class GenerateReportForms : Form
    {
        public GenerateReportForms()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bttnSerach_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbDirectory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void AutoFill_Click(object sender, EventArgs e)
        {
            tbFirstName.Text = "William";
            tbLastName.Text = "Pring";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
