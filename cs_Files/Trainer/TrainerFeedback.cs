using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Project.Trainer
{
    public partial class TrainerFeedback : Form
    {
        int trainerId;
        public TrainerFeedback(int trainerId = 1)
        {
            InitializeComponent();
            this.trainerId = trainerId;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TrainerFeedback_Load(object sender, EventArgs e)
        {

            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT * from Feedback where trainerId = "+ trainerId;
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                // Fill the DataTable with the data from the query
                adapter.Fill(dataTable);

                string query2 = "Select avg(rating) from Feedback where trainerId = " + trainerId;
                SqlCommand cmd = new SqlCommand(query2, conn);

                int avgRating = (Int32)(cmd.ExecuteScalar()); 
                textBox1.Text = avgRating.ToString();
            }

            dataGridView1.DataSource = dataTable;
                
                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
