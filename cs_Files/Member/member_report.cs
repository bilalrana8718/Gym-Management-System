using DB_Project.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Member
{
    public partial class member_report : Form
    {
        string OID;
        public member_report(string o)
        {
            OID = o;
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            filter.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private List<string> gymNames = new List<string>();

        private void member_report_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT Gym_Name FROM Gym where OwnerID = '" + OID + "'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No GYMS found found.");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                string gymName = reader.GetString(0);
                                gymNames.Add(gymName);
                            }
                        }
                    }
                }
            }

            comboBox1.Items.AddRange(gymNames.ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedGymName = comboBox1.SelectedItem.ToString();
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                DataTable dataTable = new DataTable();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();
                    string queryGymID = "SELECT GymID FROM Gym WHERE Gym_Name = '" + selectedGymName + "'";
                    SqlCommand cmdGymID = new SqlCommand(queryGymID, conn);
                    int gymID = (int)cmdGymID.ExecuteScalar();
                    string query = "Select M.MemberID, Name, Gender, progress, total_time from Member m join Member_report mr on m.Memberid = mr.MemberID where m.gymID = '" + gymID + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dataTable);
                }
                dataGridView1.DataSource = dataTable;
            }
        }

    }
}
