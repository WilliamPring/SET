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

using Windows.UI;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyGameApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        private List<Player> leaderTable;

        public BlankPage1()
        {
            this.InitializeComponent();
            leaderTable = new List<Player>();

            this.NavigationCacheMode = NavigationCacheMode.Enabled; 
        }    

        private void Back_To_Game(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.GoBack();
        }      

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           try
            {
                leaderTable.Clear();
                leaderTable = e.Parameter as List<Player>;

                foreach (var leader in leaderTable)
                {
                    myLeaderBoard.Items.Add(leader.GetUserName + ": " + leader.GetTime);
                }
            }
            catch(Exception)
            {
                myLeaderBoard.Items.Add("Empty");
            }
        }

    }
}
