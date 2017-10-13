using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using System.Windows.Forms;



namespace ClinetConsumingWebservices
{
    public partial class fmWebServiceConsume : Form
    {


  

        VinniesLoanService vls = new VinniesLoanService();
        TickerTape tt = new TickerTape();
        TextService ts = new TextService();
        List<string> tbValues = new List<string>();

        List<string> typesForTextbox = new List<string>();
        public fmWebServiceConsume()
        {

            //string temp = conversionCase.CaseConvert("123asdf", );
            InitializeComponent();
        }

        private void cbWebServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear the function name (just reset it)
            lbFunctionName.Text = "";
            typesForTextbox.Clear();
            if (cbWebServices.Items[cbWebServices.SelectedIndex].ToString() == "TextService")
            {
                lbFunctionName.Text = "CaseConvert";
                typesForTextbox.Add("string");
                typesForTextbox.Add("uint");
                CreateTextBoxForInput(2, typesForTextbox);

            }
            else if (cbWebServices.Items[cbWebServices.SelectedIndex].ToString() == "VinniesLoanService")
            {
                lbFunctionName.Text = "LoanPayment";
                typesForTextbox.Add("float");
                typesForTextbox.Add("float");
                typesForTextbox.Add("int");
                CreateTextBoxForInput(3, typesForTextbox);

            }
            else if (cbWebServices.Items[cbWebServices.SelectedIndex].ToString() == "TickerTape")
            {
                lbFunctionName.Text = "GetQuote";
                typesForTextbox.Add("string");
                CreateTextBoxForInput(1, typesForTextbox);

            }

        }


        private void CreateTextBoxForInput(int paramAmt, List<string> tempTypesForTextbox)
        {
            this.flowLayoutPanel.Controls.Clear();
            for (int textBoxID = 0; textBoxID < paramAmt; textBoxID++)
            {

                Label lb = new Label();
                lb.Text = "Parmemeter " + (textBoxID + 1) + ": ";
                flowLayoutPanel.Controls.Add(lb);
                TextBox tb = new TextBox();
                tb.Name = textBoxID.ToString() + tempTypesForTextbox[textBoxID];
                flowLayoutPanel.Controls.Add(tb);

            }
        }

        private void bttnSubmit_Click(object sender, EventArgs e)
        {
            bool isValidateTextBoxValid = true;
            isValidateTextBoxValid = ValidateTextBox();

            if ((isValidateTextBoxValid == true) && (cbWebServices.SelectedIndex != -1))
            {
                SendToWebservices();
            }
        }

        private bool SendToWebservices()
        {
            string display = "";

            try
            {
                if (cbWebServices.Items[cbWebServices.SelectedIndex].ToString() == "TextService")
                {

                    display = ts.CaseConvert(tbValues[0], Convert.ToUInt32(tbValues[1]));


                }
                else if (cbWebServices.Items[cbWebServices.SelectedIndex].ToString() == "VinniesLoanService")
                {

                    vls.LoanPayment(Convert.ToSingle(tbValues[0]), Convert.ToSingle(tbValues[1]), Convert.ToInt32(tbValues[2]));



                }
                else if (cbWebServices.Items[cbWebServices.SelectedIndex].ToString() == "TickerTape")
                {
                    QuoteInfo qi = new QuoteInfo();
                    qi = tt.GetQuote(tbValues[0]);



                }
            }
            catch(SoapException se)
            {
                GenerateMessageBox(se.Message, se.Detail.InnerText);
            }
            catch (WebException we)
            {
                GenerateMessageBox(we.Message, we.InnerException.Message);

              //  GenerateMessageBox(((HttpWebResponse)wexp.Response).StatusDescription, );


            }

            return true;
        }
        private void GenerateMessageBox(string captions, string message)
        {
            DialogResult result;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            result = MessageBox.Show(this, message, captions, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
        private bool ValidateTextBox()
        {
            bool status = true;
            int textBoxID = 1;
            tbValues.Clear();
            foreach (TextBox tb in this.flowLayoutPanel.Controls.OfType<TextBox>())
            {
                tbValues.Add(tb.Text);
                //take a look at textbox name because it has the datatype
                if (tb.Name.Contains("string"))
                {
                    if ((tb.Text.Contains("true")) || (tb.Text.Contains("false")))
                    {
                        status = false;
                        GenerateMessageBox("Types Provided is incorrect in the Parameters", "Parmemeter" + textBoxID + " Type is inccorect. Was expecting a String.");
                        break;
                    }
                }
                else if (tb.Name.Contains("float"))
                {
                    float value;
                    if (!float.TryParse(tb.Text, out value))
                    {
                        status = false;
                        GenerateMessageBox("Types Provided is incorrect in the Parameters", "Parmemeter" + textBoxID + " Type is inccorect. Was expecting a float.");
                        break;
                    }
                }
                else if (tb.Name.Contains("uint"))
                {
                    uint value;
                    if (!uint.TryParse(tb.Text, out value))
                    {
                        status = false;
                        GenerateMessageBox("Types Provided is incorrect in the Parameters", "Parmemeter" + textBoxID + " Type is inccorect. Was expecting a uint.");
                        break;
                    }

                }
                else if (tb.Name.Contains("int"))
                {
                    int value;
                    if (!int.TryParse(tb.Text, out value))
                    {
                        status = false;
                        GenerateMessageBox("Types Provided is incorrect in the Parameters", "Parmemeter" + textBoxID + " Type is inccorect. Was expecting a int.");
                        break;
                    }
                }

                textBoxID++;
            }
            return status;
        }
    }
}
