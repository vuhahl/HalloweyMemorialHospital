using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using HollowellMemorialHospital;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySqlX.XDevAPI.Common;

namespace HalloweyMemorialHospital
{
    public partial class PatientDemo : Form
    {
        private Dictionary<string, string> originalValues = new Dictionary<string, string>();

        private int selectedPatientID;
        public bool isOnMode = false; // Track whether the form is in add or modify mode
        public bool isAddMode = false;
        private bool readOnly;

        public PatientDemo(int PatientID)
        {
            InitializeComponent();
            selectedPatientID = PatientID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LOADING DATAGRIDVIEW WITH SELECTED PATIENT
            PopulatePatientData(selectedPatientID);
            dataGridView1.Columns["PatientID"].Visible = false;
            dataGridView1.Columns["IsDeleted"].Visible = false;
            SetControlstoReadOnly(true);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void PopulatePatientData(int patientID)
        {
            using (MySqlConnection conn = DBInteraction.MakeConnnection())
            {
                try
                {
                    DataTable patientData = DBInteraction.GetPatientDataById(conn, patientID);
                    // Now bind the patientData to the DataGridView
                    dataGridView1.DataSource = patientData;

                    if (patientData.Rows.Count > 0)
                    {
                        DataRow patientRow = patientData.Rows[0];

                        // Populate textboxes with patient data
                        PopulateTextboxes(patientRow);
                        // Store original values after populating textboxes
                        StoreOriginalValues();

                        // Update the mode based on the "IsDeleted" status
                        isOnMode = (Convert.ToInt32(patientRow["IsDeleted"]) == 0);

                        SetControlstoReadOnly(!readOnly);
                    }
                    else
                    {
                        // Clear textboxes if no matching patient found
                        ClearTextboxes();
                    }

                    // Now bind the patientData to the DataGridView
                    dataGridView1.DataSource = patientData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving patient data: {ex.Message}");
                }
            }
        }



        private void SetControlstoReadOnly(bool readOnly)
        {
            // set the read-only status

            txtHospitalMR.ReadOnly = readOnly;
            txtLastName.ReadOnly = readOnly;
            txtFirstName.ReadOnly = readOnly;
            txtMiddeName.ReadOnly = readOnly;
            txtSuffix.ReadOnly = readOnly;
            txtHomeAdd.ReadOnly = readOnly;
            txtHomeCity.ReadOnly = readOnly;
            txtState.ReadOnly = readOnly;
            txtHomeZip.ReadOnly = readOnly;
            txtCountry.ReadOnly = readOnly;
            txtCitizen.ReadOnly = readOnly;
            txtHomePhone.ReadOnly = readOnly;
            txtEmergencyPhone.ReadOnly = readOnly;
            txtEmail.ReadOnly = readOnly;
            txtSSN.ReadOnly = readOnly;
            txtDOB.ReadOnly = readOnly;
            txtGender.ReadOnly = readOnly;
            txtEthnicAssoc.ReadOnly = readOnly;
            txtReligion.ReadOnly = readOnly;
            txtMaritalStat.ReadOnly = readOnly;
            txtEmploymentStat.ReadOnly = readOnly;
            txtDateofExpire.ReadOnly = readOnly;
            txtReferral.ReadOnly = readOnly;
            txtHCPID.ReadOnly = readOnly;
            txtComments.ReadOnly = readOnly;
            txtDateEntered.ReadOnly = readOnly;
            txtNextofKin.ReadOnly = readOnly;
            txtNOKRelation.ReadOnly = readOnly;
        }




        private void btnAddMode_Click(object sender, EventArgs e)
        {
            // Set the form to "Add" mode
            isOnMode = true;
            isAddMode = true;

            panel6.BackColor = Color.FloralWhite;


            btnSaveInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;

            ClearTextboxes();
            SetControlstoReadOnly(false);
        }

        private void btnModifyMode_Click(object sender, EventArgs e)
        {
            // Set the form to "Modify" mode
            isOnMode = true;
            isAddMode = false;
            panel6.BackColor = Color.FloralWhite;

            btnSaveInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;

            SetControlstoReadOnly(false);

        }

        private void btnDeleteMode_Click(object sender, EventArgs e)
        {
            // Set the form to "Delete" mode
            isOnMode = true;
            panel6.BackColor = Color.FloralWhite;

            btnSaveInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;


            // Disable textboxes for delete mode
            SetControlstoReadOnly(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtHosptalMR_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMiddeName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //EXIT BUTTON ON FOOTER
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //save and insert
            if (isOnMode) // Check if in Add/Modify mode
            {
                SaveOrUpdatePatient();
            }
        }


        private void btnExitMode_Click(object sender, EventArgs e)
        {
            isOnMode = false;
            panel6.BackColor = Color.LightGray;
            SetControlstoReadOnly(true);
            btnSaveInsert.Visible = false;
            btnUndo.Visible = false;
            btnExitMode.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

                    if (patientData.Rows.Count > 0)
                    {
                        // Display the first patient's information in the text boxes
                        PopulateTextboxes(patientData.Rows[0]);
                    }
                    else
                    {
                        // Clear textboxes if no matching patient found
                        ClearTextboxes();
                    }
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

                    if (patientData.Rows.Count > 0)
                    {
                        // Display the first patient's information in the text boxes
                        PopulateTextboxes(patientData.Rows[0]);
                    }
                    else
                    {
                        // Clear textboxes if no matching patient found
                        ClearTextboxes();
                    }
                }
            }
        }

