using Project_NMT_2.Model;
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

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for WindowCreateOrUpdateTest.xaml
    /// </summary>
    public partial class WindowCreateOrUpdateTest : Window
    {

        //Window - Menu questions
        private WindowMenuCreateTest _menuCreateTest;
        // Window Admin Test
        private WindowAdmin_TestStart _windowAdmin_TestStart;
        //Count questions
        private static int CountQ { get; set; } = 0;
        //List for questions
        private List<QuestionsForTest> questionsForTests { get; set; } = new List<QuestionsForTest>();
        //Test General Infomation for table ALLTests
        private ALLTest test { get; set; }

        public WindowCreateOrUpdateTest()
        {
            InitializeComponent();
        }

        private void addQuestion_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (test != null)
                {
                    if ((_menuCreateTest == null || PresentationSource.FromVisual(_menuCreateTest) == null))
                    {
                        _menuCreateTest = new WindowMenuCreateTest();
                        _menuCreateTest.Show();
                    }
                    else
                    {
                        _menuCreateTest.Activate();
                        //MessageBox.Show("Вікно вже відкрито або\n Ви не заповнили обов'язкові поля.");
                    }
                }
                else MessageBox.Show("Вікно вже відкрито або\n Ви не заповнили обов'язкові поля.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteQuestion_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveTest_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr");
            }

        }

        private void exit_Btn_Click(object sender, RoutedEventArgs e) //Work
        {
            try
            {
                if (_windowAdmin_TestStart==null)
                {
                    _windowAdmin_TestStart = new WindowAdmin_TestStart();
                    _windowAdmin_TestStart.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InformationGeneralTest()
        {
            try
            {
                int id_subject=0;
                if (subjectForTest_ComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Виберіть предмет");
                    return;
                }
                id_subject = ServiceDB.IdSchoolSubject((subjectForTest_ComboBox.SelectedItem as SchoolSubjects).subject);

                if (titleTest_TestBox.Text == " ")
                { 
                    MessageBox.Show("Напишіть назву теста.");
                    return;
                }
                if (int.Parse(timeTest_TestBox.Text.Substring(0,2))<=24 && int.Parse(timeTest_TestBox.Text.Substring(0, 2)) <= 0 && timeTest_TestBox.Text.Substring(2,1)==":")
                {
                    
                }

                test = new ALLTest(titleTest_TestBox.Text, int.Parse(timeTest_TestBox.Text), CountQ, id_subject);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
