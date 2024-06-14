using DB_Project.Member;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
        }
    }

    public class SqlConnectionManager
    {
        private string _connectionString;

        public SqlConnectionManager()
        {
            _connectionString = "Data Source=BILAL_AKBAR\\SQLEXPRESS;Initial Catalog=Project;Integrated Security=True";
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}
