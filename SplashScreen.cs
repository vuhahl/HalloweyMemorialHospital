using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloweyMemorialHospital
{
    public partial class SplashScreen : Form

    {

        private int progressCount = 0;
        private const int MaxProgressCount = 3; // Number of times progress bar should run

        public SplashScreen()
        {
            InitializeComponent();

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            timer1.Start();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            // Check if the progress bar has completed running twice
            if (++progressCount == MaxProgressCount)
            {
                // Stop the timer
                timer1.Stop();

                // Open the Select Patient form
                SelectPatient selectPatientForm = new SelectPatient();
                selectPatientForm.Show();

                // Close the splash screen
                this.Close();
            }
        }
    }
}
