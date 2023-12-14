using HollowellMemorialHospital;
using MySql.Data.MySqlClient;
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
    public partial class Allergy : Form
    {
        private Dictionary<string, string> originalValues = new Dictionary<string, string>();
        private int patientID;
        public bool isOnMode = false; // Track whether the form is in add or modify mode
        public bool isAddMode = false;
        private bool readOnly;

        public Allergy(int patientID)
        {
            InitializeComponent();
            this.patientID = patientID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
        private void SetControlstoReadOnly(bool readOnly)
        {
            // set the read-only status

            txtAllergyID.ReadOnly = readOnly;
            txtAllergen.ReadOnly = readOnly;
            txtAllergyStart.ReadOnly = readOnly;
            txtAllergyEnd.ReadOnly = readOnly;
            txtAllergyDescription.ReadOnly = readOnly;
            
        }

        public void PopulatePatientData()
        {
            using (MySqlConnection conn = DBInteraction.MakeConnnection())
            {
                try
                {
                    DataTable allergyData = DBInteraction.GetAllergyDataById(conn, patientID);
                    dataGridView1.DataSource = allergyData;


                    if (allergyData.Rows.Count > 0)
                    {
                        DataRow allergyRow = allergyData.Rows[0];

                        // Populate textboxes with patient data
                        PopulateTextboxes(allergyRow);

                        // Update the mode based on the "IsDeleted" status
                        isOnMode = (Convert.ToInt32(allergyRow["IsDeleted"]) == 0);

                        SetControlstoReadOnly(!readOnly);
                        dataGridView1.Columns["PatientID"].Visible = false;
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

        private void PopulateTextboxes(DataRow allergyRow)
        {
            // Populate textboxes with patient data
            txtPID.Text = allergyRow["PatientID"].ToString();
            txtAllergyID.Text = allergyRow["AllergyID"].ToString();
            txtAllergen.Text = allergyRow["Allergen"].ToString();
            txtAllergyStart.Text = allergyRow["AllergyStartDate"].ToString();
            txtAllergyEnd.Text = allergyRow["AllergyEndDate"].ToString();
            txtAllergyDescription.Text = allergyRow["AllergyDescription"].ToString();

        }

        private void ClearTextboxes()
        {
            txtPID.Clear();
            txtAllergyID.Clear();
            txtAllergen.Clear();
            txtAllergyStart.Clear();
            txtAllergyEnd.Clear();
            txtAllergyDescription.Clear();
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

            ClearTextboxes();
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
                        if (selectedRow.Cells["AllergyID"].Value != null)
                        {
                            // Prompt the user for confirmation
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // Soft delete the medication record directly using AllergyID
                                int AllerID = (int)selectedRow.Cells["AllergyID"].Value;
                                int recsUpdated = DBInteraction.SoftDeleteAllergy(conn, AllerID);
                                MessageBox.Show($"Soft deleted {recsUpdated} records in Allergy!");

                                // Refresh the DataGridView after the update
                                PopulatePatientData();
                                dataGridView1.Columns["PatientID"].Visible = true;
                                dataGridView1.Columns["IsDeleted"].Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to retrieve AllergyID for the selected record.");
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

        private void btnExit_Click(object sender, EventArgs e) // exit button of footer
        {
            this.Close();
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
                !string.IsNullOrEmpty(txtAllergen.Text) &&
                !string.IsNullOrEmpty(txtAllergyStart.Text) &&
                !string.IsNullOrEmpty(txtAllergyDescription.Text) &&
                !string.IsNullOrEmpty(txtAllergyID.Text))

            {
                using (MySqlConnection conn = DBInteraction.MakeConnnection())
                {
                    AllergyProperties allergies = new AllergyProperties()
                    {
                        
                        AllergyID = txtAllergyID.Text,
                        PID = txtPID.Text,
                        Allergen = txtAllergen.Text,
                        AllergyStart = txtAllergyStart.Text,
                        AllergyEnd = txtAllergyEnd.Text,
                        Description = txtAllergyDescription.Text,
                    };

                    try
                    {
                        if (isAddMode)
                        {
                            // Insert the medication record
                            int recsInserted = DBInteraction.InsertAllergySP(conn, allergies);
                            MessageBox.Show($"Inserted {recsInserted} records into Allergy History!");
                        }
                        else
                        {
                            // Update the medication record
                            int recsUpdated = DBInteraction.UpdateAllergySP(conn, allergies);
                            MessageBox.Show($"Updated {recsUpdated} records in Allergy History!");
                        }

                        // Refresh the DataGridView after the update
                        PopulatePatientData();
                        SetControlstoReadOnly(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error! {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter required allergy information.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            UndoChanges();
        }

        private void StoreOriginalValues()
        {
            originalValues.Clear();  // Clear previous values

            // Store original values for each textbox using the Name property as the key
            originalValues.Add(txtAllergyID.Name, txtAllergyID.Text);
            originalValues.Add(txtAllergen.Name, txtAllergen.Text);
            originalValues.Add(txtAllergyStart.Name, txtAllergyStart.Text);
            originalValues.Add(txtAllergyEnd.Name, txtAllergyEnd.Text);
            originalValues.Add(txtAllergyDescription.Name, txtAllergyDescription.Text);
        }

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

        private void btnGoToPDemo_Click(object sender, EventArgs e)
        {
            if (patientID > 0)
            {
                // Open PdemoForm and pass patientID
                PatientDemo pdemoForm = new PatientDemo(patientID);
                pdemoForm.PopulatePatientData(patientID);  // method to populate medication data
                pdemoForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // GO TO MEDICATIONS FORM FROM ALLERGY FORM
            if (patientID > 0)
            {
                // Open MedicationsForm and pass patientID
                Medications medsForm = new Medications(patientID);
                medsForm.PopulatePatientData();  // method to populate medication data
                medsForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }
        }

    }
}

