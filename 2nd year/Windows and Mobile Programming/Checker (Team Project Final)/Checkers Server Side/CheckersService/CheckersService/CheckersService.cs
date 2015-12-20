/*
    FILE            : CheckersService.cs
    PROJECT         : WMP Final project
    PROGRAMMER      : Denys Politiuk
    FIRST VERSION   : 2015-12-03
    DESCRIPTION     :
        Class that contains the logic for the checkers service
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using CheckersLib;
using System.Threading;

namespace CheckersService
{
    /// <summary>
    /// Service that will run the CheckersServer
    /// </summary>
    public partial class CheckersService : ServiceBase
    {
        private CheckersServer server;
        private Thread t;

        /// <summary>
        /// Constructor for the service
        /// </summary>
        public CheckersService()
        {
            InitializeComponent();
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            this.server = new CheckersServer(9000);
        }

        /// <summary>
        /// OnStart event to start the service
        /// </summary>
        /// <param name="args">Arguments</param>
        protected override void OnStart(string[] args)
        {
            this.t = new Thread(new ThreadStart(Run));
            
            Logger.LogEvent("OnStart","Starting Service");

            t.Start();
        }

        /// <summary>
        /// OnStop event to stop the service
        /// </summary>
        protected override void OnStop()
        {            
            Logger.LogEvent("OnStop", "Stopping Service");

            try
            {
                this.server.Stop();
                t.Join();
            }
            catch (Exception e)
            {
                Logger.LogEvent("OnStop", e.Message);
            }
        }

        /// <summary>
        /// Thread method that runs the service
        /// </summary>
        private void Run()
        {
            try
            {
                this.server.Start();
            }
            catch (Exception e)
            {                
                Logger.LogEvent("Run", e.Message);
            }
        }
    }
}
