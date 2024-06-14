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

namespace DB_Project.Admin
{
    public partial class Admin_login : Form
    {
        public Admin_login()
        {
            InitializeComponent();
        }

        private void ad_log_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_menu admin_Login = new Admin_menu();
            admin_Login.Show();
            // this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GymOwner.Owner_login f1 = new GymOwner.Owner_login();
            f1.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Admin_signup admin_Login = new Admin_signup();
            admin_Login.Show();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string email = textBox2.Text;
            string password = textBox5.Text;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            SqlConnection conn = connectionManager.GetConnection();
            conn.Open();

            string query = "select * from Admin where Email = '" + email + "' and Password = '" + password + "'";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                this.Hide();
                Admin.Admin_menu admin_menu = new Admin_menu();
                admin_menu.FormClosed += ad_log_FormClosed;
                admin_menu.Show();
            }
            else
            {
                MessageBox.Show("Wrong Email or Password");
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Admin.AdminSignup admin_Signup = new AdminSignup();
            admin_Signup.FormClosed += ad_log_FormClosed;
            admin_Signup.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
