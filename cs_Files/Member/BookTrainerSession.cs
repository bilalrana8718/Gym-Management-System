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

namespace DB_Project.Member
{
    public partial class BookTrainerSession : Form
    {
        string MID;
        string GID;
        public BookTrainerSession(string MemberID)
        {
            MID = MemberID;
            InitializeComponent();
        }

        private void BookTrainerSession_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT GymID, Gym_Name FROM Gym";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No gyms found.");
                        }
                        else
                        {
                            comboBox1.Items.Clear();

                            while (reader.Read())
                            {
                                int gymID = reader.GetInt32(0);
                                string gymName = reader.GetString(1);

                                comboBox3.Items.Add(gymName);
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                string selectedGymName = comboBox3.SelectedItem.ToString();
                string query1 = "Select GymID from Gym where Gym_Name = @SelectedGymName";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.Parameters.AddWithValue("@SelectedGymName", selectedGymName);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    int gymID = reader1.GetInt32(0);
                    GID = gymID.ToString();
                    string query = "Select trainerID, Name from Trainer where trainerID in (Select trainerID from WorksIn where GymID = @GymID)";
                    reader1.Close();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GymID", gymID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("No trainers found");
                            }
                            else
                            {
                                comboBox1.Items.Clear();
                                comboBox2.Items.Clear();

                                while (reader.Read())
                                {
                                    int TrainerID = reader.GetInt32(0);
                                    string TrainerName = reader.GetString(1);

                                    comboBox1.Items.Add(TrainerID);
                                    comboBox2.Items.Add(TrainerName);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No gym found with the selected name");
                }
            }

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime startTime = dateTimePicker1.Value;
            DateTime endTime = dateTimePicker2.Value;
            string trainerID = comboBox1.Text;
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();
                string queryMaxID = "SELECT MAX(AppointmentID) FROM Apppointment"; // Corrected spelling here
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                int maxID = 0; // Initializing maxID to 0
                using (SqlDataReader reader1 = cmdMaxID.ExecuteReader())
                {
                    if (reader1.Read() && !reader1.IsDBNull(0)) // Checking if there is a value and it's not DBNull
                    {
                        maxID = reader1.GetInt32(0); // Reading the value from the reader
                    }
                }
                int newID = maxID + 1;
                // Use parameterized query to avoid SQL injection and improve code readability
                string query = "INSERT INTO Apppointment VALUES (@NewID, @MID, @TrainerID, @GID, @StartTime, @EndTime)";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Add parameters
                cmd.Parameters.AddWithValue("@NewID", newID);
                cmd.Parameters.AddWithValue("@MID", MID); // Assuming MID is declared elsewhere
                cmd.Parameters.AddWithValue("@TrainerID", trainerID);
                cmd.Parameters.AddWithValue("@GID", GID); // Assuming GID is declared elsewhere
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);
                cmd.ExecuteNonQuery();
                cmdMaxID.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            this.Close();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
