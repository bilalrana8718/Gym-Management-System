using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Collections;

namespace DB_Project.Admin
{
    public partial class Reg_Req_Admin : Form
    {
        public Reg_Req_Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string gym_name = textBox5.Text;
            string ownerID = textBox6.Text;
            string t_member = textBox2.Text;
            string revenue = textBox1.Text;
            string a_member = textBox4.Text;
            string rating = textBox9.Text;
            string location = textBox3.Text;

            if (gym_name == "" || ownerID == "" || t_member == "" || a_member == "" || revenue == "" || rating == "" || location == "")
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                SqlConnectionManager connectionManager = new SqlConnectionManager();
                using (SqlConnection conn = connectionManager.GetConnection())
                {
                    conn.Open();

                    string queryMaxID = "SELECT MAX(PerformanceID) FROM Gym_Performance";
                    SqlCommand cmdMaxID = new SqlCommand(queryMaxID, conn);
                    int maxID = Convert.ToInt32(cmdMaxID.ExecuteScalar()) + 1;

                    int total = Convert.ToInt32(t_member);
                    int active = Convert.ToInt32(a_member);
                    int revenue_main = Convert.ToInt32(revenue);
                    int rating_main = Convert.ToInt32(rating);

                    string query_performance = "Insert into Gym_Performance (PerformanceID, TotalMembers, Active_Attendence, Revenue, Rating)" +
                        "Values (@maxID, @total, @active, @revenue_main, @rating_main)";

                    SqlCommand cmd = new SqlCommand(query_performance, conn);
                    cmd.Parameters.AddWithValue("@maxID", maxID);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@active", active);
                    cmd.Parameters.AddWithValue("@revenue_main", revenue_main);
                    cmd.Parameters.AddWithValue("@rating_main", rating_main);
                    cmd.ExecuteNonQuery();


                    string queryMaxID1 = "SELECT MAX(GymID) FROM Gym";
                    SqlCommand cmdGymID = new SqlCommand(queryMaxID1, conn);
                    int GymID = Convert.ToInt32(cmdGymID.ExecuteScalar()) + 1;

                    int ownerId = Convert.ToInt32(ownerID);

                    string query_gym = "Insert into Gym (GymID, Gym_Name, Location, PerformanceID, OwnerID)" +
                        "Values (@GymID, @gym_name, @location, @maxID, @ownerId)";
                    SqlCommand cmd1 = new SqlCommand(query_gym, conn);
                    cmd1.Parameters.AddWithValue("@GymID", GymID);
                    cmd1.Parameters.AddWithValue("@gym_name", gym_name);
                    cmd1.Parameters.AddWithValue("@location", location);
                    cmd1.Parameters.AddWithValue("@maxID", maxID);
                    cmd1.Parameters.AddWithValue("@ownerId", ownerId);
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();



                    MessageBox.Show("Gym successfully registered!");
                }
                this.Close();
            }
        }
    }
}
