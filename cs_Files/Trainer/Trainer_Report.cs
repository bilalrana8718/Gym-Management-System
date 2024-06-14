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

namespace DB_Project.Trainer
{
    public partial class Trainer_Report : Form
    {
        string OID;
        public Trainer_Report(string o)
        {
            InitializeComponent(); 
            OID = o;
        }
        private void _1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilteringOut filteringOut = new FilteringOut();       
            filteringOut.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private List<string> gymNames = new List<string>();
        private void Trainer_Report_Load(object sender, EventArgs e)
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
                    string query = "Select trainerID, Name, Experience, TotalClients from Trainer where trainerID in (Select trainerID from WorksIn where GymID = '" + gymID + "')";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dataTable);
                }
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
