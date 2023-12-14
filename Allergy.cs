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

        private void button2_Click(object sender, EventArgs e)
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
            txtAllergyID.Text = allergyRow["AllergyID"].ToString();
            txtAllergen.Text = allergyRow["Allergen"].ToString();
            txtAllergyStart.Text = allergyRow["AllergyStartDate"].ToString();
            txtAllergyEnd.Text = allergyRow["AllergyEndDate"].ToString();
            txtAllergyDescription.Text = allergyRow["AllergyDescription"].ToString();

        }

        private void ClearTextboxes()
        {
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

        }
    }
}

