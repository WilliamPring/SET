using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_SQL_Assignment_03
{
    class ConnectionClass
    {
        OleDbConnection connectionSrc;
        OleDbConnection connectionDest;
        string connectionSrcString;
        string connectionDestString;
        OleDbTransaction trans;
        string tableRecv;
        string tableSend;
        DataSet ds = new DataSet();

        public ConnectionClass(string connectionString, string connectionStringDesc, string tableReading)
        {
            connectionSrcString = connectionString;
            connectionDestString = connectionStringDesc;
            tableRecv = tableReading;
        }
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
                status = true;
            }
            return status;
        }
        public bool GetInformation()
        {
            bool status = false; 
            string s = "";

            try
            {
                connectionSrc.Open();
                string queryRecv = "Select * FROM " + tableRecv;
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryRecv, connectionSrc);
                DataTable information = new DataTable();
                adapter.Fill(ds);
                adapter.Dispose();
                connectionSrc.Close();
                
                foreach (DataRow dr in information.Rows)
                {
                    //insert dr into table
                }

                //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    s += ds.Tables[0].Rows[i].ItemArray[0] + " -- " + ds.Tables[0].Rows[i].ItemArray[1] + "\n";
                //}
            }
            catch (Exception ex)
            {
                status = true;
            }
            return status;
        }

        public bool SetInformation()
        {
            bool status = false;




            return status;
        }
          

              

    }
}
