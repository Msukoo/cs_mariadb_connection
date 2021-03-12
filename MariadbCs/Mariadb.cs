using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MariadbCs
{
    class Mariadb
    {
        private static Mariadb instance = new Mariadb(); 
        public static Mariadb Instance { get { return instance; } }

        MySqlConnection connection;
        
        public string database { get; set; }
        public string server { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string port { get; set; }

        private String connectionString; 
        
        public Mariadb() 
        {
            //database = "testdb";
            //server = "125.132.149.3";
            //user = "vcanus";
            //password = "1234";

        }
        public Boolean ConnectionDB()
        {
            if (IsConnection())
            {
                return true;
            }
            try
            {
                connectionString = "server=" + server + ";port=" + port + ";user=" + user + ";database=" + database + ";password=" + password;
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
            
        }

        public void disConnection()
        {
            if (IsConnection())
            {
                connection.Close();
                connection = null;
            }
        }

        public Boolean IsConnection()
        {
            if (connection == null)
            {
                return false;
            }
            return true;
        }
        

        public Boolean CommandDB(String sql) 
        {         
            try
            {
                if (ConnectionDB())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, connection);
                    if (mySqlCommand.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                    disConnection();
                }         
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }
                   
        public DataTable SelectDB(String sql)
        {
            var dataTable = new DataTable();
            try
            {
                if (ConnectionDB())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, connection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    dataTable.Load(mySqlDataReader);
                    //StringBuilder output = new StringBuilder();
                    //foreach (DataColumn col in dataTable.Columns)
                    //{
                    //    output.AppendFormat("{0} ", col);
                    //}
                    //output.AppendLine();
                    //foreach (DataRow page in dataTable.Rows)
                    //{
                    //    foreach (DataColumn col in dataTable.Columns)
                    //    {
                    //        output.AppendFormat("{0} ", page[col]);
                    //    }
                    //    output.AppendLine();
                    //}
                    //mySqlDataReader.Close();
                    //MessageBox.Show(output.ToString());
                    disConnection();
                }
            } catch (Exception e) 
            {
                MessageBox.Show(e.ToString());
            }
            return dataTable;
        }
           


    }
}
