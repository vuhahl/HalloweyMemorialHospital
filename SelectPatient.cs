using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HalloweyMemorialHospital
{
    public partial class SelectPatient : Form
    {

        MySqlConnection conn;
        DataTable dt;
        private int selectedPatientID;

        public SelectPatient()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPopulatePatient_Click(object sender, EventArgs e)
        {
            using (conn = DBInteraction.MakeConnnection())
            {
                try
                {
                    dt = DBInteraction.GetAllPatients(conn);
                    dataGridView1.DataSource = dt;
                    this.dataGridView1.Columns["PatientID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Patient Retrieval Error!  Error=" + ex.Message);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming the PatientID is in the first column (index 0)
                int selectedPatientID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PatientID"].Value);

                // Open the Patient Demographics form with the selected PatientID
                PatientDemo patientDemoForm = new PatientDemo(selectedPatientID);
                patientDemoForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a patient before clicking 'Select Patient'.");
            }
        }

        private void btnPatientDemo_Click(object sender, EventArgs e)
        {
            // opening PatientDemographicsForm class
            PatientDemo patientDemographicsForm = new PatientDemo(selectedPatientID);
            patientDemographicsForm.Show();
        }

        private void btnAllergy_Click(object sender, EventArgs e)
        {
            // Assuming you have an AllergyForm class
            Allergy allergyForm = new Allergy();
            allergyForm.Show();

            
        }

        private void btnMedications_Click(object sender, EventArgs e)
        {
            // Assuming you have a MedicationsForm class
            Medications medicationsForm = new Medications();
            medicationsForm.Show();

            
        }

    }
}
