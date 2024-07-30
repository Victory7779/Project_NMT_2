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
        private List<ALLTest> aLLTest { get; set; }
        public WindowAdmin_TestStart()
        {
           
            InitializeComponent();
            InitializeALLTest();
            
        }

        private void InitializeALLTest()
        {
            aLLTest = new List<ALLTest>();
            aLLTest = ServiceDB.GetALLTestsString().ToList();
            try
            {
                if (aLLTest != null)
                {
                    testListView.ItemsSource = aLLTest;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
            
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
