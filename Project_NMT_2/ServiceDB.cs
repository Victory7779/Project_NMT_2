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
        //Subject 
        public static int IdSchoolSubject(string subject)
        {
            try
            {
                int idSubject = new SqlConnection(connectionString).QueryFirst<int>($"SELEC id FROM SchoolSubjects WHERE subject='{subject}'");
                return idSubject;
            }
            catch { return 0; }
        }
        public static string StringSchoolSubject(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<string>($"SELECT subject FROM SchoolSubjects WHERE id={id}");
            }
            catch { return null; }
        }

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
        public static int GetIdTest(string title)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<int>($"SELECT id FROM ALLTests WHERE Titlt='{title}'");
            }
            catch { return 0; }
        }

        public static ALLTest LastTest()
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<ALLTest>("" +
                    "SELECT TOP 1* " +
                    "FROM ALLTests " +
                    "ORDER BY id DESC");
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
                        $"VALUES ('{test.Title}', {test.CountQ}, {test.Time}, {test.id_subject})";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateALLTest_Title(ALLTest test)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"" +
                        $"UPDATE ALLTests " +
                        $"SET Title = '{test.Title}' " +
                        $"WHERE id={test.id}";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void UpdateALLTest_Time(ALLTest test)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"" +
                        $"UPDATE ALLTests " +
                        $"SET Time = {test.Time} " +
                        $"WHERE id={test.id}";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void UpdateALLTest_CountQ(ALLTest test)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"" +
                        $"UPDATE ALLTests " +
                        $"SET CountQ = {test.CountQ} " +
                        $"WHERE id={test.id}";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void UpdateALLTest_IdSubject(ALLTest test)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"" +
                        $"UPDATE ALLTests " +
                        $"SET id_subject = {test.id_subject} " +
                        $"WHERE id={test.id}";
                    db.Execute(sql);
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //Questions
        public static int? CountQuestionsOfTest(int id)
        {
            if (GetALLTestWithID(id) == null) return null;
            int count = new SqlConnection(connectionString).QueryFirst<int>($"SELECT COUNT(*) WHERE id_test={id}");
            return count;

        }
        public static QuestionsForTest GetQuestionsWithId(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<QuestionsForTest>($"" +
                    $"SELECT * FROM QuestionsForTests WHERE id={id}");
            }
            catch { return null; }
        }
        public static IEnumerable<QuestionsForTest> GetQuestions(int id_test)
        {
            try
            {
                return new SqlConnection(connectionString).Query<QuestionsForTest>($"SELECT * FROM QuestionsForTests WHERE id_test={id_test}");
            }
            catch { return null; }
        }


        public static void AddQuestions(IEnumerable<QuestionsForTest> questions)
        {
            using(var db = new SqlConnection(connectionString))
            {
                db.Open();
                foreach (var question in questions)
                {
                    string sql = $"" +
                        $"INSERT QuestionsForTests (question, id_test) " +
                        $"VALUES ('{question.question}', {question.id_test})";
                    db.Execute(sql);
                }
                db.Close();
            }
        }
        public static void DeleteQuestions(int id_test)
        {
            if (GetALLTestWithID(id_test) == null) return;
            using(var db = new SqlConnection(connectionString))
            {
                db.Open();
                string sql = $"" +
                    $"DELETE QuestionsForTests " +
                    $"WHERE id_test={id_test}";

                db.Execute(sql);
                db.Close();
            }
        }


        ///Answer SingleChoiceAnswer
        ///
        public static  IEnumerable<SingleChoiceAnswer> GetSingleChoiceAnswersForQuestion(int id_question)
        {
            try
            {
                return new SqlConnection(connectionString).Query<SingleChoiceAnswer>($"SELECT * FROM SingleChoiceAnswers WHERE id_question={id_question}");
            }
            catch { return null; }
        }

        ///Answer MultipleChoiceAnswer
        ///
        public static IEnumerable<MultipleChoiceAnswer> GetMultipleChoiceAnswersForQuestion(int id_question)
        {
            try
            {
                return new SqlConnection(connectionString).Query<MultipleChoiceAnswer>($"SELECT * FROM SingleChoiceAnswers WHERE id_question={id_question}");
            }
            catch { return null; }
        }

        ///Answer OpenAnswer
        ///

        ///Answer MachingAnswer
        ///


    }
}
