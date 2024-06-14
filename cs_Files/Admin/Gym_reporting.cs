using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Admin
{
    public partial class Gym_reporting : Form
    {
        public Gym_reporting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Gym_reporting_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                string query = "Select g.GymID, g.Gym_Name, g.Location, gp.TotalMembers, gp.Active_Attendence, gp.Revenue, gp.Rating from Gym g join Gym_Performance gp on g.PerformanceID = gp.PerformanceID ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                adapter.Fill(dataTable);
            }
            dataGridView1.DataSource = dataTable;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
