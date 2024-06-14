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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            if (comboBox1.SelectedIndex == 0)
            {
                query = "Exec Query1 @Gym_name = 'fitZone', @Trainer_name = 'afnan'";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                query = "Exec Query2 @Gym_name = 'fitZone', @Diet_name = 'Lean and Fit'";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                query = "EXEC Query3 @trainer_name = 'John Smith', @Diet_name = 'Lean and Fit'";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                query = "Exec query5";
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                query = "Exec query5";
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                query = "Exec query6";
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                query = "Exec Query7 @machine_name = 'Treadmill'";
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                query = "EXEC query8 @Meal_name = 'Grilled Chicken Salad'";
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                query = "SELECT Member.Name, Member.Gender, Member.Email, Member_Report.total_time " +
               "FROM Member, Member_Report " +
               "WHERE Member.MemberID = Member_Report.memberId AND Member_Report.total_time < 3;";

            }
            else if (comboBox1.SelectedIndex == 9)
            {
                query = "EXEC query10";
            }
            else if (comboBox1.SelectedIndex == 10)
            {
                query = "Select gym.GymID, gym.Gym_Name, (Gym_Performance.Revenue * Gym_Performance.TotalMembers) as Total_Revenue from Gym, Gym_Performance where gym.GymID = 3 and Gym.PerformanceID = Gym_Performance.PerformanceID; ";
            }
            else if (comboBox1.SelectedIndex == 11)
            {
                query = "Select Trainer.TrainerID, Trainer.Name, Trainer.Experience From Trainer where Experience > 5; ";
            }
            else if (comboBox1.SelectedIndex == 12)
            {
                query = "Select top 5 gym.GymID, gym.Gym_Name, Gym_Performance.Rating from Gym, Gym_Performance where Gym.PerformanceID = Gym_Performance.PerformanceID order by Gym_Performance.Rating desc;  ";
            }
            else if (comboBox1.SelectedIndex == 13)
            {
                query = "Select Trainer.Name, Trainer.Experience, WorkoutPlan.PlanName, WorkoutPlan.Purpose from Trainer, WorkoutPlan where Trainer.TrainerID = WorkoutPlan.TrainerID and WorkoutPlan.Purpose = 'Functional Fitness'; ";
            }
            else if (comboBox1.SelectedIndex == 14)
            {
                query = "Select Member.MemberID, Member.Name, Member.Gender, Member.Height, Member.Weight, table1.progress From Member inner join (Select Workout_Report.memberId, Workout_Report.progress from Workout_Report where Workout_Report.progress > 90) as table1 on Member.MemberID = table1.memberId; ";
            }
            else if (comboBox1.SelectedIndex == 15)
            {
                query = "Select Gym.GymID, Gym.Gym_Name, Gym.Location, AVG(Member.Weight) as Average_Weight from Gym inner join Member on Gym.GymID = Member.GymID group by Gym.GymID, Gym.Gym_Name, Gym.Location;  ";
            }
            else if (comboBox1.SelectedIndex == 16)
            {
                query = "Select Table_1.MemberID, Table_1.Name, Table_1.Email From (Select Member.MemberID, Member.Name, Member.Email, Workout_Report.progress   From Member   inner Join Workout_Report   on Member.MemberID = Workout_Report.memberId   where Workout_Report.progress > 80) as Table_1 inner join  (Select DietPlan.DietPlanID, DietPlan.Purpose, Select_dietplan.MemberID   from DietPlan   inner join select_dietplan   on DietPlan.DietPlanID = select_dietplan.DietPlanID   where Purpose = 'Muscle Building') as Table_2 on Table_1.MemberID = Table_2.MemberID; ";
            }
            else if (comboBox1.SelectedIndex == 17)
            {
                query = "Select Member.Name, Member.Gender,Member.Height, Member.Weight, Member_Report.progress, Member_Report.total_time From Member, Member_Report where Member.MemberID = Member_Report.memberId and Member_Report.progress > 80; ";
            }
            else if (comboBox1.SelectedIndex == 18)
            {
                query = "Select Trainer.Name, COUNT(Apppointment.TrainerID) as Number_of_Appointments from Trainer inner join Apppointment on Trainer.TrainerID = Apppointment.TrainerID group by Trainer.TrainerID, Trainer.Name; ";
            }
            else if (comboBox1.SelectedIndex == 19)
            {
                query = "Select top 1 Trainer.TrainerID, Trainer.Name, Trainer.Experience, AVG(Feedback.rating) AS Average_Rating From Trainer inner join Feedback on Trainer.TrainerID = Feedback.TrainerID group by Trainer.TrainerID, Trainer.Name, Trainer.Experience order by Average_Rating desc;";
            }
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                adapter.Fill(dataTable);
            }
            dataGridView1.DataSource = dataTable;
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }
    }
}
