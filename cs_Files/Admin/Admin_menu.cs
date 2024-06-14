using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Admin
{
    public partial class Admin_menu : Form
    {
        public Admin_menu()
        {
            InitializeComponent();
        }

        private void admin_menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Admin_menu_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Revoke_membership_admin form1 = new Revoke_membership_admin();
            form1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gym_reporting gym_Reporting = new Gym_reporting();
            gym_Reporting.FormClosed += admin_menu_FormClosed;
            gym_Reporting.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Reg_Req_Admin form2 = new Reg_Req_Admin();
            form2.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Reg_Req_Admin reg_Req_Admin = new Reg_Req_Admin();
            reg_Req_Admin.FormClosed += admin_menu_FormClosed;
            reg_Req_Admin.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Revoke_membership_admin revoke_Membership_Admin = new Revoke_membership_admin();
            revoke_Membership_Admin.FormClosed += admin_menu_FormClosed;
            revoke_Membership_Admin.Show();
        }

        private void Admin_menu_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.FormClosed += admin_menu_FormClosed;    
            reports.Show();
        }
    }
}
