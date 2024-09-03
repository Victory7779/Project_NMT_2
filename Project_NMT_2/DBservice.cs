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
        //public static void AddNewUserPersonalInformation(UserPersonalInfomation user)
        //{
        //    try
        //    {



        //        using (var db = new SqlConnection(connectionString))
        //        {
        //            db.Open();
        //            string sql = $"INSERT UserPersonalInfomations (Photo, Name, Age) " +
        //                $" VALUES (@Photo, N'{user.Name}', {user.Age})";
        //            using (SqlCommand command = new SqlCommand(sql, db))
        //            {
        //                // Добавляем параметр с данными изображения
        //                command.Parameters.AddWithValue("@Photo", user.Photo);
        //                db.Execute(sql);

        //            }


        //            db.Close();

        //        }

        //        string query = "SELECT TOP 1 id "  +
        //            "FROM UserPersonalInfomations " +
        //            "ORDER BY ID DESC;";



        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            SqlCommand command = new SqlCommand(query, connection);
        //            connection.Open();
        //            object value = command.ExecuteScalar();
        //            if (value != null)
        //            {
        //                idOfUser = value.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //}

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

        //public static void AddNewSubjectUsers(SubjectUsers subjectUsers)
        //{
        //    try
        //    {
        //        using (var db = new SqlConnection(connectionString))
        //        {
        //            db.Open();

        //            // SQL-запрос для вставки данных
        //            string sql = "INSERT INTO SubjectUsers (Ukrainian, Mathematics, History, id_user) " +
        //                         "VALUES (@Ukrainian, @Mathematics, @History, @id_user);"; 
        //                         //"SELECT SCOPE_IDENTITY();";  // Получение ID последней вставленной записи

        //            using (SqlCommand command = new SqlCommand(sql, db))
        //            {
        //                // Добавляем параметры
        //                command.Parameters.AddWithValue("@Ukrainian", subjectUsers.Ukrainian); 
        //                command.Parameters.AddWithValue("@Mathematics", subjectUsers.Mathematics); 
        //                command.Parameters.AddWithValue("@History", subjectUsers.History);   
        //                command.Parameters.AddWithValue("@id_user", idOfUser);   // id_user


        //                db.Execute(sql);
        //            }
        //        }

        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //}

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



        //public static IEnumerable<string> GetUsersInformation()
        //=> new SqlConnection(connectionString).Query<string>("SELECT * FROM InitializationUsers" +
        //    "INNER JOIN UserPersonalInformations " +
        //    "ON InitializationUsers ");
    }
}
