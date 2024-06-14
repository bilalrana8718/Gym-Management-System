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
    public partial class MemberSignup : Form
    {
        string selectedGymName;
        public MemberSignup()
        {
            InitializeComponent();
        }
        string MID;
        private void Member_Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void MemberSignup_Load(object sender, EventArgs e)
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
            Member_login member_Login = new Member_login();
            this.Hide();
            member_Login.FormClosed += Member_Login_FormClosed;
            member_Login.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string FirstName = textBox6.Text;
            string SecondName = textBox4.Text;
            string Age = numericUpDown3.Value.ToString();
            string Height = numericUpDown2.Value.ToString();
            string Gender = comboBox2.Text;
            string Weight = numericUpDown1.Value.ToString();
            string Email = textBox2.Text;
            string GymName = selectedGymName;
            string Password = textBox5.Text;
            string fullName = FirstName + " " + SecondName;

            if (FirstName == "" || SecondName == "" || Age == "" || Height == "" || Gender == "" || Weight == "" || Email == "" || GymName == "" || Password == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                SqlConnection conn = connectionManager.GetConnection();

                    conn.Open();
                    
                string queryEmail = "select * from Member where Email = '" + Email + "'";
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

                    string queryMaxID = "SELECT MAX(MemberID) FROM Member";
                    SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                    int maxID = Convert.ToInt32(cmdMaxID.ExecuteScalar());
                    int newID = maxID + 1;
                    MID = newID.ToString();

                    string queryGymID = "SELECT GymID FROM Gym WHERE Gym_Name = '" + GymName + "'";
                    SqlCommand cmdGymID = new SqlCommand(queryGymID, conn);
                    reader = cmdGymID.ExecuteReader();
                    int gymID = 0;
                    if (reader.Read())
                    {
                        gymID = reader.GetInt32(0);
                    }
                    reader.Close();

                    string query = "INSERT INTO Member VALUES (" + newID + ", '" + fullName + "', " + Age + ", " + Height + ", " + Weight + ", '" + Gender + "', '" + Email + "', '" + Password + "'," + gymID + ")";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.ExecuteNonQuery();

                    cmdMaxID.Dispose();
                    cmd.Dispose();
                    conn.Close();

                    //Bilal Add the opening of the form here
                    this.Hide();
                    Member_Menu1 member_Menu1 = new Member_Menu1(MID);
                    member_Menu1.Show();
                    member_Menu1.FormClosed += Member_Login_FormClosed;
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
