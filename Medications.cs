using HollowellMemorialHospital;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloweyMemorialHospital
{
    public partial class Medications : Form
    {
        private Dictionary<string, string> originalValues = new Dictionary<string, string>();

        private int patientID;
        public bool isOnMode = false; // Track whether the form is in add or modify mode
        public bool isAddMode = false;
        private bool readOnly;

        public Medications(int patientID)
        {
            InitializeComponent();
            this.patientID = patientID;
        }

        private void Medications_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetControlstoReadOnly(bool readOnly)
        {
            // set the read-only status

            txtMedID.ReadOnly = readOnly;
            txtMedications.ReadOnly = readOnly;
            txtMedAMT.ReadOnly = readOnly;
            txtMedUnit.ReadOnly = readOnly;
            txtInstructions.ReadOnly = readOnly;
            txtMedStart.ReadOnly = readOnly;
            txtMedEnd.ReadOnly = readOnly;
            txtPrescription.ReadOnly = readOnly;
        }

        public void PopulatePatientData()
        {
            using (MySqlConnection conn = DBInteraction.MakeConnnection())
            {
                try
                {
                    DataTable medicationData = DBInteraction.GetMedicationDataById(conn, patientID);
                    dataGridView1.DataSource = medicationData;
                    

                    if (medicationData.Rows.Count > 0)
                    {
                        DataRow medicationRow = medicationData.Rows[0];

                        // Populate textboxes with patient data
                        PopulateTextboxes(medicationRow);

                        // Update the mode based on the "IsDeleted" status
                        isOnMode = (Convert.ToInt32(medicationRow["IsDeleted"]) == 0);

                        SetControlstoReadOnly(!readOnly);
                        dataGridView1.Columns["PatientID"].Visible = true;
                        dataGridView1.Columns["IsDeleted"].Visible = false;
                    }
                    else
                    {
                        // Clear textboxes if no matching patient found
                        ClearTextboxes();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving patient data: {ex.Message}");
                }
            }
        }

        private void PopulateTextboxes(DataRow medicationRow)
        {
            // Populate textboxes with patient data
            txtMedID.Text = medicationRow["MedicationID"].ToString();
            txtMedID.Text = medicationRow["PatientID"].ToString();
            txtMedications.Text = medicationRow["Medication"].ToString();
            txtMedAMT.Text = medicationRow["MedicationAMT"].ToString();
            txtMedUnit.Text = medicationRow["MedicationUnit"].ToString();
            txtInstructions.Text = medicationRow["Instructions"].ToString();
            txtMedStart.Text = medicationRow["MedicationStartDate"].ToString();
            txtMedEnd.Text = medicationRow["MedicationEndDate"].ToString();
            txtPrescription.Text = medicationRow["PrescriptionHCP"].ToString();

        }

        private void ClearTextboxes()
        {
            txtMedID.Clear();
            txtPID.Clear();
            txtMedications.Clear();
            txtMedAMT.Clear();
            txtMedUnit.Clear();
            txtInstructions.Clear();
            txtMedStart.Clear();
            txtMedEnd.Clear();
            txtPrescription.Clear();
        }

        private void btnAddMode_Click(object sender, EventArgs e)
        {
            // Set the form to "Add" mode
            isOnMode = true;
            isAddMode = true;

            panel4.BackColor = Color.FloralWhite;


            btnSaveandInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;


            SetControlstoReadOnly(false);
        }

        private void btnModifyMode_Click(object sender, EventArgs e)
        {
            // Set the form to "Modify" mode
            isOnMode = true;
            isAddMode = false;
            panel4.BackColor = Color.FloralWhite;

            btnSaveandInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;

            SetControlstoReadOnly(false);
        }

        private void btnDeleteMode_Click(object sender, EventArgs e)
        {
            // Set the form to "Delete" mode
            isOnMode = true;
            panel4.BackColor = Color.FloralWhite;

            btnSaveandInsert.Visible = true;
            btnUndo.Visible = true;
            btnExitMode.Visible = true;


            // Disable textboxes for delete mode
            SetControlstoReadOnly(false);

            using (MySqlConnection conn = DBInteraction.MakeConnnection())
            {
                try
                {
                    // Assuming you have a DataGridView named dataGridView1
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        // Get the selected row
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                        // Retrieve the MedicationID from the selected row's data
                        if (selectedRow.Cells["MedicationID"].Value != null)
                        {
                            // Prompt the user for confirmation
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // Soft delete the medication record directly using MedID
                                int medID = (int)selectedRow.Cells["MedicationID"].Value;
                                int recsUpdated = DBInteraction.SoftDeleteMedication(conn, medID);
                                MessageBox.Show($"Soft deleted {recsUpdated} records in Medication!");

                                // Refresh the DataGridView after the update
                                PopulatePatientData();
                                dataGridView1.Columns["PatientID"].Visible = true;
                                dataGridView1.Columns["IsDeleted"].Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to retrieve MedicationID for the selected record.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a record to delete.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error! {ex.Message}");
                }

            }
        }

            private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            UndoChanges();
        }

        private void btnExitMode_Click(object sender, EventArgs e)
        {
            isOnMode = false;
            panel4.BackColor = Color.LightGray;
            SetControlstoReadOnly(true);
            btnSaveandInsert.Visible = false;
            btnUndo.Visible = false;
            btnExitMode.Visible = false;
        }

        private void btnSaveandInsert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPID.Text) &&
                !string.IsNullOrEmpty(txtMedications.Text) &&
                !string.IsNullOrEmpty(txtMedAMT.Text) &&
                !string.IsNullOrEmpty(txtMedUnit.Text) &&
                !string.IsNullOrEmpty(txtInstructions.Text) &&
                !string.IsNullOrEmpty(txtMedStart.Text) )

            {
                using (MySqlConnection conn = DBInteraction.MakeConnnection())
                {
                    Meds medication = new Meds()
                    {
                        MedID = txtMedID.Text,
                        PID = txtPID.Text,
                        MedName = txtMedications.Text,
                        MedAMT = txtMedAMT.Text,
                        MedUnit = txtMedUnit.Text,
                        Instructions = txtInstructions.Text,
                        MedStart = txtMedStart.Text,
                        MedEnd = txtMedEnd.Text, 
                        Prescription = txtPrescription.Text,
                    };

                    try
                    {
                        if (isAddMode)
                        {
                            // Insert the medication record
                            int recsInserted = DBInteraction.InsertMedicationSP(conn, medication);
                            MessageBox.Show($"Inserted {recsInserted} records into PatientMedications!");
                        }
                        else
                        {
                            // Update the medication record
                            int recsUpdated = DBInteraction.UpdateMedicationSP(conn, medication);
                            MessageBox.Show($"Updated {recsUpdated} records in PatientMedications!");
                        }

                        // Refresh the DataGridView after the update
                        PopulatePatientData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error! {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter required medication information.");
            }
        }

        private void StoreOriginalValues()
        {
            originalValues.Clear();  // Clear previous values

            // Store original values for each textbox using the Name property as the key
            originalValues.Add(txtMedID.Name, txtMedID.Text);
            originalValues.Add(txtMedications.Name, txtMedications.Text);
            originalValues.Add(txtMedAMT.Name, txtMedAMT.Text);
            originalValues.Add(txtMedUnit.Name, txtMedUnit.Text);
            originalValues.Add(txtInstructions.Name, txtInstructions.Text);
            originalValues.Add(txtMedStart.Name, txtMedStart.Text);
            originalValues.Add(txtMedEnd.Name, txtMedEnd.Text);
            originalValues.Add(txtPrescription.Name, txtPrescription.Text);
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
