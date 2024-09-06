using Microsoft.Win32;
using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        bool ContainsCyrillic(string s)
        {
            return Regex.IsMatch(UserEmailRgstrTextBox.Text, @"\p{IsCyrillic}");
        }

        private void AddPhotoRgstrButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.bmp",
                Title = "Select an Image File"
            };
            if (openFileDialog.ShowDialog() == true)
            {

                Uri fileUri = new Uri(openFileDialog.FileName);
                UserPictureRegistrationImage.Source = new BitmapImage(fileUri);
            }
        }
        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new JpegBitmapEncoder(); // or some other encoder

                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
        private static bool CheckIfEmailExists(string connectionString, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT 1 FROM InitializationUsers WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    var result = command.ExecuteScalar();

                    return result != null;
                }
            }
        }

        private void SaveRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if(isAdminCheckBox.IsChecked == true) { SaveAdminsInformation(); return; }
            else { SaveUserInformation(); return;}
        }
        private void SaveAdminsInformation()
        {
            //проверка на существование такого имейла в базе данных
            string connectionString = @"Server=DESKTOP-0C5DMI4\MSSQLSERVER01; Database=TestNMT; Integrated Security=True; TrustServerCertificate=True;";


            string emailToCheck = UserEmailRgstrTextBox.Text;
            bool emailExists = CheckIfEmailExists(connectionString, emailToCheck);

            if (emailExists)
            {
                MessageBox.Show($"Email {UserEmailRgstrTextBox.Text} вже зайнятий, напишіть інший! ");
                UserEmailRgstrTextBox.Text = null;
                return;

            }
            //Проверка на правильность заполнение всех данных в окне RegistrationWindow
            bool containsCyrillic = ContainsCyrillic(UserEmailRgstrTextBox.Text);
            if (containsCyrillic == true) { MessageBox.Show("Строка [Email] вміщує символи кирилиці. Перевірте та спробуйте ще раз!"); return; }
            if (UserEmailRgstrTextBox.Text.Contains("@") != true) { MessageBox.Show("Ваш email не вміщає знаку (@), перевірте та спробуйте ще раз!"); return; }

            //bool isNumeric = int.TryParse(UserAgeRgstrTextBox.Text, out _);
            //if (isNumeric == false) { MessageBox.Show("Строка [Вік] вміщує інші символи, окрім цифи. Перевірте та спробуйте ще раз!"); return; }

            //bool checkedCheckBoxes = false;
            //if (UkrCheckBox.IsChecked == true || MathCheckBox.IsChecked == true || HistoryCheckBox.IsChecked == true)
            //{
            //    checkedCheckBoxes = true;
            //}
            if ( UserEmailRgstrTextBox != null && UserPasswordRgstrTextBox != null )
            {

            }
            else
            {
                MessageBox.Show("Ви не ввели эл.пошту та пароль, спробуйте ще раз!"); return;
            }

            //Создание и добавление InitializationAdmin to DB
            InitializationAdmin initializationAdmin = new InitializationAdmin(UserEmailRgstrTextBox.Text, UserPasswordRgstrTextBox.Text);
            DBservice.AddNewInitializationAdmins(initializationAdmin);

            
        }
        private void SaveUserInformation()
        {
            //проверка на существование такого имейла в базе данных
            string connectionString = @"Server=DESKTOP-0C5DMI4\MSSQLSERVER01; Database=TestNMT; Integrated Security=True; TrustServerCertificate=True;";


            string emailToCheck = UserEmailRgstrTextBox.Text;
            bool emailExists = CheckIfEmailExists(connectionString, emailToCheck);

            if (emailExists)
            {
                MessageBox.Show($"Email {UserEmailRgstrTextBox.Text} вже зайнятий, напишіть інший! ");
                UserEmailRgstrTextBox.Text = null;
                return;

            }




            //Проверка на правильность заполнение всех данных в окне RegistrationWindow
            bool containsCyrillic = ContainsCyrillic(UserEmailRgstrTextBox.Text);
            if (containsCyrillic == true) { MessageBox.Show("Строка [Email] вміщує символи кирилиці. Перевірте та спробуйте ще раз!"); return; }
            if (UserEmailRgstrTextBox.Text.Contains("@") != true) { MessageBox.Show("Ваш email не вміщає знаку (@), перевірте та спробуйте ще раз!"); return; }
            bool isNumeric = int.TryParse(UserAgeRgstrTextBox.Text, out _);
            if (isNumeric == false) { MessageBox.Show("Строка [Вік] вміщує інші символи, окрім цифи. Перевірте та спробуйте ще раз!"); return; }

            bool checkedCheckBoxes = false;
            if (UkrCheckBox.IsChecked == true || MathCheckBox.IsChecked == true || HistoryCheckBox.IsChecked == true)
            {
                checkedCheckBoxes = true;
            }
            if (UserNameRgstrTextBox != null && UserAgeRgstrTextBox != null && UserEmailRgstrTextBox != null && UserPasswordRgstrTextBox != null &&
          UserPictureRegistrationImage.Source != null && checkedCheckBoxes == true)
            {
            }
            else
            {
                MessageBox.Show("Ви не ввели де-які дані, спробуйте ще раз!" ); return;
            }

            //Создание и добавление UserPersonalInformation to DB
            var photoInByteFormat = BitmapSourceToByteArray((BitmapSource)UserPictureRegistrationImage.Source);
            UserPersonalInfomation new_user = new UserPersonalInfomation(UserNameRgstrTextBox.Text, Int32.Parse(UserAgeRgstrTextBox.Text), photoInByteFormat);
            DBservice.AddNewUserPersonalInformation(new_user);

            ////Создание и добавление InitializationUser to DB
            InitializationUser initializationUser = new InitializationUser(UserEmailRgstrTextBox.Text, UserPasswordRgstrTextBox.Text, new_user.id);
            DBservice.AddNewInitializationUSer(initializationUser, new_user);

            //Создание и добавление SubjectUSers to DB 

            bool? nullableUkr = UkrCheckBox.IsChecked;
            bool regularUkr = nullableUkr.GetValueOrDefault();

            bool? nullableMath = MathCheckBox.IsChecked;
            bool regularMath = nullableMath.GetValueOrDefault();

            bool? nullableHistory = HistoryCheckBox.IsChecked;
            bool regularHistory = nullableHistory.GetValueOrDefault();

            SubjectUsers subjectUser = new SubjectUsers(regularUkr, regularMath, regularHistory, new_user.id);
            DBservice.AddNewSubjectUsers(subjectUser);

        }


    }
}
