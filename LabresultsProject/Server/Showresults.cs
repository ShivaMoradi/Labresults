/*
using System;
using System.Globalization;
using static LabresultsProject.Results;
namespace Server
{
    public class Showresults
    {
        public List<Patient> Parse(string filePath)
        {
            var users = new List<Patient>();

            using (var reader = new StreamReader("C:\\Users\\Admin\\source\\repos\\minitestDiaverum\\Labresults\\LabresultsProject\\Server\\Software Developer Test - Appendix Q10.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split('|');

                    if (fields.Length == 4)
                    {
                        var patient = new Patient
                        {
                            Id = int.Parse(fields[0]),
                            Name = fields[1],
                            Email = fields[2],
                            DateOfBirth = DateTime.ParseExact(fields[3], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        };

                        users.Add(patient);
                    }
                }
            }

            return users;
        }
    }


}
}*/
