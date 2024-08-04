using Dapper;
using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2
{
    public class ServiceDB
    {
        private static readonly string connectionString = @"Server=DESKTOP-0C5DMI4\MSSQLSERVER01; Database=TestNMT; Integrated Security=True; TrustServerCertificate=True;";
        //For table SchoolSubjects 
        public static IEnumerable<string> GetSchoolSubjects()
        => new SqlConnection(connectionString).Query<string>("SELECT subject FROM SchoolSubjects ");

        //For table ALLTeats
        public static IEnumerable<ALLTest> GetALLTests()
            => new SqlConnection(connectionString).Query<ALLTest>("SELECT * FROM ALLTests ");
        public static ALLTest GetALLTestWithID(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<ALLTest>($"SELECT * FROM ALLTests WHERE id={id}");
            }
            catch { return null; }
        }
        public static void DeleteALLTest(int id)
        {
            try
            {
                if (GetALLTestWithID(id) == null) return;
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"DELETE ALLTests WHERE id={id}";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddALLTestSQL(ALLTest test)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"INSERT ALLTests (Title, CountQ, Time) " +
                        $"VALUES ('{test.Title}', {test.CountQ}, {test.Time})";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Count question
        public static int? CountQuestionsOfTest(int id)
        {
            if (GetALLTestWithID(id) == null) return null;
            int count = new SqlConnection(connectionString).QueryFirst<int>($"SELECT COUNT(*) WHERE id_test={id}");
            return count;

        }

        //Subject 
        public static int IdSchoolSubject(string subject)
        {
            try
            {
                int idSubject = new SqlConnection(connectionString).QueryFirst<int>($"SELEC id FROM SchoolSubjects WHERE subject={subject}");
                return idSubject;
            }
            catch { return 0; }
        }
    }
}
