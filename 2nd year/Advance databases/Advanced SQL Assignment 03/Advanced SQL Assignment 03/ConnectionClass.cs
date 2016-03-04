/*
* Filename:	ConnectionClass.cs
* Project:	ASQL: Assignment 3 - Programming Abstractions
* By:		William Pring
* Date:		March 3, 2016
* Description:	This Class will provide supporting classes for the OleDBConnection. It will provide the connection for the stream souce
* it will provide the extraction and the inserting of the data
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_SQL_Assignment_03
{
    /// <summary>
    /// This connection class oversees the OleDbConnections and its supporting methods
    /// </summary>
    class ConnectionClass
    {
        //varible
        private OleDbConnection connectionSrc;
        private OleDbConnection connectionDest;
        private string connectionSrcString;
        private string connectionDestString;
        private OleDbTransaction trans;
        private string tableRecv;
        private string tableSend;
        private DataTable ds = new DataTable();
        /// <summary>
        /// Constructor that inits all the variable and set the project up
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="connectionStringDesc"></param>
        /// <param name="tableReading"></param>
        /// <param name="tableWrite"></param>
        public ConnectionClass(string connectionString, string connectionStringDesc, string tableReading, string tableWrite)
        {
            connectionSrcString = connectionString;
            connectionDestString = connectionStringDesc;
            tableRecv = tableReading;
            tableSend = tableWrite;
        }
        /// <summary>
        /// Assign the Oledbconnections to the connection string
        /// </summary>
        /// <returns>Status</returns>
        public bool Connect()
        {
            bool status = false;
            try
            {
                connectionSrc = new OleDbConnection(connectionSrcString);
                connectionDest = new OleDbConnection(connectionDestString);
            }
            catch (Exception ex)
            {
                //return true once it fails
                status = true;
            }
            return status;
        }
        /// <summary>
        /// Reciving information in the database and puts into into the datatable
        /// </summary>
        /// <returns></returns>
        public bool GetInformation()
        {
            bool status = false;
            try
            {
                //open connection
                connectionSrc.Open();
                //create query
                string queryRecv = "Select * FROM [" + tableRecv + "]";
                //assign it to an adapter
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryRecv, connectionSrc);
                //DataTable gets filled
                adapter.Fill(ds);
                //dispose
                adapter.Dispose();
                //close connection
                connectionSrc.Close();
            }
            catch (Exception ex)
            {
                status = true;
            }
            return status;
        }
        /// <summary>
        /// Set the information to the database
        /// </summary>
        /// <returns></returns>

        public bool SetInformation()
        {
            OleDbCommand command = new OleDbCommand();
            string query = "";
            int errorOrNot = 0;
            bool status = false;
            try
            {
                command.Connection = connectionDest;
                //open connection
                connectionDest.Open();
                //start it
                trans = connectionDest.BeginTransaction();
                //ransaction
                command.Transaction = trans;

                try
                {
                    OleDbParameter pr = new OleDbParameter();
                    //iter to the datarow
                    foreach (DataRow dr in ds.Rows)
                    {

                        command.Parameters.Clear();
                        //create the statement
                        query = "INSERT INTO [" + tableSend + "] VALUES(";
                        for (int i = 0; i < dr.ItemArray.Length; i++)
                        {
                            query += "?";
                            pr = new OleDbParameter();
                            pr.ParameterName = i.ToString();
                            pr.Value = dr.ItemArray[i];
                            //adding it to the parmeters
                            command.Parameters.Add(pr);
                            //continue by adding a comma if their is more information
                            if (i != (dr.ItemArray.Length - 1))
                            {
                                query += ",";
                            }
                        }
                        //end the query
                        query += ");";

                        command.CommandText = query;
                        //execute it
                        errorOrNot = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    status = true;
                    //rollback all the changes if their is a problem
                    trans.Rollback();
                }
            }
            catch (Exception ex)
            {
                status = true;
            }
            //commit it
            if (status != true)
            {
                trans.Commit();
            }

            return status;
        }
    }
}

