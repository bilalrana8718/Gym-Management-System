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

namespace DB_Project.Member
{
    public partial class TrainerFeedback : Form
    {
        string MID;
        public TrainerFeedback(string M)
        {
            MID = M;
            InitializeComponent();
        }

        private void TrainerFeedback_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT TrainerID, Name FROM Trainer";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No trainer found.");
                        }
                        else
                        {
                            comboBox1.Items.Clear();

                            while (reader.Read())
                            {
                                int TrainerID = reader.GetInt32(0);
                                string TrainerName = reader.GetString(1);

                                comboBox1.Items.Add(TrainerName);
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int trainerId;
            int rating = (int)numericUpDown3.Value;
            string review = textBox6.Text;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                trainerId = 0; // Default value in case no records are found
                string queryTrainerID = "SELECT trainerId FROM Trainer WHERE name = @TrainerName";
                SqlCommand cmdTrainerID = new SqlCommand(queryTrainerID, conn);
                cmdTrainerID.Parameters.AddWithValue("@TrainerName", comboBox1.Text);
                object result = cmdTrainerID.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    trainerId = Convert.ToInt32(result);
                }

                // Inserting the new record
                string query = "INSERT INTO Feedback (MemberID, TrainerID, rating, review) VALUES (@MemberID, @TrainerId, @rating, @review)";
                SqlCommand cmdInsert = new SqlCommand(query, conn);
                cmdInsert.Parameters.AddWithValue("@MemberID", MID);
                cmdInsert.Parameters.AddWithValue("@TrainerId", trainerId);
                cmdInsert.Parameters.AddWithValue("@rating", rating);
                cmdInsert.Parameters.AddWithValue("@review", review);

                cmdInsert.ExecuteNonQuery();

                cmdTrainerID.Dispose();
                cmdInsert.Dispose();

                conn.Close();
            }
            this.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
