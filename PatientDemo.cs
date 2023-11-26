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

namespace HalloweyMemorialHospital
{
    public partial class PatientDemo : Form
    {

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

        private void PopulatePatientData(int patientID)
        {
            using (MySqlConnection conn = DBInteraction.MakeConnnection())
            {// POPULATE TEXTBOXES WITH THE SELECTED PATIENT INFO
                try
                {
                    DataTable patientData = DBInteraction.GetPatientDataById(conn, patientID);
                    dataGridView1.DataSource = patientData;

                    if (patientData.Rows.Count > 0)
                    {
                        DataRow patientRow = patientData.Rows[0];

                        // Populate textboxes with patient data
                        txtHospitalMR.Text = patientRow["HospitalMRNum"].ToString();
                        txtLastName.Text = patientRow["PtLastName"].ToString();
                        txtFirstName.Text = patientRow["PtFirstName"].ToString();
                        txtMiddeName.Text = patientRow["PtMiddleInitial"].ToString();
                        txtSuffix.Text = patientRow["Suffix"].ToString();
                        txtHomeAdd.Text = patientRow["HomeAddress"].ToString();
                        txtHomeCity.Text = patientRow["HomeCity"].ToString();
                        txtState.Text = patientRow["HomeState/Province/Region"].ToString();
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


                        // Update the mode based on the "IsDeleted" status
                        isOnMode = (Convert.ToInt32(patientRow["IsDeleted"]) == 0);

                        SetControlstoReadOnly(!readOnly);
                    }
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
            txtHCPID.ReadOnly = readOnly;
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

            // Show the panel with text boxes
            panel6.BackColor = Color.FloralWhite;
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
            //SAVE AND INSERT BUTTON
            if (isOnMode) // Check if in Add/Modify mode
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
                            //mandatory fields
                            HospitalMR = txtHospitalMR.Text,
                            LastName = txtLastName.Text,
                            FirstName = txtFirstName.Text,
                            HomePhone = txtHomePhone.Text,
                            EmergencyPhoneNumber = txtEmergencyPhone.Text,
                            Email = txtEmail.Text,
                            DOB = txtDOB.Text,
                            DateEntered = txtDateEntered.Text,
                        };

                        try
                        {
                            
                            if (isAddMode) // Check if in Add mode
                            {
                                // Insert the patient record
                                int recsInserted = DBInteraction.InsertPatientSP(conn, p);
                                MessageBox.Show($"Inserted {recsInserted} records into PatientDemo!");
                            }
                            else // Modify mode
                            {
                                // Update the patient record
                                int recsUpdated = DBInteraction.UpdatePatientRecord(conn, p);
                                MessageBox.Show($"Updated {recsUpdated} records in PatientDemo!");
                            }
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
            if(!string.IsNullOrEmpty(txtLNameLookup.Text.Trim()) || !string.IsNullOrEmpty(txtIDLookup.Text.Trim()))
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
            // Populate textboxes with patient data
            txtHospitalMR.Text = patientRow["HospitalMRNum"].ToString();
            txtLastName.Text = patientRow["PtLastName"].ToString();
            txtFirstName.Text = patientRow["PtFirstName"].ToString();
            txtMiddeName.Text = patientRow["PtMiddleInitial"].ToString();
            txtSuffix.Text = patientRow["Suffix"].ToString();
            txtHomeAdd.Text = patientRow["HomeAddress"].ToString();
            txtHomeCity.Text = patientRow["HomeCity"].ToString();
            txtState.Text = patientRow["HomeState/Province/Region"].ToString();
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
            txtHCPID.Clear();
            txtDateEntered.Clear();
            txtNextofKin.Clear();
            txtNOKRelation.Clear();
        }



        private void txtIDLookup_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
