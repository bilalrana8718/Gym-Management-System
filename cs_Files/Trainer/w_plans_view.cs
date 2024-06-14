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

namespace DB_Project.Trainer
{
    public partial class w_plans_view : Form
    {
        public w_plans_view()
        {
            InitializeComponent();
        }

        private void trainer_2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkoutPlanAdjustment workout_PlanAdjustment = new WorkoutPlanAdjustment();
            workout_PlanAdjustment.FormClosed += trainer_2_FormClosed;
            workout_PlanAdjustment.Show();
        }

        private void w_plans_view_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT PlanName FROM WorkoutPlan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No Workout plan found.");
                        }
                        else
                        {
                            comboBox1.Items.Clear();

                            while (reader.Read())
                            {
                                string gymName = reader.GetString(0);

                                comboBox1.Items.Add(gymName);
                            }
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string workoutName;

            if (comboBox1.SelectedItem != null)
            {
                workoutName = ((dynamic)comboBox1.SelectedItem);


                SqlConnectionManager connectionManager = new SqlConnectionManager();
                DataTable dataTable = new DataTable();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT wp.PlanName AS WorkoutPlan, we.Day, esr.ExerciseID, e.Target_Muscle AS TargetMuscle, e.Machine, esr.Sets, esr.Reps FROM WorkoutPlan_Exercise we INNER JOIN Exercise_Set_Rep esr ON we.Exercise_Set_RepID = esr.Exercise_Set_RepID INNER JOIN WorkoutPlan wp ON we.WorkoutPlanID = wp.WorkoutPlanID INNER JOIN Exercise e ON esr.ExerciseID = e.ExerciseID Where wp.PlanName = '" + workoutName + "';";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    // Fill the DataTable with the data from the query
                    adapter.Fill(dataTable);
                }
                dataGridView1.DataSource = dataTable;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
