using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DB_Project.GymOwner
{
    public partial class Owner_login : Form
    {
        public Owner_login()
        {
            InitializeComponent();
        }

        private void owner_login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Owner_login_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerSignup owner_Signup = new OwnerSignup();
            owner_Signup.FormClosed += owner_login_FormClosed;
            owner_Signup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text;
            string password = textBox5.Text;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            SqlConnection conn = connectionManager.GetConnection();
            conn.Open();

            string query = "SELECT OwnerID FROM Owner WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Check if there's a row
                        {
                            int ownerID = reader.GetInt32(0); // Assuming OwnerID is of type INT
                            this.Hide();
                            Owner_Menu ownerMenu = new Owner_Menu(ownerID.ToString());
                            ownerMenu.FormClosed += owner_login_FormClosed;
                            ownerMenu.Show();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Email or Password");
                        }
                    }
                }
            
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
