// import neccesery namespaces
// Database connection
// Define variables
// Create a class and a method of Class to read the text file
// Save the data from text file to database mysql/ DataGrip
// display the data in a webpage.


using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.IO;
using static LabresultsProject.Results;
namespace LabresultsProject;

public class Results
{

    public record Patient(int CLINIC_NO, string BARCODE, int PATIENT_ID, string PATIENT_NAME, DateTime DOB, string GENDER, DateTime COLLECTIONDATE, DateTime COLLECTIONTIME,
                         string TESTCODE, string TESTNAME, string RESULT, string UNIT, string REFRANGELOW, string REFRANGEHIGH, string NOTE, string NONSPECREFS);
    public class LabResultP
    {
        string input = "C:\\Users\\Admin\\source\\repos\\minitestDiaverum\\Labresults\\LabresultsProject\\Server\\Software-Developer-Test-Appendix-Q10.csv";
        public List<Patient> Parse(string input)
        {
            var labResults = new List<Patient>();
            using (var reader = new StreamReader(input))
            {                

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split('|');

                    if (fields.Length >= 16)
                    {
                        var patient = new Patient(
                                int.Parse(fields[0]),
                                fields[1],
                                int.Parse(fields[2]),
                                fields[3],
                                DateTime.Parse(fields[4]),
                                fields[5],
                                DateTime.Parse(fields[6]),
                                DateTime.Parse(fields[7]),
                                fields[8],
                                fields[9],
                                fields[10],
                                fields[11],
                                fields[12],
                                fields[13],
                                fields[14],
                                fields[15]
                            );
                        labResults.Add(patient);
                    }

                }
            }
            return labResults;
        }
    }


    public static List<Patient> All(State state)
    {
        List<Patient> patients = new();
        string strQuery = "SELECT * FROM patients";
        using (var reader = MySqlHelper.ExecuteReader(state.DB, strQuery))
        {

            while (reader.Read())
            {
                var patient = new Patient(
                 reader.GetInt32("CLINIC_NO"),
                       reader.GetString("BARCODE"),
                       reader.GetInt32("PATIENT_ID"),
                       reader.GetString("PATIENT_NAME"),
                       reader.GetDateTime("DOB"),
                       reader.GetString("GENDER"),
                       reader.GetDateTime("COLLECTIONDATE"),
                       reader.GetDateTime("COLLECTIONTIME"),
                       reader.GetString("TESTCODE"),
                       reader.GetString("TESTNAME"),
                       reader.GetString("RESULT"),
                       reader.GetString("UNIT"),
                       reader.GetString("REFRANGELOW"),
                       reader.GetString("REFRANGEHIGH"),
                       reader.GetString("NOTE"),
                       reader.GetString("NONSPECREFS")
                );
            

                patients.Add(patient);

            }
        }
            return patients;
        }

    public static void SaveToDatabase(List<Patient> patients, State state)
    {
    foreach (var patient in patients)
    {
        string strQuery = "INSERT INTO patients (CLINIC_NO, BARCODE, PATIENT_ID, PATIENT_NAME, DOB, GENDER, COLLECTIONDATE, COLLECTIONTIME, TESTCODE, TESTNAME, RESULT, UNIT, REFRANGELOW, REFRANGEHIGH, NOTE, NONSPECREFS)" +
                          "values(@CLINIC_NO, @BARCODE, @PATIENT_ID, @PATIENT_NAME, @DOB, @GENDER, @COLLECTIONDATE, @COLLECTIONTIME, @TESTCODE, " +
                                 "@TESTNAME, @RESULT, @UNIT, @REFRANGELOW, @REFRANGEHIGH, @NOTE, @NONSPECREFS)";

        MySqlHelper.ExecuteNonQuery(state.DB, strQuery,

            new MySqlParameter("@CLINIC_NO", patient.CLINIC_NO),
            new MySqlParameter("@BARCODE", patient.BARCODE),
            new MySqlParameter("@PATIENT_ID", patient.PATIENT_ID),
            new MySqlParameter("@PATIENT_NAME", patient.PATIENT_NAME),
            new MySqlParameter("@DOB", patient.DOB),
            new MySqlParameter("@GENDER", patient.GENDER),
            new MySqlParameter("@COLLECTIONDATE", patient.COLLECTIONDATE),
            new MySqlParameter("@COLLECTIONTIME", patient.COLLECTIONTIME),
            new MySqlParameter("@TESTCODE", patient.TESTCODE),
            new MySqlParameter("@TESTNAME", patient.TESTNAME),
            new MySqlParameter("@RESULT", patient.RESULT),
            new MySqlParameter("@UNIT", patient.UNIT),
            new MySqlParameter("@REFRANGELOW", patient.REFRANGELOW),
            new MySqlParameter("@REFRANGEHIGH", patient.REFRANGEHIGH),
            new MySqlParameter("@NOTE", patient.NOTE),
            new MySqlParameter("@NONSPECREFS", patient.NONSPECREFS)
        );



    }

}
public static void Main(string[] args)
{
    var parser = new LabResultP();
    string input = "C:\\Users\\Admin\\source\\repos\\minitestDiaverum\\Labresults\\LabresultsProject\\Server\\Software-Developer-Test-Appendix-Q10.csv";
    var patients = parser.Parse(input);
    if (patients.Count == 0)
    {
        Console.WriteLine("No data to save. Exiting...");
        return;
    }
    string connectionString = "server=localhost;uid=root;pwd=mypassword;database=Labresults;port=3306";

    var state = new State(connectionString);
    SaveToDatabase(patients, state);

    Console.WriteLine("Data saved to database successfully.");
}
}



