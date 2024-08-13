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
    /// Interaction logic for WindowAdmin_TestStart.xaml
    /// </summary>
    public partial class WindowAdmin_TestStart : Window
    {
        // Window for Test Template
        private WindowCreateOrUpdateTest createOrUpdateTest;

        //For Binding ListView
        private List<ALLTest> aLLTest { get; set; }
        public WindowAdmin_TestStart()
        {
            InitializeComponent();
            InitializeALLTest();
            
        }

        private void InitializeALLTest()
        {
            aLLTest = new List<ALLTest>();
            aLLTest = ServiceDB.GetALLTests().ToList();
            try
            {
                if (aLLTest != null)
                {
                    test_ListView.ItemsSource = aLLTest;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
            
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)//Work
        {
            try
            {
                if (createOrUpdateTest == null || PresentationSource.FromVisual(createOrUpdateTest) == null)
                {
                    createOrUpdateTest = new WindowCreateOrUpdateTest(this);
                    createOrUpdateTest.Show();
                    this.Close();
                }
                else createOrUpdateTest.Activate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e) //WORK!!!!
        {
            if (test_ListView.SelectedItem != null)
            {
                //Окно сообщения
                //-------------------------------------------------
                string messageBoxText = "Ви впевнені?";
                string caption = "Видалення теста";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                //-------------------------------------------------
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        ServiceDB.DeleteALLTest((test_ListView.SelectedItem as ALLTest).id);
                        aLLTest.Remove(test_ListView.SelectedItem as ALLTest);
                        test_ListView.Items.Refresh();
                        MessageBox.Show("Тест видалений!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else MessageBox.Show("Не вибрали тест!");
           
        }

        private void exit_Btn_Click(object sender, RoutedEventArgs e)// Work
        {
            try
            {
                var exit = MessageBox.Show("Ви дійсно хочете завершити програму?", "Вихід з програми", MessageBoxButton.YesNo);
                if (exit==MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
