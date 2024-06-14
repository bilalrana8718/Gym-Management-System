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

namespace DB_Project.Trainer
{
    public partial class New_trainer : Form
    {
        public New_trainer()
        {
            InitializeComponent();
        }
        private void new_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;
            string email = textBox6.Text;
            string password = textBox4.Text;
            string experience = textBox7.Text;
            string totalclients = textBox3.Text;
            string GymID = textBox1.Text;


            if (name == "" || email == "" || password == "" || experience == "" || totalclients == "" || GymID == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string queryEmail = "SELECT COUNT(*) FROM Trainer WHERE Email = @Email";
                    SqlCommand cmdCheckEmail = new SqlCommand(queryEmail, conn);
                    cmdCheckEmail.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmdCheckEmail.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Email Already Taken");
                    }
                    else
                    {
                        string queryMaxID = "SELECT MAX(TrainerID) FROM Trainer";
                        SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                        int maxID = Convert.ToInt32(cmdMaxID.ExecuteScalar()) + 1;

                        string query = @"INSERT INTO Trainer (TrainerID, Name, Email, Password, Experience, TotalClients) 
                                 VALUES (@TrainerID, @Name, @Email, @Password, @Experience, @TotalClients)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@TrainerID", maxID);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password); // Password should not be stored in plain text. Consider hashing.
                        cmd.Parameters.AddWithValue("@Experience", experience);
                        cmd.Parameters.AddWithValue("@TotalClients", totalclients); // Assuming initially no clients

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Trainer successfully added!");

                        cmdMaxID.Dispose();
                        cmd.Dispose();
                    }
                }

                TrainerMenu form2form = new TrainerMenu();
                this.Hide();
                form2form.FormClosed += new_FormClosed;
                form2form.ShowDialog();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string name = textBox5.Text;
            string email = textBox6.Text;
            string password = textBox4.Text;
            string experience = textBox7.Text;
            string totalclients = textBox3.Text;
            string GymID = textBox1.Text;


            if (name == "" || email == "" || password == "" || experience == "" || totalclients == "" || GymID == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string queryEmail = "SELECT COUNT(*) FROM Trainer WHERE Email = @Email";
                    SqlCommand cmdCheckEmail = new SqlCommand(queryEmail, conn);
                    cmdCheckEmail.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmdCheckEmail.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Email Already Taken");
                    }
                    else
                    {
                        string queryMaxID = "SELECT MAX(TrainerID) FROM Trainer";
                        SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                        int maxID = Convert.ToInt32(cmdMaxID.ExecuteScalar()) + 1;

                        string query = @"INSERT INTO Trainer (TrainerID, Name, Email, Password, Experience, TotalClients) 
                                 VALUES (@TrainerID, @Name, @Email, @Password, @Experience, @TotalClients)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@TrainerID", maxID);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password); // Password should not be stored in plain text. Consider hashing.
                        cmd.Parameters.AddWithValue("@Experience", experience);
                        cmd.Parameters.AddWithValue("@TotalClients", totalclients); // Assuming initially no clients
                        //cmd.Parameters.AddWithValue("@GymId", GymID);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Trainer successfully added!");

                        cmdMaxID.Dispose();
                        cmd.Dispose();
                    }
                }

                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void New_trainer_Load(object sender, EventArgs e)
        {

        }
    }
}
