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
        public static IEnumerable<ALLTest> GetALLTestsString()
            => new SqlConnection(connectionString).Query<ALLTest>("SELECT * FROM ALLTests ");

    }
}
