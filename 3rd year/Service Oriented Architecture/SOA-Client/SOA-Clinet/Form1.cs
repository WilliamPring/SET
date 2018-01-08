using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOA_Clinet
{
    public partial class Form1 : Form
    {
        int pointX = 10;
        int pointY = 10;
        string serviceTagName;
        SocketConnector sc = new SocketConnector();

        public Form1()
        {
            InitializeComponent();
            serviceTagName = "";
            sc.StartService();
        }

        private void bttnStart_Click(object sender, EventArgs e)
        {
            bool status = sc.QuerryService("POSTAL");
            if(status)
            {
                GenerateUI();
            }
        }

        private void GenerateUI()
        {
            foreach(QueryServiceResponse response in sc.qsr)
            {
                if(response.type =="ARG")
                {
                    Label a = new Label();
                    TextBox tb = new TextBox();
                    a.Text = response.name + ":";
                    a.Location = new Point(pointX, pointY);
                    pointY += 23;
                    tb.Location = new Point(pointX, pointY);
                    tb.Name = response.name;
                    pointY += 20;
                    panControls.Controls.Add(a);
                    panControls.Controls.Add(tb);
                    panControls.Show();
    
                }
            }
        }
        private bool Validate()
        {
            int i = 0;
            bool status = false;
            foreach (Control control in panControls.Controls)
            {
                if (control is TextBox)
                {
                    foreach (QueryServiceResponse response in sc.qsr)
                    {
                        if((response.required == "optional") && (control.Text == ""))
                        {
                            continue;
                        }
                        if((control.Name == response.name))
                        {
                            bool temp = ValidateDataType(response.datatype, control.Text);
                            if (temp)
                            {
                                sc.qsr[i].value = control.Text;
                                status = true;
                            }
                            i++;
                        }
                    }
                }
            }

            return status;
        }

        private bool ValidateDataType(string type, string value)
        {
            type = type.ToLower();
            bool status = false;
            if(type == "char")
            {
            
            }
            else if(type =="short")
            {
                short number;
                status = Int16.TryParse(value, out number);
            }
            else if (type == "int")
            {
                int number;
                status = Int32.TryParse(value, out number);
            }
            else if (type == "long")
            {
                long number;
                status = long.TryParse(value, out number);
            }
            else if (type == "float")
            {
                float number;
                status = float.TryParse(value, out number);
            }
            else if (type == "double")
            {
                double number;
                status = double.TryParse(value, out number);
            }
            else if (type == "string")
            {
                status = true;
            }
      
            return status;
        }
        private void cbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceTagName = cbServices.SelectedItem.ToString();
        }

        private void btnSendArgs_Click(object sender, EventArgs e)
        {
            //validate shit
            bool status = Validate();
            if(status==true)
            {
                sc.ExecuteServiceMessage();
                pointY += 20;
                foreach (QueryServiceResponse response in sc.qsr)
                {
                    if (response.type == "RSP")
                    {
                        Label a = new Label();
                        a.Text = response.name + ":" + response.value;
                        a.Location = new Point(pointX, pointY);
                        pointY += 23;
                        panControls.Controls.Add(a);
                        panControls.Show();

                    }
                }
            }


        }

        private void bttnClearPanal_Click(object sender, EventArgs e)
        {
            pointX = 10;
            pointY = 10;
            panControls.Controls.Clear();
        }
    }
}
