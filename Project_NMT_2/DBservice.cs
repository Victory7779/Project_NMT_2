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
        private static readonly string connectionString = @"Server=EMIL-KOSTENKO\SQLEXPRESS; Database=TestNMT; Integrated Security=True; TrustServerCertificate=True;";

        public static string idOfUser = string.Empty;

        // Метод для получения данных пользователя из базы данных
        public static UserPersonalInfomation GetUserData(int id_user)
        {
            UserPersonalInfomation userData = null;

            
            string query = "SELECT Name, Age, Photo FROM UserPersonalInfomations WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id_user);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader.GetString(0);
                        int age = reader.GetInt32(1);
                        byte[] photo = reader[2] as byte[];
                        userData = new UserPersonalInfomation(name, age, photo);
                        
                    }
                }
            }

            return userData;
        }
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



    }
}
