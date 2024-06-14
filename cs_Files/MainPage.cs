using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Member
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MAINFormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Member.Member_login memberLogin = new Member.Member_login();
            memberLogin.FormClosed += MAINFormClosed;
            memberLogin.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trainer.Trainer_Login trainer_Login = new Trainer.Trainer_Login();
            trainer_Login.FormClosed += MAINFormClosed;
            trainer_Login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin.Admin_login adminLogin = new Admin.Admin_login(); 
            adminLogin.FormClosed += MAINFormClosed;
            adminLogin.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            GymOwner.Owner_login gyms = new GymOwner.Owner_login();
            gyms.FormClosed += MAINFormClosed;
            gyms.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
