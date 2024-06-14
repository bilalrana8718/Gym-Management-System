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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Project.Trainer
{
    public partial class WorkoutPlanAdjustment : Form
    {
        int workoutId;
        public WorkoutPlanAdjustment(int workoutId = 0)
        {
            InitializeComponent();
            this.workoutId = workoutId;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void WorkoutPlanAdjustment_Load(object sender, EventArgs e)
        {

            comboBox1.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT ExerciseName FROM Exercise";


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
                                string Exercises = reader.GetString(0);

                                comboBox1.Items.Add(Exercises);
                                comboBox5.Items.Add(Exercises);
                                comboBox6.Items.Add(Exercises);

                            }
                        }
                    }
                }
            }

        }
    }
}
