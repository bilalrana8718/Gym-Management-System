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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DB_Project.Admin
{
    public partial class Revoke_membership_admin : Form
    {
        public Revoke_membership_admin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Revoke_membership_admin_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT GymID, Gym_Name FROM Gym";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No gyms found.");
                        }
                        else
                        {
                            comboBox1.Items.Clear();

                            while (reader.Read())
                            {
                                int gymID = reader.GetInt32(0);
                                string gymName = reader.GetString(1);

                                comboBox1.Items.Add(gymName);
                            }
                        }
                    }
                }
            }

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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox5.Text;
            if (id == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                        string query1 = "DELETE FROM Member_Report WHERE MemberID = @Id";
                        SqlCommand cmd1 = new SqlCommand(query1, conn);
                        cmd1.Parameters.AddWithValue("@Id", id);
                        cmd1.ExecuteNonQuery();

                        string query = "DELETE FROM Member WHERE MemberID = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Member successfully Removed!");
                        cmd.Dispose();
                    
                }

                this.Close();
            }
        }
    }
}
