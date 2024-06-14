using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Member
{
    public partial class Workout_plans : Form
    {
        string MID;
        
        public Workout_plans(string mid)
        {
            InitializeComponent();
            MID = mid;
        }
        private void wp_FormClosed(object sender, FormClosedEventArgs e)
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
            Member.Adjust_Work_Plan adjust_Work_Plan = new Member.Adjust_Work_Plan();
            adjust_Work_Plan.FormClosed += wp_FormClosed;
            adjust_Work_Plan.Show();

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

        private void Workout_plans_Load(object sender, EventArgs e)
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int workoutID = 0;
            string workoutName = comboBox1.Text;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryTrainerID = "SELECT workoutPlanID from WorkoutPlan where PlanName = '" + workoutName + "'";
                SqlCommand cmdMaxID = new SqlCommand(queryTrainerID, conn);
                object result = cmdMaxID.ExecuteScalar();

                workoutID = 1; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    workoutID = Convert.ToInt32(result);
                }


                // Inserting the new record
                string query = "INSERT INTO Select_Workout VALUES (@MemberID, @workoutID) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MemberID", Int32.Parse(MID));
                cmd.Parameters.AddWithValue("@workoutID", workoutID);

                cmd.ExecuteNonQuery();

                cmdMaxID.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            this.Close();
        }
    }
}
