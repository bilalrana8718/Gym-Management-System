using DB_Project.Member;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Project.Trainer
{
    public partial class DietPlanCreation : Form
    {
        int TrainerID;
        public DietPlanCreation(int TID = 0)
        {
            InitializeComponent();
            TrainerID = TID;
            this.DietPlanCreation_Load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DietPlanCreation_Load()
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT name from Meal";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No meals found.");
                        }
                        else
                        {
                            comboBox1.Items.Clear();
                            comboBox2.Items.Clear();
                            comboBox3.Items.Clear();
                            comboBox4.Items.Clear();

                            while (reader.Read())
                            {
                                string mealName = reader.GetString(0);

                                comboBox1.Items.Add(mealName);
                                comboBox2.Items.Add(mealName);
                                comboBox3.Items.Add(mealName);
                                comboBox4.Items.Add(mealName);

                            }
                        }
                    }
                }
            }
        }


        private void insertIntoDietPlanMeals(int dietID, string mealTime, System.Windows.Forms.ComboBox meal)
        {
            if (meal.Text == "")
                return;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the id of meal
                string queryMaxID = "SELECT MealID FROM Meal where name = '" + meal.Text + "'";
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                object result = cmdMaxID.ExecuteScalar();

                if (result == DBNull.Value || result == null)
                    return;

                int mealId = Convert.ToInt32(result);
                // Inserting the new record
                string query = "INSERT INTO DietPlanMeals VALUES (@DietPlanID, @MealID, @Time_) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DietPlanID", dietID);
                cmd.Parameters.AddWithValue("@MealID", mealId);
                cmd.Parameters.AddWithValue("@Time_", mealTime);


                cmd.ExecuteNonQuery();
                cmdMaxID.Dispose();

                cmd.Dispose();
                conn.Close();
            }

        }
 
        private void DietPlanCreation_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string d_name = textBox2.Text;
            string purpose = comboBox5.Text;
            string type_of_diet = comboBox6.Text;
            int DietPlanID = 0;


            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryMaxID = "SELECT MAX(DietPlanID) FROM DietPlan";
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                object result = cmdMaxID.ExecuteScalar();

                int maxID = 0; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    maxID = Convert.ToInt32(result);
                }

                DietPlanID = maxID + 1;

                // Inserting the new record
                string query = "INSERT INTO DietPlan VALUES (@DietPlanID, @dietPlanName, @Type_of_Diet, @purpose, @TrainerID) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DietPlanID", DietPlanID);
                cmd.Parameters.AddWithValue("@dietPlanName", d_name);
                cmd.Parameters.AddWithValue("@Type_of_Diet", type_of_diet);
                cmd.Parameters.AddWithValue("@purpose", purpose);
                cmd.Parameters.AddWithValue("@TrainerID", TrainerID);

                cmd.ExecuteNonQuery();

                cmdMaxID.Dispose();
                cmd.Dispose();
                conn.Close();
            }

            insertIntoDietPlanMeals(DietPlanID, "Breakfast", comboBox1);
            insertIntoDietPlanMeals(DietPlanID, "Lunch", comboBox2);
            insertIntoDietPlanMeals(DietPlanID, "Dinner", comboBox3);
            insertIntoDietPlanMeals(DietPlanID, "Snack", comboBox4);

            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }
    }
}
