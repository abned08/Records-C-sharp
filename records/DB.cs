using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace records
{
    class DB
    {
        public static SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" +Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\recordsDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=False;");
        public static SqlCommand cmd =new SqlCommand("",conn);

        public static void ChangeDBFileName (String NewPathWithFileName)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\" + NewPathWithFileName +";Integrated Security=True;Connect Timeout=30";         
            }
        }
        public static void Open()
        {
            if (conn.State == ConnectionState.Closed)
                try
                {
                    conn.Open();
                    
                }catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
        }
        public static void Close()
        {
            if (conn.State == ConnectionState.Open)
                try
                {
                   conn.Close(); 

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            
        }
        
        public static DataTable GetData (String Select)
        {
            try
            {
                Open();
                
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
               
            }
            DataTable tbl = new DataTable();
                cmd.CommandText = Select;
                tbl.Load(cmd.ExecuteReader());
                Close();
                return tbl;
            
        }

        public static void Run(String Sql)
        {
            Open();
            cmd.CommandText = Sql;
            cmd.ExecuteNonQuery();
            Close();
        }
    }
}