        private void PopulateTextboxes(DataRow patientRow)
        {
            // Populate textboxes with patient datajn  
            txtPID.Text = patientRow["PatientID"].ToString();
            txtHospitalMR.Text = patientRow["HospitalMRNum"].ToString();
            txtLastName.Text = patientRow["PtLastName"].ToString();
            txtFirstName.Text = patientRow["PtFirstName"].ToString();
            txtMiddeName.Text = patientRow["PtMiddleInitial"].ToString();
            txtSuffix.Text = patientRow["Suffix"].ToString();
            txtHomeAdd.Text = patientRow["HomeAddress"].ToString();
            txtHomeCity.Text = patientRow["HomeCity"].ToString();
            txtState.Text = patientRow["HomeStateProvinceRegion"].ToString();
            txtHomeZip.Text = patientRow["HomeZip"].ToString();
            txtCountry.Text = patientRow["Country"].ToString();
            txtCitizen.Text = patientRow["Citizenship"].ToString();
            txtHomePhone.Text = patientRow["PtHomePhone"].ToString();
            txtEmergencyPhone.Text = patientRow["EmergencyPhoneNumber"].ToString();
            txtEmail.Text = patientRow["EmailAddress"].ToString();
            txtSSN.Text = patientRow["SSN"].ToString();
            txtDOB.Text = patientRow["DOB"].ToString();
            txtGender.Text = patientRow["Gender"].ToString();
            txtEthnicAssoc.Text = patientRow["EthnicAssociation"].ToString();
            txtReligion.Text = patientRow["Religion"].ToString();
            txtMaritalStat.Text = patientRow["MaritalStatus"].ToString();
            txtEmploymentStat.Text = patientRow["EmploymentStatus"].ToString();
            txtDateofExpire.Text = patientRow["DateofExpire"].ToString();
            txtReferral.Text = patientRow["Referral"].ToString();
            txtHCPID.Text = patientRow["CurrentPrimaryHCPId"].ToString();
            txtComments.Text = patientRow["Comments"].ToString();
            txtHCPID.Text = patientRow["CurrentPrimaryHCPId"].ToString();
            txtDateEntered.Text = patientRow["DateEntered"].ToString();
            txtNextofKin.Text = patientRow["NextOfKinId"].ToString();
            txtNOKRelation.Text = patientRow["NextOfKinRelationshipToPatient"].ToString();

        }

