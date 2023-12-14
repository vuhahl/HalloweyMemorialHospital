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
        
        public SplashScreen()
        {
            InitializeComponent();
            

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            
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

            
            }

        private void button1_Click(object sender, EventArgs e)
        {
            // opening PatientDemographicsForm class
            SelectPatient selectPatient = new SelectPatient();
            selectPatient.Show();

           
        }

       
    }
    
}
