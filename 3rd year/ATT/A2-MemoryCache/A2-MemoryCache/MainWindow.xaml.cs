using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Caching;
using System.IO;

namespace A2_MemoryCache
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Cache global user data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnCacheAllGeneralUserData_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string fileContents = cacheData("AllUserCacheData.txt" , "SharedCacheData");
            var elapsedMs = watch.ElapsedMilliseconds;
            MessageBox.Show("Shared User Data:\n" + fileContents + "\nTime (Milliseconds): " + elapsedMs);
        }
        /// <summary>
        /// Getting the current file path and gose 2 levels up and navigates the the cahce folder that was created and appends the file
        /// name that was given
        /// </summary>
        /// <param name="textFileName"></param>
        /// <returns></returns>
        private string getPathOfFile(string textFileName)
        {
            string curPath = Directory.GetCurrentDirectory();
            string desirePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(curPath, @"..\..\")) + "cache\\" + textFileName;
            return desirePath;
        }
        /// <summary>
        /// The function that will be caching the data
        /// </summary>
        /// <param name="textFileName"></param>
        /// <param name="cacheId"></param>
        /// <returns></returns>
        private string cacheData(string textFileName, string cacheId)
        {
            string path = "";
            path = getPathOfFile(textFileName);
            ObjectCache cache = MemoryCache.Default;
            string fileContents = cache[cacheId] as string;
            if (fileContents == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30);

                List<string> filePaths = new List<string>();
                filePaths.Add(path);
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));
                fileContents = File.ReadAllText(path) + "\n" + DateTime.Now;
                cache.Set(cacheId, fileContents, policy);

            }
            return fileContents;
        }
        /// <summary>
        /// Showing how to access the cached data without writing to it will give error mesage if the cache expired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CacheUserData_Click(object sender, RoutedEventArgs e)
        {
            string path = getPathOfFile("UserData.txt");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Username: " + tbUserName.Text);
            sb.AppendLine("User Id: " + tbUserId.Text);
            File.WriteAllText(path, sb.ToString());
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string fileContents = cacheData("UserData.txt", "UserCacheData");
            long elapsedMs = watch.ElapsedMilliseconds;
            MessageBox.Show("Shared User Data:\n" + fileContents + "\nTime (Milliseconds): " + elapsedMs);
        }
        /// <summary>
        /// Caching single user data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnCheckCacheUserData_Click(object sender, RoutedEventArgs e)
        {
            ObjectCache cache = MemoryCache.Default;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string userCacheData = cache["UserCacheData"] as string;
            long elapsedMs = watch.ElapsedMilliseconds;

            tbCacheData.Content = userCacheData;
            tbCacheTime.Content = elapsedMs;
            if (userCacheData == null)
            {
                tbCacheData.Content = "Cannot no longer access cache data as time expired \nor no cache data(10s)";
            }
        }
        /// <summary>
        /// Clearing the cache lables 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnClearTextField_Click(object sender, RoutedEventArgs e)
        {
            tbCacheData.Content = "";
            tbCacheTime.Content = "";
        }
    }
}
