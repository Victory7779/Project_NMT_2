using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;
using System.Data.SqlClient;
using Project_NMT_2;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DBservice.IsUserExists(EmailTextBox.Text, PasswordTextBox.Text) == true)
            {
                int id_user = (int)DBservice.GetUserIdInLoginWindow(EmailTextBox.Text, PasswordTextBox.Text);
                UserMainWindow userMainWindow = new UserMainWindow(id_user);
                userMainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ваш e-mail або пароль введені не вірно, спробуйте ще раз");
                EmailTextBox.Text = "";
                PasswordTextBox.Text = "";
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
