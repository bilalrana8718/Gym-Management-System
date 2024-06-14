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

    public partial class ExerciseCreation : Form
    {
        string MachineSelected;
        public ExerciseCreation()
        {
            InitializeComponent();
            ExerciseCreation_Load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ExerciseCreation_Load()
        {

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT name FROM Machines";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No Machines found.");
                        }
                        else
                        {
                            comboBox2.Items.Clear();

                            while (reader.Read())
                            {
                                string machineName = reader.GetString(0);

                                comboBox2.Items.Add(machineName);
                            }
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                MachineSelected = ((dynamic)comboBox2.SelectedItem);
            }
        }

        private void ExerciseCreation_Load(object sender, EventArgs e)
        {

        }

        private void ExerciseCreation_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string e_name = textBox1.Text;
            string machine = comboBox2.Text;
            string target_muscle = comboBox1.Text;
            int Rest_interval = (int)numericUpDown1.Value;
            int experience_req = (int)numericUpDown2.Value;

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                // Extracting the max MemberID and adding 1 to i
                string queryMaxID = "SELECT MAX(ExerciseID) FROM Exercise";
                SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                object result = cmdMaxID.ExecuteScalar();

                int maxID = 0; // Default value in case no records are found
                if (result != DBNull.Value && result != null)
                {
                    maxID = Convert.ToInt32(result);
                }

                int newID = maxID + 1;

                // Inserting the new record
                string query = "INSERT INTO Exercise VALUES (@ExerciseID, @Target_Muscle, @Machine, @Rest_Intervals, @Experience_Required) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ExerciseID", newID);
                cmd.Parameters.AddWithValue("@Target_Muscle", target_muscle);
                cmd.Parameters.AddWithValue("@Machine", machine);
                cmd.Parameters.AddWithValue("@Rest_Intervals", Rest_interval);
                cmd.Parameters.AddWithValue("@Experience_Required", experience_req);

                cmd.ExecuteNonQuery();

                cmdMaxID.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }
    }
}
