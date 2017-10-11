using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuseCreateOrdersCSV.Properties;

namespace NeuseCreateOrdersCSV
{
    class GetProcData
    {
        static readonly string ConnectionString = Settings.Default.ConnectionString;

        public static DataSet PopulateProcList()
        {
            DataSet procOrderList = GetProcDataList();
            return procOrderList;
        }

        public static DataSet GetProcDataList()
        {
            var procListDataSet = new DataSet();
            const string selectProcString = " SELECT *"
                                            + "  FROM orders_proc"
                                            + " where ordProc = 0";
            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var adapter = new SqlDataAdapter(selectProcString, connection);
                    adapter.Fill(procListDataSet);
                    return procListDataSet;
                }
            }
            catch (Exception e)
            {
                System.Console.Write(
                    "An error occurred trying to retrieve orders_proc" +
                    Environment.NewLine +
                    e);
                return procListDataSet;
            }
        }

        public static DataSet PopulateAllOrdersList()
        {
            var procListDataSet = new DataSet();
            const string selectProcString = " SELECT *"
                                            + "  FROM orders";
            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var adapter = new SqlDataAdapter(selectProcString, connection);
                    adapter.Fill(procListDataSet);
                    return procListDataSet;
                }
            }
            catch (Exception e)
            {
                Console.Write(
                    "An error occurred trying to retrieve orders" +
                    Environment.NewLine +
                    e);
                return procListDataSet;
            }
        }
        public static void UpdateProcRecordProcesses(string ordID)
        {
            try
            {
                SqlConnection connection;

                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var command1 = new SqlCommand("spUpdateOrderProcSentToClient", connection);

                    // define the parameters of the stored procedure

                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add(new SqlParameter("@ORDID", SqlDbType.Int, 50));

                    // fill in the parameters
                    command1.Parameters["@ORDID"].Value = ordID;
                    command1.ExecuteNonQuery();


                }
            }
            catch
                (Exception e)
            {
                System.Console.Write(e.ToString());
            }
        }
    }
}
