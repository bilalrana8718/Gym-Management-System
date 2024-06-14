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
    public partial class AppointmentBooking : Form
    {
        int trainerId;
        public AppointmentBooking(int trainerId)
        {
            InitializeComponent();
            this.trainerId = trainerId;
        }
        private void trainer4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentManagement appointmentManagement = new AppointmentManagement();
            appointmentManagement.FormClosed += trainer4_FormClosed;
            appointmentManagement.Show();   
        }

        private void AppointmentBooking_Load(object sender, EventArgs e)
        {
            SqlConnectionManager connectionManager = new SqlConnectionManager();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = connectionManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT * from apppointment where trainerId = " + trainerId;
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                // Fill the DataTable with the data from the query
                adapter.Fill(dataTable);
            }

            dataGridView1.DataSource = dataTable;
        }
    }
}
