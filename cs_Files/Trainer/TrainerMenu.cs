using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Trainer
{
    public partial class TrainerMenu : Form
    {
        int TrainerID;
        public TrainerMenu(int TID = 0)
        {
            InitializeComponent();
            TrainerID = TID;
        }
        private void trainer_menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            w_plans_view appointmentView = new w_plans_view();
            appointmentView.FormClosed += trainer_menu_FormClosed;
            appointmentView.Show(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExerciseCreation exerciseCreation = new ExerciseCreation();
            exerciseCreation.FormClosed += trainer_menu_FormClosed;
            exerciseCreation.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            diet_plan diet_Plan = new diet_plan(TrainerID);
            diet_Plan.FormClosed += trainer_menu_FormClosed;    
            diet_Plan.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkoutPlanCreation workoutPlanCreation = new WorkoutPlanCreation(TrainerID);    
            workoutPlanCreation.FormClosed += trainer_menu_FormClosed;
            workoutPlanCreation.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MealCreation mealCreation = new MealCreation(); 
            mealCreation.FormClosed += trainer_menu_FormClosed; 
            mealCreation.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DietPlanCreation dietPlanCreation = new DietPlanCreation(TrainerID);
            dietPlanCreation.FormClosed += trainer_menu_FormClosed;
            dietPlanCreation.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentBooking appointmentBooking = new AppointmentBooking(TrainerID);
            appointmentBooking.FormClosed += trainer_menu_FormClosed;
            appointmentBooking.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrainerFeedback feedback = new TrainerFeedback(TrainerID);   
            feedback.FormClosed += trainer_menu_FormClosed; 
            feedback.Show();
        }
    }
}
