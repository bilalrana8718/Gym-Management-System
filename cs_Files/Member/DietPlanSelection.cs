using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project
{
    public partial class DietPlanSelection : Form
    {
        string MID;
        public DietPlanSelection(string mID)
        {
            InitializeComponent();
            MID = mID;
        }

        private void Dietplan_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DietPlanCreation form2form = new DietPlanCreation();
            this.Hide();
            form2form.FormClosed += Dietplan_FormClosed;
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Member.DietPlan form2form = new Member.DietPlan(MID);
            this.Hide();
            form2form.FormClosed += Dietplan_FormClosed;
            form2form.ShowDialog();
        }
    }
}
