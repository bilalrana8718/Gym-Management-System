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
    public partial class MealCreation : Form
    {
        public MealCreation()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MealCreation_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string m_name = textBox1.Text;
            int portionSize = (int)numericUpDown1.Value;
            int fats = (int)numericUpDown3.Value;
            int fibre = (int)numericUpDown2.Value;
            int protein = (int)numericUpDown5.Value;
            int carbs = (int)numericUpDown4.Value;



            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryMaxID = "SELECT MAX(MealID) FROM Meal";
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                object result = cmdMaxID.ExecuteScalar();

                int maxID = 0; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    maxID = Convert.ToInt32(result);
                }

                int newID = maxID + 1;

                // Inserting the new record
                string query = "INSERT INTO Meal VALUES (@MealID, @name, @carbs, @protein, @fats, @fibre, @portionSize) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MealID", newID);
                cmd.Parameters.AddWithValue("@name", m_name);
                cmd.Parameters.AddWithValue("@carbs", carbs);
                cmd.Parameters.AddWithValue("@protein", protein);
                cmd.Parameters.AddWithValue("@fats", fats);
                cmd.Parameters.AddWithValue("@fibre", fibre);
                cmd.Parameters.AddWithValue("@portionSize", portionSize);

                cmd.ExecuteNonQuery();

                cmdMaxID.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            this.Close();
        }
    }
}
