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
                "FROM patientdemo";
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM PatientDemo WHERE PatientID = @PatientID", connection))
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
        // for modify mode 
        public static int UpdatePatientRecord(MySqlConnection connection, Patient patient)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UpdatePatientRecord", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    AddPatientParam(cmd, patient);

                    // Execute the query
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

    }

}

