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
        private OleDbConnection connectionSrc;
        private OleDbConnection connectionDest;
        private string connectionSrcString;
        private string connectionDestString;
        private OleDbTransaction trans;
        private string tableRecv;
        private string tableSend;

        DataTable ds = new DataTable();

        public ConnectionClass(string connectionString, string connectionStringDesc, string tableReading, string tableWrite)
        {
            connectionSrcString = connectionString;
            connectionDestString = connectionStringDesc;
            tableRecv = tableReading;
            tableSend = tableWrite;
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
            try
            {
                connectionSrc.Open();
                string queryRecv = "Select * FROM " + tableRecv;
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryRecv, connectionSrc);
                adapter.Fill(ds);
                adapter.Dispose();
                connectionSrc.Close();
            }
            catch (Exception ex)
            {
                status = true;
            }
            return status;
        }


        public bool SetInformation()
        {
            OleDbCommand command = new OleDbCommand();
            string query = "";
            int errorOrNot = 0;
            bool status = false;
            try
            {
                command.Connection = connectionDest;

                connectionDest.Open();
                trans = connectionDest.BeginTransaction();
                command.Transaction = trans;

                try
                {
                    foreach (DataRow dr in ds.Rows)
                    {
                        query = "INSERT INTO " + tableSend + " VALUES('";
                        for (int i = 0; i < dr.ItemArray.Length; i++)
                        {
                            query += dr.ItemArray[i].ToString();
                            if (i != (dr.ItemArray.Length - 1))
                            {
                                query += "','";
                            }
                        }
                        query += "');";
                        command.CommandText = query;
                        errorOrNot = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            catch (Exception ex)
            {
                status = true;
            }



            return status;
        }
    }
}

