using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Windows;
using Project_NMT_2;
 

namespace Project_NMT_2
{
    public class DBservice
    {
        private static readonly string connectionString = @"Server=DESKTOP-0C5DMI4\MSSQLSERVER01; Database=TestNMT; Integrated Security=True; TrustServerCertificate=True;";

        public static string idOfUser = string.Empty;
       

        public static void AddNewUserPersonalInformation(UserPersonalInfomation user)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();

                    // SQL-запрос для вставки данных
                    string sql = "INSERT INTO UserPersonalInfomations (Photo, Name, Age) " +
                                 "VALUES (@Photo, @Name, @Age);" +
                                 "SELECT SCOPE_IDENTITY();";  // Получение ID последней вставленной записи

                    using (SqlCommand command = new SqlCommand(sql, db))
                    {
                        // Добавляем параметры
                        command.Parameters.AddWithValue("@Photo", user.Photo); // Байтовый массив фото
                        command.Parameters.AddWithValue("@Name", user.Name); // Имя
                        command.Parameters.AddWithValue("@Age", user.Age);   // Возраст

                        // Выполняем запрос и получаем идентификатор последней вставленной записи
                        object insertedId = command.ExecuteScalar();
                        if (insertedId != null)
                        {
                            idOfUser = insertedId.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

       

        public static void AddNewSubjectUsers(SubjectUsers subjectUsers)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();

                    // SQL-запрос для вставки данных
                    string sql = "INSERT INTO SubjectUsers (Ukrainian, Mathematics, History, id_user) " +
                                 "VALUES (@Ukrainian, @Mathematics, @History, @id_user);";

                    using (SqlCommand command = new SqlCommand(sql, db))
                    {
                        // Добавляем параметры
                        command.Parameters.AddWithValue("@Ukrainian", subjectUsers.Ukrainian);
                        command.Parameters.AddWithValue("@Mathematics", subjectUsers.Mathematics);
                        command.Parameters.AddWithValue("@History", subjectUsers.History);
                        command.Parameters.AddWithValue("@id_user", idOfUser);   // id_user

                        // Выполняем команду
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddNewInitializationUSer(InitializationUser initializationUser, UserPersonalInfomation userInfo)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    
                    string sql = $"INSERT InitializationUsers (Email, Password, id_user) " +
                        $" VALUES ( '{initializationUser.Email}' , '{initializationUser.Password}', {idOfUser})";
                    db.Execute(sql);
                    db.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            MessageBox.Show("Данні користувача додані");
            
        }

        public static void AddNewInitializationAdmins(InitializationAdmin initializationAdmin)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();

                    string sql = $"INSERT InitializationAdmins (Email, Password) " +
                        $" VALUES ( '{initializationAdmin.Email}' , '{initializationAdmin.Password}' )";
                    db.Execute(sql);
                    db.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            MessageBox.Show("Данні адміна додані");

        }

        public static bool IsUserExists(string email, string password)
        {
            bool userExists = false;

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();

                    string sql = "SELECT COUNT(*) FROM InitializationUsers WHERE Email = @Email AND Password = @Password;";

                    using (SqlCommand command = new SqlCommand(sql, db))
                    {
                        // Добавляем параметры
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        // Выполняем команду и получаем количество записей
                        int count = (int)command.ExecuteScalar();

                        // Если count больше 0, значит такой пользователь существует
                        userExists = count > 0;
                        db.Execute(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return userExists;
        }


        public static int? GetUserIdInLoginWindow(string email, string password)
        {
            int? userId = null;

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();

                    string sql = $"SELECT id_user FROM InitializationUsers WHERE Email = @Email AND Password = @Password;";

                    using (SqlCommand command = new SqlCommand(sql, db))
                    {
                        // Добавляем параметры
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        // Выполняем запрос и получаем id_user
                        var result = command.ExecuteScalar();

                        // Проверяем, найден ли пользователь
                        if (result != null)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return userId;
        }

        public static (bool Ukrainian, bool Mathematics, bool History)? GetSubjects(int id_user)
        {
            (bool Ukrainian, bool Mathematics, bool History)? subjects = null;

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();

                    string sql = "SELECT Ukrainian, Mathematics, History FROM SubjectUsers WHERE id_user = @id_user;";

                    using (SqlCommand command = new SqlCommand(sql, db))
                    {
                        command.Parameters.AddWithValue("@id_user", id_user);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool Ukrainian = reader.GetBoolean(0);
                                bool Mathematics = reader.GetBoolean(1);
                                bool History = reader.GetBoolean(2);

                                subjects = (Ukrainian, Mathematics, History);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return subjects;
        }



        //Subject for test
        public static int GetIDSubject(string subject)
            => new SqlConnection(connectionString).QueryFirst<int>($"SELECT id FROM SchoolSubjects WHERE subject='{subject}'");


        // ALLTest
        public static IEnumerable<ALLTest> GetSubjectTest(string subject)
        {
            try
            {
                int id_subject = DBservice.GetIDSubject(subject);
                return new SqlConnection(connectionString).Query<ALLTest>($"SELECT * FROM ALLTests WHERE id_subject={id_subject}");
            }
            catch { return null; }
        }

        //Pass Test
        public static IEnumerable<PassedTest> GetSubjectTestPass(string subject, int? id_user)
        {
            try
            { 

                return new SqlConnection(connectionString).Query<PassedTest>($"SELECT * FROM PassedTests WHERE id_user={id_user}");
            }
            catch { return null; }
        }
        public static void AddPassTest(PassedTest passedTest)
        {
            using(var db= new SqlConnection(connectionString))
            {
                db.Open();
                string sql = $" INSERT PassedTests (Title, CorrectQ, CountQ, Time, id_test, id_user) " +
                    $"VALUES ('{passedTest.Time}', {passedTest.CorrectQ}, {passedTest.CountQ}, {passedTest.Time}, {passedTest.id_test}, {passedTest.id_user})";
                db.Execute(sql);
                db.Close();
            }
        }

    }
}
