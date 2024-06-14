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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DB_Project.Trainer
{
    public partial class trainer_signup : Form
    {

        string selectedGymName;
        public trainer_signup()
        {
            InitializeComponent();
        }
        private void trainer1_Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Trainer_Login trainer_Login = new Trainer_Login();
            trainer_Login.Show();
            this.Hide();
            trainer_Login.FormClosed += trainer1_Login_FormClosed;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            string email = textBox2.Text;
            string totalClients = textBox7.Text;
            string experiene = textBox1.Text;
            string password = textBox5.Text;
            string GymName = selectedGymName;

            if(name == "" || email == "" || totalClients =="" || experiene == "" || password == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                SqlConnection conn = connectionManager.GetConnection();

                    conn.Open();

                string queryEmail = "Use Project; select * from Trainer where Email = '" + email + "'";
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
                    string queryMaxID = "Use Project;SELECT MAX(TrainerID) FROM Trainer";
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

                    string query = "INSERT INTO Trainer VALUES (" + newID + ", '" + name + "', '" + email + "', '" + password + "', " + experiene + ", " + totalClients + "," + gymID + ")";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.ExecuteNonQuery();

                    cmdMaxID.Dispose();
                    cmd.Dispose();
                    conn.Close();

                    //Bilal Add the opening of the form here
                    this.Hide();
                    TrainerMenu trainerMenu = new TrainerMenu();
                    trainerMenu.Show();
                    trainerMenu.FormClosed += trainer1_Login_FormClosed;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void trainer_signup_Load(object sender, EventArgs e)
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
                selectedGymName = ((dynamic)comboBox1.SelectedItem);
            }
        }
    }
}
