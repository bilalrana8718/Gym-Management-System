using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.GymOwner
{
    public partial class GymReport : Form
    {
         string OID;
        public GymReport(string O)
        {
            InitializeComponent();
            OID = O;
        }

        private void GymReport_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                string query = "Select g.GymID, g.Gym_Name, g.Location, gp.TotalMembers, gp.Active_Attendence, gp.Revenue, gp.Rating from Gym g join Gym_Performance gp on g.PerformanceID = gp.PerformanceID where OwnerID = '" + OID+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                adapter.Fill(dataTable);
            }
            dataGridView1.DataSource = dataTable;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
