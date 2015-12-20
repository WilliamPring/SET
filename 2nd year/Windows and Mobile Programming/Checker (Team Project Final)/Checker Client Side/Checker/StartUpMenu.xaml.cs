using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using CheckersClient; 


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Checker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartUpMenu : Page
    {
        TCPIPconnectorClient myConnector; 

        public StartUpMenu()
        {
            this.InitializeComponent();
            myConnector = new TCPIPconnectorClient(); 

        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            string retMsg = ""; 

            if (enteredIPAdress.Text != "")
            {
                errorMsg.Text = "";

                retMsg = myConnector.Connect(enteredIPAdress.Text, 9000);

                if(retMsg != "Success")
                {
                    errorMsg.Text = "*Invalid IP Address!";
                }
                else
                {
                    //errorMsg.Text = "Connected";

                    this.Frame.Navigate(typeof(MainPage), myConnector); //Go to the new page
                }
            }
            else
            {
                errorMsg.Text = "*Please do not leave textbox blank!";
            }
        }
    }
}