        private void ClearTextboxes()
        {
            // Populate textboxes with patient data
            txtPID.Clear();
            txtHospitalMR.Clear();
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddeName.Clear();
            txtSuffix.Clear();
            txtHomeAdd.Clear();
            txtHomeCity.Clear();
            txtState.Clear();
            txtHomeZip.Clear();
            txtCountry.Clear();
            txtCitizen.Clear();
            txtHomePhone.Clear();
            txtEmergencyPhone.Clear();
            txtEmail.Clear();
            txtSSN.Clear();
            txtDOB.Clear();
            txtGender.Clear();
            txtEthnicAssoc.Clear();
            txtReligion.Clear();
            txtMaritalStat.Clear();
            txtEmploymentStat.Clear();
            txtDateofExpire.Clear();
            txtReferral.Clear();
            txtHCPID.Clear();
            txtComments.Clear();
            txtDateEntered.Clear();
            txtNextofKin.Clear();
            txtNOKRelation.Clear();
        }



        private void txtIDLookup_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGoToAllergies_Click(object sender, EventArgs e)
        {
            //AllergyForm class
            if (selectedPatientID > 0)
            {
                // Open MedicationsForm and pass patientID
                Allergy allergyForm = new Allergy(selectedPatientID);
                allergyForm.PopulatePatientData();  // method to populate medication data
                allergyForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }
        }


