using Project_NMT_2.AddElementForQuestion;
using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using static System.Net.Mime.MediaTypeNames;

namespace Project_NMT_2
{

    public partial class WindowOnePoint : Window
    {
        private WindowCreateOrUpdateTest windowCreateOrUpdateTest;
        private string commands;
        private int idQuestion { get; set; }
        
        public WindowOnePoint(Window window1, string command)
        {
            windowCreateOrUpdateTest = window1 as WindowCreateOrUpdateTest;
            commands = command;
            idQuestion = ServiceDB.LastQuestion().id;
            InitializeComponent();
            InitializeElement();
        }

        private void InitializeElement()
        {
            if (commands=="One")
            {
                InitializeRadioBtn();
            }
            if (commands=="Many")
            {
                InitializeCheckBox();
            }
            if (commands=="Open")
            {

            }
            if (commands=="Comb")
            {

            }
        }

        private void InitializeRadioBtn()
        {
            try
            {
                for (int i = 0; i <= 1; i++)
                {
                    answer_StackPanel.Children.Add(RadioBtn.GetRadioBtn(answer_StackPanel).stackPanel);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeCheckBox()
        {
            try
            {
                for (int i = 0; i <= 1; i++)
                {
                    answer_StackPanel.Children.Add(CheckBx.GetCheckBox(answer_StackPanel).stackPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //ADD new element

        private void AddOneOption()
        {
            try
            {
                answer_StackPanel.Children.Add(RadioBtn.GetRadioBtn(answer_StackPanel).stackPanel);
            }
            catch
            {
                MessageBox.Show("Максимальна кількість варіантів = 6!");
            }
        }
        private void AddManyOptions()
        {
            try
            {
                answer_StackPanel.Children.Add(CheckBx.GetCheckBox(answer_StackPanel).stackPanel);
            }
            catch
            {
                MessageBox.Show("Максимальна кількість варіантів = 6!");
            }
        }
        private void AddOpenOption()
        {

        }
        private void AddMachingOption()
        {

        }

        private void addOneOption_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (commands == "One")
            {
                AddOneOption();
            }
            if(commands == "Many")
            {
                AddManyOptions();
            }
            if(commands == "Open")
            {
                AddOpenOption();
            }
            if (commands == "Comb")
            {
                AddMachingOption();
            }

        }

        private  void save_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               var question = new Model.QuestionsForTest() { id = (idQuestion + 1), question = question_TextBox.Text, id_test = windowCreateOrUpdateTest.test.id };
                var answers = answer_StackPanel.Children.OfType<StackPanel>();
                foreach (var answer in answers)
                {
                    var textBox = answer.Children.OfType<TextBox>().FirstOrDefault();
                    var radioBtn = answer.Children.OfType<RadioButton>().FirstOrDefault();
                    if (textBox!=null && radioBtn!=null)
                    {
                        question.SingleChoiceAnswers.Add(new SingleChoiceAnswer(textBox.Text, radioBtn.IsChecked.Value, question.id));
                    }
                }
                windowCreateOrUpdateTest.questions.Add(question);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exit_Btn_Click(object sender, RoutedEventArgs e)// WORK//
        {
            try
            {
                var exit = MessageBox.Show("Ви хочете вийти з сторінки?\n Питання не будуть збережені!", "Вихід з сторінки", MessageBoxButton.YesNo);
                if ( exit == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
