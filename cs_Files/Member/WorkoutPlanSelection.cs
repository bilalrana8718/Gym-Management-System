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
    public partial class WorkoutPlanSelection : Form
    {
        string MID;
        public WorkoutPlanSelection(string m)
        {
            MID = m;
            InitializeComponent();
        }
        private void wps_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkPlanCreation workPlanCreation = new WorkPlanCreation(MID);
            workPlanCreation.FormClosed += wps_FormClosed;
            workPlanCreation.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Member.Workout_plans workout_Plans = new Member.Workout_plans(MID);
            workout_Plans.FormClosed += wps_FormClosed;
            workout_Plans.Show();
        }

        private void Workout_Plans_FormClosed(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WorkoutPlanSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
