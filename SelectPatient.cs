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
            
            // Check if any row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the patientID from the selected row
                int patientID = Convert.ToInt32(selectedRow.Cells["PatientID"].Value);

                // Open MedicationsForm and pass patientID
                PatientDemo pdemoForm = new PatientDemo(patientID);
                pdemoForm.PopulatePatientData(selectedPatientID);  // method to populate patient data
                pdemoForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }

        }

        private void btnAllergy_Click(object sender, EventArgs e)
        {
            // AllergyForm 
            // Check if any row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the patientID from the selected row
                int patientID = Convert.ToInt32(selectedRow.Cells["PatientID"].Value);

                // Open MedicationsForm and pass patientID
                Allergy allergyForm = new Allergy(patientID);
                allergyForm.PopulatePatientData();  // method to populate patient data
                allergyForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }

        }

        private void btnMedications_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the patientID from the selected row
                int patientID = Convert.ToInt32(selectedRow.Cells["PatientID"].Value);

                // Open MedicationsForm and pass patientID
                Medications medsForm = new Medications(patientID);
                medsForm.PopulatePatientData();  // method to populate patient data
                medsForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }
        }


        private void btnSearchforPatient_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLNameLookup.Text.Trim()) || !string.IsNullOrEmpty(txtIDLookup.Text.Trim()))
            {
                using (MySqlConnection conn = DBInteraction.MakeConnnection())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtLNameLookup.Text.Trim())) // Search by last name
                        {
                            SearchByLastName(conn);
                        }
                        else if (int.TryParse(txtIDLookup.Text.Trim(), out int patientID)) // Search by patient ID
                        {
                            SearchByPatientID(conn, patientID);
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid last name or patient ID to search for.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error searching for patient: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a last name or patient ID to search for.");
            }
        }


        private void SearchByLastName(MySqlConnection conn)
        {
            string lastName = txtLNameLookup.Text.Trim();

            using (MySqlCommand cmd = new MySqlCommand("SearchPatientByLastName", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@lastNameParam", lastName);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable patientData = new DataTable();
                    adapter.Fill(patientData);

                    dataGridView1.DataSource = patientData;
                }

            }
        }

        private void SearchByPatientID(MySqlConnection conn, int patientID)
        {
            using (MySqlCommand cmd = new MySqlCommand("GetPatientByPatientID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@patientIDParam", patientID);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable patientData = new DataTable();
                    adapter.Fill(patientData);

                    dataGridView1.DataSource = patientData;

                }
            }
        }
    }
}
