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
    /// Interaction logic for WindowMenuCreateTest.xaml
    /// </summary>
    public partial class WindowMenuCreateTest : Window
    {
        private WindowCreateOrUpdateTest windowCreateOrUpdateTest;

        private WindowOnePoint windowOnePoint;

        public WindowMenuCreateTest(Window window)
        {
            windowCreateOrUpdateTest = window as WindowCreateOrUpdateTest;
            InitializeComponent();
        }

        private void CreateQuestion(string command)
        {
            try
            {
                if (windowOnePoint == null)
                {
                    windowOnePoint = new WindowOnePoint(windowCreateOrUpdateTest, command);
                    windowOnePoint.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void oneOption_Btn_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestion("One");
        }

        private void manyOptions_Btn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (windowManyOptions == null)
            //    {
            //        windowManyOptions = new WindowManyOptions(windowCreateOrUpdateTest);
            //        windowManyOptions.Show();
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            CreateQuestion("Many");
        }

        private void openOption_Btn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (windowOpenOption == null)
            //    {
            //        windowOpenOption = new WindowOpenOption(windowCreateOrUpdateTest);
            //        windowOpenOption.Show();
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            CreateQuestion("Open");
        }

        private void conformityOption_Btn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (windowMarginOptions == null)
            //    {
            //        windowMarginOptions = new WindowMarginOptions(windowCreateOrUpdateTest);
            //        windowMarginOptions.Show();
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            CreateQuestion("Comb");
        }

    }
}
