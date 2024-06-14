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
    public partial class diet_plan : Form
    {
        int trainerId;
        public diet_plan(int trainerId = 0)
        {
            InitializeComponent();
            this.trainerId = trainerId;
            diet_plan_Load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void diet_plan_Load()
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT DietPlanId FROM DietPlan where trainerID = " + trainerId;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No diet plans found.");
                        }
                        else
                        {
                            comboBox1.Items.Clear();

                            while (reader.Read())
                            {
                                int workoutID = reader.GetInt32(0);

                                comboBox1.Items.Add(workoutID);
                            }
                        }
                    }
                }
            }
        }

        private void diet_plan_Load(object sender, EventArgs e)
        {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                DataTable dataTable = new DataTable();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT dp.DietPlanID, dp.DietPlan_Name, dp.Type_of_Diet, dp.Purpose, dm.Time_, m.name AS MealName, m.carbs, m.protein, m.fats, m.fibre FROM DietPlan dp JOIN DietPlanMeals dm ON dp.DietPlanID = dm.DietPlanID JOIN Meal m ON dm.MealID = m.MealID where dp.trainerid = " + trainerId;
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    adapter.Fill(dataTable);
                }
                dataGridView1.DataSource = dataTable;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
                return;

            int dietID = Int32.Parse(comboBox1.Text);

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                //delete from dietplanmeals table
                string query3;
                SqlCommand cmd3;
                query3 = "delete from dietplanMeals where dietplanID = " + dietID;
                cmd3 = new SqlCommand(query3, conn);
                cmd3.ExecuteNonQuery();


                //deleting from dietplan table
                string query4 = "delete from dietplan where dietplanID = " + dietID;
                SqlCommand cmd4 = new SqlCommand(query4, conn);
                cmd4.ExecuteNonQuery();


                cmd3.Dispose();
                cmd4.Dispose();
                conn.Close();
            }
            comboBox1.SelectedIndex = -1;
            diet_plan_Load();
        }
    }
}
