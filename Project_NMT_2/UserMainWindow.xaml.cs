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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        private List<ALLTest> ukrainainTest;
        private List<ALLTest> mathTest;
        private List<ALLTest> historyTest;

        private List<PassedTest> ukrainainTestPass;
        private List<PassedTest> mathTestPass;
        private List<PassedTest> historyTestPass;
        int? ID_user = null;
        public UserMainWindow(int id_user)
        {
            InitializeComponent();
            ID_user = id_user;
            Show_NotShowTabitem();
            InitializeUkrTest();
            InitializeUkrTestPass();
            InitializeMathTest();
            InitializeMathTestPass();
            InitializeHistiryTest();
            InitializeHistiryTestPass();

        }

        //Initialize
        private void InitializeUkrTest()
        {
            try
            {
                ukrainainTest = new List<ALLTest>();
                ukrainainTest = DBservice.GetSubjectTest("Українська мова").ToList();
                if (ukrainainTest!=null)
                {
                    ukrTest_ListView.ItemsSource = ukrainainTest;
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void InitializeUkrTestPass()
        {
            ukrainainTestPass = new List<PassedTest>();
            ukrainainTestPass = DBservice.GetSubjectTestPass("Українська мова", ID_user).ToList();
            try
            {
                if (ukrainainTestPass!=null)
                {
                    ukrTestPASS_ListView.ItemsSource = ukrainainTestPass;
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void InitializeMathTest()
        {
            try
            {
                mathTest = new List<ALLTest>();
                mathTest = DBservice.GetSubjectTest("Математика").ToList();
                if (mathTest!=null)
                {
                    mathTest_ListView.ItemsSource = mathTest;
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void InitializeMathTestPass()
        {
            try
            {
                mathTestPass = new List<PassedTest>();
                mathTestPass = DBservice.GetSubjectTestPass("Математика", ID_user).ToList();
                if (mathTestPass!=null)
                {
                    mathTestPASS_ListView.ItemsSource = mathTestPass;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void InitializeHistiryTest()
        {
            try
            {
                historyTest = new List<ALLTest>();
                historyTest = DBservice.GetSubjectTest("Історія України").ToList();
                if (historyTest!=null)
                {
                    historyTest_ListView.ItemsSource = historyTest;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void InitializeHistiryTestPass()
        {
            try
            {
                historyTestPass = new List<PassedTest>();
                historyTestPass = DBservice.GetSubjectTestPass("Історія України", ID_user).ToList();
                if (historyTestPass!=null)
                {
                    historyTestPASS_ListView.ItemsSource = historyTestPass;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }





        public void Show_NotShowTabitem()
        {
            var subjects = DBservice.GetSubjects((int)ID_user);
            bool Ukrainian = false;
            bool Mathematics = false;
            bool History = false;

            if (subjects.HasValue)
            {
                Ukrainian = subjects.Value.Ukrainian;
                Mathematics = subjects.Value.Mathematics;
                History = subjects.Value.History;

            }

            ///UKRAINIAN
            #region UKRAINIAN
            if (Ukrainian)
            {
                if (!SubjectstabControl.Items.Contains(UkrTabItem))
                {
                    SubjectstabControl.Items.Add(UkrTabItem);
                }
            }
            else
            {
                if (SubjectstabControl.Items.Contains(UkrTabItem))
                {
                    SubjectstabControl.Items.Remove(UkrTabItem);
                    
                }
            }
            #endregion

            ///MATH
            #region Mathematics
            if (Mathematics)
            {
                if (!SubjectstabControl.Items.Contains(MathTabItem))
                {
                    SubjectstabControl.Items.Add(MathTabItem);
                }
            }
            else
            {
                if (SubjectstabControl.Items.Contains(MathTabItem))
                {
                    SubjectstabControl.Items.Remove(MathTabItem);
                }
            }
            #endregion

            ///History
            #region History
            if (History)
            {
                if (!SubjectstabControl.Items.Contains(HistoryTabItem))
                {
                    SubjectstabControl.Items.Add(HistoryTabItem);
                }
            }
            else
            {
                if (SubjectstabControl.Items.Contains(HistoryTabItem))
                {
                    SubjectstabControl.Items.Remove(HistoryTabItem);
                }
            } 
            #endregion
        }


        //Button
        private void startUkr_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var test = ukrTest_ListView.SelectedItem as ALLTest;
                DBservice.AddPassTest(new PassedTest(test.Title, 0, test.CountQ, 20, test.id, int.Parse(ID_user.ToString())));
                InitializeUkrTestPass();
                ukrTestPASS_ListView.Items.Refresh();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("Ви не вибрали тест \n" + ex.Message);
            }
            
        }
        private void startMath_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var test = mathTest_ListView.SelectedItem as ALLTest;
                DBservice.AddPassTest(new PassedTest(test.Title, 0, test.CountQ, 20, test.id, int.Parse(ID_user.ToString())));
                InitializeMathTestPass();
                mathTestPASS_ListView.Items.Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ви не вибрали тест \n" + ex.Message);
            }
        }

        private void startHistory_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var test = historyTest_ListView.SelectedItem as ALLTest;
                DBservice.AddPassTest(new PassedTest(test.Title, 0, test.CountQ, 20, test.id, int.Parse(ID_user.ToString())));
                InitializeHistiryTestPass();
                historyTestPASS_ListView.Items.Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ви не вибрали тест \n" + ex.Message);
            }
        }

        private void end_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string messageBoxText = "Ви впевнені?";
                string caption = "Закриття додатка";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage image = MessageBoxImage.Exclamation;
                MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, button, image, MessageBoxResult.Yes);
                if(result==MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        
    }
}
