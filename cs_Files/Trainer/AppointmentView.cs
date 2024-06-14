using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project.Trainer
{
    public partial class AppointmentView : Form
    {
        public AppointmentView()
        {
            InitializeComponent();
        }

        private void trainer_3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentManagement appointmentBooking = new AppointmentManagement();
            appointmentBooking.FormClosed += trainer_3_FormClosed;
            appointmentBooking.Show();  
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void AppointmentView_Load(object sender, EventArgs e)
        {

        }
    }
}
