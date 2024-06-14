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

namespace DB_Project.GymOwner
{
    public partial class OwnerSignup : Form
    {
        public OwnerSignup()
        {
            InitializeComponent();
        }
        string selectedGymName;

        private void _3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            string email = textBox2.Text;
            string password = textBox5.Text;
            string GymName = comboBox1.Text;
            if (name == "" || email == "" || password == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string queryEmail = "SELECT * FROM Owner WHERE Email = '" + email + "'";
                    SqlCommand cmd1 = new SqlCommand(queryEmail, conn);
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Email Already Taken");
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();

                        string queryMaxID = "SELECT MAX(OwnerID) FROM Owner";
                        SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                        int maxID = Convert.ToInt32(cmdMaxID.ExecuteScalar());
                        int newID = maxID + 1;

                        string queryGymID = "SELECT GymID FROM Gym WHERE Gym_Name = '" + GymName + "'";
                        SqlCommand cmdGymID = new SqlCommand(queryGymID, conn);
                        reader = cmdGymID.ExecuteReader();
                        int gymID = 0;
                        if (reader.Read())
                        {
                            gymID = reader.GetInt32(0);
                        }
                        reader.Close();

                        string query = "INSERT INTO Owner VALUES (" + newID + ", '" + name + "', '" + email + "', '" + password + "', " + gymID + ")";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();



                        this.Hide();
                        Owner_Menu owner_Menu = new Owner_Menu(newID.ToString());
                        owner_Menu.FormClosed += _3_FormClosed;
                        owner_Menu.Show();

                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                selectedGymName = ((dynamic)comboBox1.SelectedItem);
            }
        }

        private void OwnerSignup_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

