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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Project.Member
{
    public partial class DietPlan : Form
    {
        string MID;
        public DietPlan(string mID)
        {
            InitializeComponent();
            MID = mID;
        }
        private void Dietplan_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string DietPlan;
            if (comboBox1.SelectedItem != null)
            {
                DietPlan = ((dynamic)comboBox1.SelectedItem);

                SqlConnectionManager connectionManager = new SqlConnectionManager();
                DataTable dataTable = new DataTable();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT dp.DietPlanID, dp.DietPlan_Name, dp.Type_of_Diet, dp.Purpose, dm.Time_, m.name AS MealName, m.carbs, m.protein, m.fats, m.fibre FROM DietPlan dp JOIN DietPlanMeals dm ON dp.DietPlanID = dm.DietPlanID JOIN Meal m ON dm.MealID = m.MealID where DietPlan_Name = '" + DietPlan + "';";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    adapter.Fill(dataTable);
                }
                dataGridView1.DataSource = dataTable;

            }
        }

        private void DietPlan_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT DietPlan_Name FROM DietPlan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No Diet plan found.");
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

        private void button3_Click(object sender, EventArgs e)
        {
            int dietplanID = 0;
            string dietPlanName = comboBox1.Text;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryTrainerID = "SELECT dietPlanID from DietPlan where Dietplan_name = '" + dietPlanName + "'";
                SqlCommand cmdMaxID = new SqlCommand(queryTrainerID, conn);
                object result = cmdMaxID.ExecuteScalar();

                dietplanID = 1; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    dietplanID = Convert.ToInt32(result);
                }


                // Inserting the new record
                string query = "INSERT INTO Select_DietPlan VALUES (@MemberID, @DietPlanID) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MemberID", Int32.Parse(MID));
                cmd.Parameters.AddWithValue("@DietPlanID", dietplanID);

                cmd.ExecuteNonQuery();

                cmdMaxID.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            this.Close();
        }
    }
}
