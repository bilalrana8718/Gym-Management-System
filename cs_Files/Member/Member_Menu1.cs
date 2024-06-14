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
    public partial class Member_Menu1 : Form
    {
        string MID;
        public Member_Menu1(string MemberID)
        {
            MID = MemberID;
            InitializeComponent();
        }

        private void membermenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkoutPlanSelection workoutPlanSelection = new WorkoutPlanSelection(MID);
            workoutPlanSelection.FormClosed += membermenu_FormClosed;
            workoutPlanSelection.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DietPlanSelection dietPlanSelection = new DietPlanSelection(MID);  
            dietPlanSelection.FormClosed += membermenu_FormClosed;
            dietPlanSelection.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookTrainerSession session = new BookTrainerSession(MID);
            session.FormClosed += membermenu_FormClosed;
            session.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrainerFeedback session = new TrainerFeedback(MID);
            session.FormClosed += membermenu_FormClosed;
            session.Show();
        }

        private void Member_Menu1_Load(object sender, EventArgs e)
        {

        }
    }
}
