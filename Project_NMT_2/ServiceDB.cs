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
        //Выводим список школьных предметов
        public static IEnumerable<string> GetSchoolSubjects()
        {
            try
            {
                return new SqlConnection(connectionString).Query<string>("SELECT subject FROM SchoolSubjects ");
            }
            catch { return null; }
        }
        
        //Ищем id предмета по его названию
        public static int IdSchoolSubject(string subject)
        {
            try
            {
                int idSubject = new SqlConnection(connectionString).QueryFirst<int>($"SELECT id FROM SchoolSubjects WHERE subject='{subject}'");
                return idSubject;
            }
            catch { return 0; }
        }
        //Находим название предмета по id 
        public static string StringSchoolSubject(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<string>($"SELECT subject FROM SchoolSubjects WHERE id={id}");
            }
            catch { return null; }
        }



        //For table ALLTeats
        //Выводим список всех тестов
        public static IEnumerable<ALLTest> GetALLTests()
            => new SqlConnection(connectionString).Query<ALLTest>("SELECT * FROM ALLTests ");
        //Вывод теста по id
        public static ALLTest GetALLTestWithID(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<ALLTest>($"SELECT * FROM ALLTests WHERE id={id}");
            }
            catch { return null; }
        }
        // Вывод id теста по его названию
        public static int GetIdTest(string title)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<int>($"SELECT id FROM ALLTests WHERE Titlt='{title}'");
            }
            catch { return 0; }
        }

        //Выводим последний тест, чтобы получить id
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

        //Удаление теста по id
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

        //Добавить один тест 
        public static void AddALLTestSQL(ALLTest test)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string sql = $"INSERT ALLTests (Title, CountQ, Time, id_subject) " +
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

        //Список тестов с выбранным предметом
        public static IEnumerable<ALLTest> GetTestWithSubject(int subjectId)
        {
            try
            {
                return new SqlConnection(connectionString).Query<ALLTest>($"SELECT * FROM ALLTests WHERE id_subject={subjectId} ");
            }
            catch { return null; }
        }

        //Обновить название теста
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
        //Обновить время теста
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
        //Обновить количество вопросов теста
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
        //Обновить школьный предмет теста
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

        // id последнего вопроса в базк данных
        public static QuestionsForTest LastQuestion()
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<QuestionsForTest>("" +
                    "SELECT TOP 1* " +
                    "FROM QuestionsForTests " +
                    "ORDER BY id DESC");
            }
            catch { return null; }
        }
        //Подсчет количества вопросов для теста 
        public static int? CountQuestionsOfTest(int id)
        {
            if (GetALLTestWithID(id) == null) return null;
            int count = new SqlConnection(connectionString).QueryFirst<int>($"SELECT COUNT(*) WHERE id_test={id}");
            return count;

        }
        //Вывод вопроса по id
        public static QuestionsForTest GetQuestionsWithId(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<QuestionsForTest>($"" +
                    $"SELECT * FROM QuestionsForTests WHERE id={id}");
            }
            catch { return null; }
        }
        //Вывод всех вопросов за id_test
        public static IEnumerable<QuestionsForTest> GetQuestions(int id_test)
        {
            try
            {
                return new SqlConnection(connectionString).Query<QuestionsForTest>($"SELECT * FROM QuestionsForTests WHERE id_test={id_test}");
            }
            catch { return null; }
        }

        //Добавление коллекции вопросов
        public static void AddQuestions(IEnumerable<QuestionsForTest> questions)//???
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
        //Добавление одного вопроса в базу данных
        public static void AddQuestion(QuestionsForTest question)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                    string sql = $"" +
                        $"INSERT QuestionsForTests (question, id_test) " +
                        $"VALUES ('{question.question}', {question.id_test})";
                    db.Execute(sql);
                db.Close();
            }
        }


        //Удаление вопроса с его вариантами ответов
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
        //Добавление список ответов
        public static void AddSingleChoiceAnswers(SingleChoiceAnswer answer)
        {
            using (var db = new SqlConnection(connectionString))
            {
                int answerR=0;
                if (answer.answerTrueOrFalse == true) { answerR = 1; }
                db.Open();
                string sql = $"" +
                    $"INSERT SingleChoiceAnswers (textAnswer, answerTrueOrFalse, id_question) " +
                    $"VALUES ('{answer.textAnswer}', {answerR}, {answer.id_question})";
                db.Execute(sql);
                db.Close();
            }
        }

        public static IEnumerable<SingleChoiceAnswer> GetSingleChoiceAnswersForQuestion(int id_question)
        {
            try
            {
                return new SqlConnection(connectionString).Query<SingleChoiceAnswer>($"SELECT * FROM SingleChoiceAnswers WHERE id_question={id_question}");
            }
            catch { return null; }
        }
        ///Answer MultipleChoiceAnswer
        ///
        //Добавление список ответов
        public static void AddMultipleChoiceAnswer(MultipleChoiceAnswer answer)
        {
            using (var db = new SqlConnection(connectionString))
            {
                int answerR = 0;
                if (answer.answerTrueOrFalse == true) { answerR = 1; }
                db.Open();
                string sql = $"" +
                   $"INSERT MultipleChoiceAnswers (textAnswer, answerTrueOrFalse, id_question) " +
                   $"VALUES ('{answer.textAnswer}', {answerR}, {answer.id_question})";
                db.Execute(sql);
                db.Close();
            }
        }

        public static IEnumerable<MultipleChoiceAnswer> GetMultipleChoiceAnswerForQuestion(int id_question)
        {
            try
            {
                return new SqlConnection(connectionString).Query<MultipleChoiceAnswer>($"SELECT * FROM MultipleChoiceAnswers WHERE id_question={id_question}");
            }
            catch { return null; }
        }
        ///Answer OpenAnswer
        ///
        //Добавление список ответов
        public static void AddOpenAnswer(OpenAnswer answer)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                string sql = $"" +
                    $"INSERT OpenAnswers (textAnswerOne, textAnswerTwo, id_question) " +
                    $"VALUES ('{answer.textAnswerOne}', '{answer.textAnswerTwo}', {answer.id_question})";
                db.Execute(sql);
                db.Close();
            }
        }

        public static IEnumerable<OpenAnswer> GetOpenAnswerForQuestion(int id_question)
        {
            try
            {
                return new SqlConnection(connectionString).Query<OpenAnswer>($"SELECT * FROM OpenAnswers WHERE id_question={id_question}");
            }
            catch { return null; }
        }
        ///Answer MachingAnswer
        ///
        //Добавление список ответов
        public static void AddMachingAnswer(MachingAnswer answer)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                string sql = $"" +
                    $"INSERT MachingAnswers (textAnswer, answerTrueOrFalse, id_question) " +
                    $"VALUES ('{answer.textAnswer}', '{answer.answerTrueOrFalse}', {answer.id_question})";
                db.Execute(sql);
                db.Close();
            }
        }

        public static IEnumerable<MachingAnswer> GetMachingAnswerForQuestion(int id_question)
        {
            try
            {
                return new SqlConnection(connectionString).Query<MachingAnswer>($"SELECT * FROM MachingAnswers WHERE id_question={id_question}");
            }
            catch { return null; }
        }
        //Users
        //____________________________________________________________
        public static IEnumerable<UserPersonalInfomation> GetAllUsers ()
        {
            try
            {
                return new SqlConnection(connectionString).Query<UserPersonalInfomation>("SELECT * FROM UserPersonalInfomations");
            }
            catch { return null; }
        }
        public static void UserPersonalBlocking(int blocking, int id_user)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                string sql = $"" +
                    $"UPDATE UserPersonalInfomations " +
                    $"SET blocking={blocking} " +
                    $"WHERE id={id_user}";
                db.Execute(sql);
                db.Close();
            }
        }
        public static UserPersonalInfomation GetUserPersonal(int id_user)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<UserPersonalInfomation>($"SELECT * FROM UserPersonalInfomations WHERE id={id_user}");
            }
            catch { return null; }
        }

        //Reviews
        public static IEnumerable<Reviews> GetReviewsOneUsers(int id_User)
        {
            try
            {
                return new SqlConnection(connectionString).Query<Reviews>($"SELECT * FROM Reviews WHERE id_user={id_User}");
            }
            catch { return null; }
        }
        public static IEnumerable<Reviews>GetAllReviews()
        {
            try
            {
                return new SqlConnection(connectionString).Query<Reviews>($"SELECT * FROM Reviews");
            }
            catch { return null; }
        }
        public static Reviews GetOneReview(int id)
        {
            try
            {
                return new SqlConnection(connectionString).QueryFirst<Reviews>($"SELECT * FROM Reviews WHERE id={id}");
            }
            catch { return null; }
        }
        public static void DeleteReviews(int id)
        {
            if (ServiceDB.GetOneReview(id) == null) return;
            using(var db=new SqlConnection(connectionString))
            {
                db.Open();
                string sql = $"" +
                    $"DELETE FROM Reviews WHERE id={id}";
                db.Execute(sql);
                db.Close();
            }

        }
    }
}
