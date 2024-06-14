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

namespace DB_Project.Trainer
{
    public partial class Trainer_Login : Form
    {
        int TrainerID;
        public Trainer_Login()
        {
            InitializeComponent();
        }
        private void trainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text;
            string password = textBox5.Text;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            SqlConnection conn = connectionManager.GetConnection();
            conn.Open();

            string query = "select * from Trainer where Email = '" + email + "' and Password = '" + password + "'";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                this.Hide();
                reader.Read();
                TrainerID = reader.GetInt32(0);
                TrainerMenu trainerMenu = new TrainerMenu(TrainerID);
                trainerMenu.FormClosed += trainer_FormClosed;
                trainerMenu.Show();
            }

            else
            {
                MessageBox.Show("Wrong Email or Password");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            trainer_signup trainer_Signup = new trainer_signup();
            trainer_Signup.Show();
            trainer_Signup.FormClosed += trainer_FormClosed;
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
