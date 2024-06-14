using DB_Project.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.GymOwner
{
    public partial class Owner_Menu : Form
    {
        string OID;
        public Owner_Menu(string o)
        {
            InitializeComponent();
            OID = o;
        }
        private void O_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Member.member_report member_Report = new Member.member_report(OID);
            member_Report.FormClosed += O_Menu_FormClosed;
            member_Report.Show();
        }

        private void Owner_Menu_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GymReport gym_Owned = new GymReport(OID);
            gym_Owned.FormClosed += O_Menu_FormClosed;
            gym_Owned.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trainer.Trainer_Report trainer_Report = new Trainer.Trainer_Report(OID);
            trainer_Report.FormClosed += O_Menu_FormClosed;
            trainer_Report.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trainer.New_trainer new_Trainer = new Trainer.New_trainer();
            new_Trainer.FormClosed += O_Menu_FormClosed;    
            new_Trainer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin.Acc_manage acc_Manage = new Admin.Acc_manage();   
            acc_Manage.FormClosed += O_Menu_FormClosed;
            acc_Manage.Show();
        }
    }
}
