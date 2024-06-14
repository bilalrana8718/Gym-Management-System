using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Project.Member
{
    public partial class Member_login : Form
    {
        public Member_login()
        {
            InitializeComponent();
        }
        string MID;
        private void Member_Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string Email = textBox2.Text;
            string Password = textBox5.Text;

            if (Email == "" || Password == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                SqlConnection conn = connectionManager.GetConnection();
                conn.Open();

                string query = " select MemberID from Member where Email = '" + Email + "' and Password = '" + Password + "'";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    MID = reader.GetInt32(0).ToString();
                    this.Hide();
                    Member_Menu1 member_Menu = new Member_Menu1(MID);
                    member_Menu.FormClosed += Member_Login_FormClosed;
                    member_Menu.Show();
                }
                else
                {
                    MessageBox.Show("Wrong Email or Password");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Member.MemberSignup memberSignup = new Member.MemberSignup();
            memberSignup.FormClosed += Member_Login_FormClosed; 
            memberSignup.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Member_login_Load(object sender, EventArgs e)
        {

        }
    }
}
