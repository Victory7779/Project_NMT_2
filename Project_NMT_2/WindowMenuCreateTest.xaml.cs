using System;
using System.Windows;


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
            CreateQuestion("Many");
        }

        private void openOption_Btn_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestion("Open");
        }

        private void conformityOption_Btn_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestion("Comb");
        }

    }
}
