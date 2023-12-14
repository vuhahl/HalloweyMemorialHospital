using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HollowellMemorialHospital;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace HalloweyMemorialHospital
{
    internal class DBInteraction
    {
        //connection code 
        public static MySqlConnection MakeConnnection()
        {
            string connStr = "server=127.0.0.1; uid=root; pwd=pnwPRIDE4321; database=hollowellmemorial";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        // retrieve patient method for get patient button on Select Patient Form
        public static DataTable GetAllPatients(MySqlConnection conn)
        {
            string SQLquery = "SELECT PatientID, PtFirstName, PtLastName, DOB, PtHomePhone,  Gender " +
                "FROM patientdemo WHERE IsDeleted = 0";
            DataTable dt = new DataTable();

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = new MySqlCommand(SQLquery, conn);
            da.Fill(dt);
            return dt;
        }

        // Getting Patient Data byt ID in order to populate Patient Demo DATAVIEW with selection
        public static DataTable GetPatientDataById(MySqlConnection connection, int patientID)
        {
            DataTable patientData = new DataTable();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM PatientDemo WHERE PatientID = @PatientID AND IsDeleted = 0 ;", connection))
                {
                    cmd.Parameters.AddWithValue("@PatientID", patientID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(patientData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPatientDataById: {ex.Message}");
            }

            return patientData;
        }

        // get MEDICATION DATA BY PATIENT ID
        public static DataTable GetMedicationDataById(MySqlConnection connection, int patientID)
        {
            DataTable medicationData = new DataTable();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM pmedications WHERE PatientID = @PatientID", connection))
                {
                    cmd.Parameters.AddWithValue("@PatientID", patientID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(medicationData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetMedicationDataById: {ex.Message}");
            }

            return medicationData;
        }

        //get allergy data by patient id
        public static DataTable GetAllergyDataById(MySqlConnection connection, int patientID)
        {
            DataTable allergyData = new DataTable();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM allergyhist WHERE PatientID = @PatientID", connection))
                {
                    cmd.Parameters.AddWithValue("@PatientID", patientID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(allergyData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllergyDataById: {ex.Message}");
            }

            return allergyData;
        }

        // insert patient in add mode
        public static int InsertPatientSP(MySqlConnection connection, Patient patient)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("InsertPatientSP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    AddPatientParam(cmd, patient);

                    // Execute the query
                    int recordsAffected = cmd.ExecuteNonQuery();
                    return recordsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InsertPatientUsingSP: {ex.Message}");
                throw;
            }
        }

        // filling text boxes - patient demo
        public static void AddPatientParam(MySqlCommand cmd, Patient patient)
        {
            
            cmd.Parameters.Add("@HospitalMRParam", MySqlDbType.VarChar).Value = patient.HospitalMR;
            cmd.Parameters.Add("@PtLastNameParam", MySqlDbType.VarChar).Value = patient.LastName;
            cmd.Parameters.Add("@PtPreviousLastNameParam", MySqlDbType.VarChar).Value = patient.PreviousLName;
            cmd.Parameters.Add("@PtFirstNameParam", MySqlDbType.VarChar).Value = patient.FirstName;
            cmd.Parameters.Add("@PtMiddleInitialParam", MySqlDbType.VarChar).Value = patient.MiddleInitial;
            cmd.Parameters.Add("@SuffixParam", MySqlDbType.VarChar).Value = patient.Suffix;
            cmd.Parameters.Add("@HomeAddressParam", MySqlDbType.VarChar).Value = patient.HomeAddress;
            cmd.Parameters.Add("@HomeCityParam", MySqlDbType.VarChar).Value = patient.HomeCity;
            cmd.Parameters.Add("@StateProvinceRegionParam", MySqlDbType.VarChar).Value = patient.StateProvinceRegion;
            cmd.Parameters.Add("@HomeZipParam", MySqlDbType.VarChar).Value = patient.HomeZip;
            cmd.Parameters.Add("@CountryParam", MySqlDbType.VarChar).Value = patient.Country;
            cmd.Parameters.Add("@CitizenshipParam", MySqlDbType.VarChar).Value = patient.Citizenship;
            cmd.Parameters.Add("@PtHomePhoneParam", MySqlDbType.VarChar).Value = patient.HomePhone;
            cmd.Parameters.Add("@EmergencyPhoneNumberParam", MySqlDbType.VarChar).Value = patient.EmergencyPhoneNumber;
            cmd.Parameters.Add("@EmailAddressParam", MySqlDbType.VarChar).Value = patient.Email;
            cmd.Parameters.Add("@SSNParam", MySqlDbType.VarChar).Value = patient.SSN;
            cmd.Parameters.Add("@DOBParam", MySqlDbType.VarChar).Value = patient.DOB;
            cmd.Parameters.Add("@GenderParam", MySqlDbType.VarChar).Value = patient.Gender;
            cmd.Parameters.Add("@EthnicAssociationParam", MySqlDbType.VarChar).Value = patient.EthnicAssociation;
            cmd.Parameters.Add("@ReligionParam", MySqlDbType.VarChar).Value = patient.Religion;
            cmd.Parameters.Add("@MaritalStatusParam", MySqlDbType.VarChar).Value = patient.MaritalStatus;
            cmd.Parameters.Add("@EmploymentStatusParam", MySqlDbType.VarChar).Value = patient.EmploymentStatus;
            cmd.Parameters.Add("@DateofExpireParam", MySqlDbType.VarChar).Value = patient.DateofExpire;
            cmd.Parameters.Add("@ReferralParam", MySqlDbType.VarChar).Value = patient.Referral;
            cmd.Parameters.Add("@CurrentPrimaryHCPIDParam", MySqlDbType.VarChar).Value = patient.CurrentPrimaryHCPId;
            cmd.Parameters.Add("@CommentsParam", MySqlDbType.VarChar).Value = patient.Comments;
            cmd.Parameters.Add("@DateEnteredParam", MySqlDbType.VarChar).Value = patient.DateEntered;
            cmd.Parameters.Add("@NextOfKinIDParam", MySqlDbType.VarChar).Value = patient.NextOfKinId;
            cmd.Parameters.Add("@NextOfKinRelationshipToPatientParam", MySqlDbType.VarChar).Value = patient.NextOfKinRelationship;


        }
        // PATIENT DEMO for modify mode - updating patient 
        public static int UpdatePatientSP(MySqlConnection connection, Patient patient)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UpdatePatientSP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    AddPatientUpdateParam(cmd, patient);
                    int recordsUpdated = cmd.ExecuteNonQuery();
                    return recordsUpdated;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePatientRecord: {ex.Message}");
                throw;
            }
        }

        // modify mode 
        public static void AddPatientUpdateParam(MySqlCommand cmd, Patient patient)
        {
            cmd.Parameters.AddWithValue("@PatientIDParam", patient.PID);
            cmd.Parameters.AddWithValue("@HospitalMRParam", patient.HospitalMR);
            cmd.Parameters.AddWithValue("@PtLastNameParam", patient.LastName);
            cmd.Parameters.AddWithValue("@PtPreviousLastNameParam", patient.PreviousLName);
            cmd.Parameters.AddWithValue("@PtFirstNameParam", patient.FirstName);
            cmd.Parameters.AddWithValue("@PtMiddleInitialParam", patient.MiddleInitial);
            cmd.Parameters.AddWithValue("@SuffixParam", patient.Suffix);
            cmd.Parameters.AddWithValue("@HomeAddressParam", patient.HomeAddress);
            cmd.Parameters.AddWithValue("@HomeCityParam", patient.HomeCity);
            cmd.Parameters.AddWithValue("@StateProvinceRegionParam", patient.StateProvinceRegion);
            cmd.Parameters.AddWithValue("@HomeZipParam", patient.HomeZip);
            cmd.Parameters.AddWithValue("@CountryParam", patient.Country);
            cmd.Parameters.AddWithValue("@CitizenshipParam", patient.Citizenship);
            cmd.Parameters.AddWithValue("@PtHomePhoneParam", patient.HomePhone);
            cmd.Parameters.AddWithValue("@EmergencyPhoneNumberParam", patient.EmergencyPhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddressParam", patient.Email);
            cmd.Parameters.AddWithValue("@SSNParam", patient.SSN);
            cmd.Parameters.AddWithValue("@DOBParam", patient.DOB);
            cmd.Parameters.AddWithValue("@GenderParam", patient.Gender);
            cmd.Parameters.AddWithValue("@EthnicAssociationParam", patient.EthnicAssociation);
            cmd.Parameters.AddWithValue("@ReligionParam", patient.Religion);
            cmd.Parameters.AddWithValue("@MaritalStatusParam", patient.MaritalStatus);
            cmd.Parameters.AddWithValue("@EmploymentStatusParam", patient.EmploymentStatus);
            cmd.Parameters.AddWithValue("@DateofExpireParam", patient.DateofExpire);
            cmd.Parameters.AddWithValue("@ReferralParam", patient.Referral);
            cmd.Parameters.AddWithValue("@CurrentPrimaryHCPIdParam", patient.CurrentPrimaryHCPId);
            cmd.Parameters.AddWithValue("@CommentsParam", patient.Comments);
            cmd.Parameters.AddWithValue("@DateEnteredParam", patient.DateEntered);
            cmd.Parameters.AddWithValue("@NextOfKinIdParam", patient.NextOfKinId);
            cmd.Parameters.AddWithValue("@NextOfKinRelationshipToPatientParam", patient.NextOfKinRelationship);
            
        }

        //delete mode for patient demo 
        public static int SoftDeletePatient(MySqlConnection connection, int PID)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SoftDeletePatientSP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Update the IsDeleted parameter to 1
                    cmd.Parameters.AddWithValue("@PatientIDParam", PID);
                    cmd.Parameters.AddWithValue("@IsDeletedParam", 1);

                    // Execute the query
                    int recordsUpdated = cmd.ExecuteNonQuery();
                    return recordsUpdated;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SoftDeleteMedication: {ex.Message}");
                throw;
            }
        }

        // delete mode for medication class 
        public static int SoftDeleteMedication(MySqlConnection connection, int medID)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SoftDeleteMedicationSP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Update the IsDeleted parameter to 1
                    cmd.Parameters.AddWithValue("@MedicationIDParam", medID);
                    cmd.Parameters.AddWithValue("@IsDeletedParam", 1);

                    // Execute the query
                    int recordsUpdated = cmd.ExecuteNonQuery();
                    return recordsUpdated;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SoftDeleteMedication: {ex.Message}");
                throw;
            }
        }


        //inserting new medication record
        public static int InsertMedicationSP(MySqlConnection connection, Meds medication)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("InsertMedicationSP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    AddMedicationParam(cmd, medication);

                    // Execute the query
                    int recordsAffected = cmd.ExecuteNonQuery();
                    return recordsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InsertMedicationSP: {ex.Message}");
                throw;
            }
        }

        public static void AddMedicationParam(MySqlCommand cmd, Meds medication)
        {
            cmd.Parameters.Add("@MedicationIDParam", MySqlDbType.VarChar).Value = medication.PID;
            cmd.Parameters.Add("@PIDParam", MySqlDbType.VarChar).Value = medication.PID;
            cmd.Parameters.Add("@MedicationParam", MySqlDbType.VarChar).Value = medication.MedName;
            cmd.Parameters.Add("@MedicationAmtParam", MySqlDbType.VarChar).Value = medication.MedAMT;
            cmd.Parameters.Add("@MedicationUnitParam", MySqlDbType.VarChar).Value = medication.MedUnit;
            cmd.Parameters.Add("@InstructionsParam", MySqlDbType.VarChar).Value = medication.Instructions;
            cmd.Parameters.Add("@MedicationStartDateParam", MySqlDbType.VarChar).Value = medication.MedStart;
            cmd.Parameters.Add("@MedicationEndDateParam", MySqlDbType.VarChar).Value = medication.MedEnd;
            cmd.Parameters.Add("@PrescriptionHCPParam", MySqlDbType.VarChar).Value = medication.Prescription;
            
        }

        public static int UpdateMedicationSP(MySqlConnection connection, Meds medication)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UpdateMedicationSP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    AddMedicationUpdateParam(cmd, medication);

                    // Execute the query
                    int recordsUpdated = cmd.ExecuteNonQuery();
                    return recordsUpdated;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateMedicationSP: {ex.Message}");
                throw;
            }
        }

        public static void AddMedicationUpdateParam(MySqlCommand cmd, Meds medication)
        {
            cmd.Parameters.AddWithValue("@MedicationIDParam", medication.MedID);
            cmd.Parameters.AddWithValue("@PIDParam", medication.PID);
            cmd.Parameters.AddWithValue("@MedicationParam", medication.MedName);
            cmd.Parameters.AddWithValue("@MedicationAmtParam", medication.MedAMT);
            cmd.Parameters.AddWithValue("@MedicationUnitParam", medication.MedUnit);
            cmd.Parameters.AddWithValue("@InstructionsParam", medication.Instructions);
            cmd.Parameters.AddWithValue("@MedicationStartDateParam", medication.MedStart);
            cmd.Parameters.AddWithValue("@MedicationEndDateParam", medication.MedEnd);
            cmd.Parameters.AddWithValue("@PrescriptionHCPParam", medication.Prescription);
        }
    }

    }