        private void SaveOrUpdatePatient()
        {
            if (!string.IsNullOrEmpty(txtHospitalMR.Text) &&
                !string.IsNullOrEmpty(txtLastName.Text) &&
                !string.IsNullOrEmpty(txtFirstName.Text) &&
                !string.IsNullOrEmpty(txtHomePhone.Text) &&
                !string.IsNullOrEmpty(txtEmergencyPhone.Text) &&
                !string.IsNullOrEmpty(txtEmail.Text) &&
                !string.IsNullOrEmpty(txtDOB.Text))
            {
                using (MySqlConnection conn = DBInteraction.MakeConnnection())
                {
                    Patient p = new Patient()
                    {
                        HospitalMR = txtHospitalMR.Text,
                        LastName = txtLastName.Text,
                        FirstName = txtFirstName.Text,
                        MiddleInitial = txtMiddeName.Text,
                        Suffix = txtSuffix.Text,
                        HomeAddress = txtHomeAdd.Text,
                        HomeCity = txtHomeCity.Text,
                        StateProvinceRegion = txtState.Text,
                        HomeZip = txtHomeZip.Text,
                        Country = txtCountry.Text,
                        Citizenship = txtCitizen.Text,
                        HomePhone = txtHomePhone.Text,
                        EmergencyPhoneNumber = txtEmergencyPhone.Text,
                        Email = txtEmail.Text,
                        SSN = txtSSN.Text,
                        DOB = txtDOB.Text,
                        Gender = txtGender.Text,
                        EthnicAssociation = txtEthnicAssoc.Text,
                        Religion = txtReligion.Text,
                        MaritalStatus = txtMaritalStat.Text,
                        EmploymentStatus = txtEmploymentStat.Text,
                        DateofExpire = txtDateofExpire.Text,
                        Referral = txtReferral.Text,
                        CurrentPrimaryHCPId = txtHCPID.Text,
                        Comments = txtComments.Text,
                        DateEntered = txtDateEntered.Text,
                        NextOfKinId = txtNextofKin.Text,
                        NextOfKinRelationship = txtNOKRelation.Text

                    };

                    try
                    {
                        if (isAddMode)
                        {
                            // Insert the patient record
                            int recsInserted = DBInteraction.InsertPatientSP(conn, p);
                            MessageBox.Show($"Inserted {recsInserted} records into PatientDemo!");
                        }
                        else
                        {
                            // Update the patient record
                            int recsUpdated = DBInteraction.UpdatePatientSP(conn, p);
                            MessageBox.Show($"Updated {recsUpdated} records in PatientDemo!");
                        }

                        // Refresh the DataGridView after the update
                        PopulatePatientData(selectedPatientID);

                        
                       
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error! {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter required information.");
            }
        }

        private void btnGoToMeds_Click(object sender, EventArgs e)
        {
            if (selectedPatientID > 0)
            {
                // Open MedicationsForm and pass patientID
                Medications medsForm = new Medications(selectedPatientID);
                medsForm.PopulatePatientData();  // method to populate medication data
                medsForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }
        }

        private void btnDeleteMode_Click_1(object sender, EventArgs e)
        {
            // Set the form to "Delete" mode
            isOnMode = true;
            panel4.BackColor = Color.FloralWhite;

            btnSaveInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;


            // Disable textboxes for delete mode
            SetControlstoReadOnly(false);

            using (MySqlConnection conn = DBInteraction.MakeConnnection())
            {
                try
                {
                    // Assuming you have a DataGridView named dataGridViewPatients
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        // Get the selected row
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                        // Retrieve the PatientID from the selected row's data
                        if (selectedRow.Cells["PatientID"].Value != null)
                        {
                            // Prompt the user for confirmation
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this patient record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // Soft delete the patient record directly using PatientID
                                int PID = (int)selectedRow.Cells["PatientID"].Value;
                                int recsUpdated = DBInteraction.SoftDeletePatient(conn, PID);
                                MessageBox.Show($"Soft deleted {recsUpdated} patient records!");

                                // Refresh the DataGridView after the update
                                PopulatePatientData(PID);
                                dataGridView1.Columns["IsDeleted"].Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to retrieve PatientID for the selected record.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a patient record to delete.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error! {ex.Message}");
                }

            }
        }



        private void btnUndo_Click(object sender, EventArgs e)
        {   
            UndoChanges();
        }

        private void StoreOriginalValues()
        {
            originalValues.Clear();  // Clear previous values

            // Store original values for each textbox using the Name property as the key
            originalValues.Add(txtPID.Name, txtPID.Text);
            originalValues.Add(txtHospitalMR.Name, txtHospitalMR.Text);
            originalValues.Add(txtLastName.Name, txtLastName.Text);
            originalValues.Add(txtFirstName.Name, txtFirstName.Text);
            originalValues.Add(txtMiddeName.Name, txtMiddeName.Text);
            originalValues.Add(txtSuffix.Name, txtSuffix.Text);
            originalValues.Add(txtHomeAdd.Name, txtHomeAdd.Text);
            originalValues.Add(txtHomeCity.Name, txtHomeCity.Text);
            originalValues.Add(txtState.Name, txtState.Text);
            originalValues.Add(txtHomeZip.Name, txtHomeZip.Text);
            originalValues.Add(txtCountry.Name, txtCountry.Text);
            originalValues.Add(txtCitizen.Name, txtCitizen.Text);
            originalValues.Add(txtHomePhone.Name, txtHomePhone.Text);
            originalValues.Add(txtEmergencyPhone.Name, txtEmergencyPhone.Text);
            originalValues.Add(txtEmail.Name, txtEmail.Text);
            originalValues.Add(txtSSN.Name, txtSSN.Text);
            originalValues.Add(txtDOB.Name, txtDOB.Text);
            originalValues.Add(txtGender.Name, txtGender.Text);
            originalValues.Add(txtEthnicAssoc.Name, txtEthnicAssoc.Text);
            originalValues.Add(txtReligion.Name, txtReligion.Text);
            originalValues.Add(txtMaritalStat.Name, txtMaritalStat.Text);
            originalValues.Add(txtEmploymentStat.Name, txtEmploymentStat.Text);
            originalValues.Add(txtDateofExpire.Name, txtDateofExpire.Text);
            originalValues.Add(txtReferral.Name, txtReferral.Text);
            originalValues.Add(txtHCPID.Name, txtHCPID.Text);
            originalValues.Add(txtComments.Name, txtComments.Text);
            originalValues.Add(txtDateEntered.Name, txtDateEntered.Text);
            originalValues.Add(txtNextofKin.Name, txtNextofKin.Text);
            originalValues.Add(txtNOKRelation.Name, txtNOKRelation.Text);
        }

        // Call this method to revert changes when the "Undo" button is clicked
        private void UndoChanges()
        {
            // Prompt the user for confirmation
            DialogResult result = MessageBox.Show("Do you want to undo the entire form?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (var entry in originalValues)
                {
                    string controlName = entry.Key;
                    string originalText = entry.Value;

                    // Find the TextBox control by name and update its text
                    Control[] foundControls = this.Controls.Find(controlName, true);
                    if (foundControls.Length > 0 && foundControls[0] is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)foundControls[0]).Text = originalText;
                    }
                }

                // Set controls back to read-only state
                SetControlstoReadOnly(true);
            }
        }
    }
}


