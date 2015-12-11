/*
File name: MainPage.xaml.cs
Project: Windows 10 universal Application
By: William Pring and Naween Mehanmal
Date: 
Description: The starting view
*/



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


using Windows.UI.Xaml.Shapes;
using Windows.UI; 

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyGameApp
{
    /// <summary>
    /// The main page the game when starting
    /// </summary>
    ///     
    public sealed partial class MainPage : Page
    {
        /*
         Name: MainPage 
         Purpose: Constructor
         Data Members : Void
         Return: void
      */
        public MainPage()
        {
            this.InitializeComponent();        
        }
        /*
            Name: MainPage 
            Purpose: Starts the game
            Data member: void
            Return: void
        */
        private void Go_To_Game(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePlay));
        }
    }
}

