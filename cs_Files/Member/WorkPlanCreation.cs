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

namespace DB_Project
{
    public partial class WorkPlanCreation : Form
    {
        string MID;
        public WorkPlanCreation(string m)
        {
            InitializeComponent();
            MID = m;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WorkPlanCreation_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox9.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();
            comboBox13.Items.Clear();
            comboBox14.Items.Clear();
            comboBox15.Items.Clear();
            comboBox16.Items.Clear();
            comboBox17.Items.Clear();
            comboBox18.Items.Clear();
            comboBox19.Items.Clear();
            comboBox20.Items.Clear();
            comboBox21.Items.Clear();

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT ExerciseId FROM Exercise";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No Exercise found.");
                        }
                        else
                        {

                            comboBox1.Items.Clear();

                            while (reader.Read())
                            {
                                //(reader.GetString(1));
                                //int gymID = reader.GetInt32(0);
                                int Exercises = reader.GetInt32(0);

                                comboBox1.Items.Add(Exercises);
                                comboBox2.Items.Add(Exercises);
                                comboBox3.Items.Add(Exercises);
                                comboBox4.Items.Add(Exercises);
                                comboBox5.Items.Add(Exercises);
                                comboBox6.Items.Add(Exercises);
                                comboBox7.Items.Add(Exercises);
                                comboBox8.Items.Add(Exercises);
                                comboBox9.Items.Add(Exercises);
                                comboBox10.Items.Add(Exercises);
                                comboBox11.Items.Add(Exercises);
                                comboBox12.Items.Add(Exercises);
                                comboBox13.Items.Add(Exercises);
                                comboBox14.Items.Add(Exercises);
                                comboBox15.Items.Add(Exercises);
                                comboBox16.Items.Add(Exercises);
                                comboBox17.Items.Add(Exercises);
                                comboBox18.Items.Add(Exercises);
                                comboBox19.Items.Add(Exercises);
                                comboBox20.Items.Add(Exercises);
                                comboBox21.Items.Add(Exercises);
                            }
                        }
                    }
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int insertIntoTable(System.Windows.Forms.ComboBox[] combobox, NumericUpDown[] sets, NumericUpDown[] reps)
        {
            int newID;
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryMaxID = "SELECT MAX(exercise_set_repID) FROM exercise_set_rep";
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                object result = cmdMaxID.ExecuteScalar();

                int maxID = 0; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    maxID = Convert.ToInt32(result);
                }

                newID = maxID + 1;

                for (int i = 0; i < 3; i++)
                {
                    if (combobox[i].Text != "")
                    {


                        int exerciseID = Int32.Parse(combobox[i].Text);


                        string query = "INSERT INTO exercise_set_rep VALUES (@exercise_set_repID, @exerciseNum, @ExerciseID, @Sets, @reps) ";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@exercise_set_repID", newID);
                        cmd.Parameters.AddWithValue("@exerciseNum", i + 1);
                        cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);
                        cmd.Parameters.AddWithValue("@Sets", (int)sets[i].Value);
                        cmd.Parameters.AddWithValue("@reps", (int)reps[i].Value);

                        cmd.ExecuteNonQuery();

                        cmdMaxID.Dispose();
                        cmd.Dispose();
                    }
                }
                conn.Close();

            }
            return newID;

        }

        private void insert_WorkoutPlan_Exercise(int workoutID, string day, System.Windows.Forms.ComboBox[] combobox, NumericUpDown[] sets, NumericUpDown[] reps)
        {
            int exercise_set_repID = insertIntoTable(combobox, sets, reps);

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string checkIfinExerciseSetsReps = "Select Exercise_set_repID from Exercise_set_rep where Exercise_set_repID = " + exercise_set_repID;
                SqlCommand cmd1 = new SqlCommand(checkIfinExerciseSetsReps, conn);
                object result = cmd1.ExecuteScalar();

                if (result == DBNull.Value || result == null)
                {
                    return;
                }


                string query = "INSERT INTO WorkoutPlan_Exercise VALUES (@exercise_set_repID,@ExerciseNum, @WorkoutPlanID, @day) ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@exercise_set_repID", exercise_set_repID);
                cmd.Parameters.AddWithValue("@ExerciseNum", 1);
                cmd.Parameters.AddWithValue("@WorkoutPlanID", workoutID);
                cmd.Parameters.AddWithValue("@day", day);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //insert into workoutplan table
            int workoutID;
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryMaxID = "SELECT MAX(WorkoutPlanID) FROM WorkoutPlan";
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                object result = cmdMaxID.ExecuteScalar();

                int maxID = 0; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    maxID = Convert.ToInt32(result);
                }

                workoutID = maxID + 1;

                // Inserting the new record
                string query = "INSERT INTO WorkoutPlan VALUES (@WorkoutPLanID, @PlanName, @Purpose, @TrainerID) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@WorkoutPLanID", workoutID);
                cmd.Parameters.AddWithValue("@PlanName", textBox1.Text);
                cmd.Parameters.AddWithValue("@Purpose", comboBox22.Text);
                cmd.Parameters.AddWithValue("@TrainerID", DBNull.Value);


                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cmdMaxID.Dispose();
                conn.Close();
            }

            //Call for Monday
            System.Windows.Forms.ComboBox[] combobox = { comboBox1, comboBox6, comboBox5 };
            NumericUpDown[] sets = { numericUpDown1, numericUpDown3, numericUpDown2 }, reps = { numericUpDown42, numericUpDown40, numericUpDown41 };

            insert_WorkoutPlan_Exercise(workoutID, "Monday", combobox, sets, reps);


            //Tuesday
            combobox[0] = comboBox4; combobox[1] = comboBox2; combobox[2] = comboBox3;
            sets[0] = numericUpDown36; sets[1] = numericUpDown34; sets[2] = numericUpDown35;
            reps[0] = numericUpDown6; reps[1] = numericUpDown4; reps[2] = numericUpDown5;

            insert_WorkoutPlan_Exercise(workoutID, "Tuesday", combobox, sets, reps);



            //Wednesday
            combobox[0] = comboBox9; combobox[1] = comboBox7; combobox[2] = comboBox8;
            sets[0] = numericUpDown39; sets[1] = numericUpDown37; sets[2] = numericUpDown38;
            reps[0] = numericUpDown9; reps[1] = numericUpDown7; reps[2] = numericUpDown8;

            insert_WorkoutPlan_Exercise(workoutID, "Wednesday", combobox, sets, reps);



            //Thursday
            combobox[0] = comboBox12; combobox[1] = comboBox10; combobox[2] = comboBox11;
            sets[0] = numericUpDown33; sets[1] = numericUpDown31; sets[2] = numericUpDown32;
            reps[0] = numericUpDown12; reps[1] = numericUpDown10; reps[2] = numericUpDown11;

            insert_WorkoutPlan_Exercise(workoutID, "Thursday", combobox, sets, reps);



            //Friday
            combobox[0] = comboBox15; combobox[1] = comboBox13; combobox[2] = comboBox14;
            sets[0] = numericUpDown30; sets[1] = numericUpDown28; sets[2] = numericUpDown29;
            reps[0] = numericUpDown15; reps[1] = numericUpDown13; reps[2] = numericUpDown14;

            insert_WorkoutPlan_Exercise(workoutID, "Friday", combobox, sets, reps);



            //Saturday
            combobox[0] = comboBox18; combobox[1] = comboBox16; combobox[2] = comboBox17;
            sets[0] = numericUpDown21; sets[1] = numericUpDown19; sets[2] = numericUpDown20;
            reps[0] = numericUpDown18; reps[1] = numericUpDown16; reps[2] = numericUpDown17;

            insert_WorkoutPlan_Exercise(workoutID, "Saturday", combobox, sets, reps);



            //Sunday
            combobox[0] = comboBox21; combobox[1] = comboBox19; combobox[2] = comboBox20;
            sets[0] = numericUpDown27; sets[1] = numericUpDown25; sets[2] = numericUpDown26;
            reps[0] = numericUpDown24; reps[1] = numericUpDown22; reps[2] = numericUpDown23;

            insert_WorkoutPlan_Exercise(workoutID, "Sunday", combobox, sets, reps);


            this.Close();
        }
    }
}
