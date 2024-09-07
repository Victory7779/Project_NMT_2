using Project_NMT_2.AddElementForQuestion;
using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public WindowOnePoint(Window window1, string command)
        {
            windowCreateOrUpdateTest = window1 as WindowCreateOrUpdateTest;
            commands = command;
            InitializeComponent();
            InitializeElement();

        }


        //Initialize
        //__________________________________________________
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
                InitializeTextBox();
            }
            if (commands=="Comb")
            {
                InitializeMaching();
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
        private void InitializeTextBox()
        {
            try
            {
                answer_StackPanel.Children.Add(TextBx.GetTextBox(answer_StackPanel).stackPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitializeMaching()
        {
            try
            {
                for (int i = 0; i <2 ; i++)
                {
                    option_StackPanel.Children.Add(MachingDataGrid.GetMachingDataGrid(option_StackPanel).stackPanel);
                }
                answer_StackPanel.Children.Add(MachingDataGrid.GetMachingDataGrid(answer_StackPanel).stackPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        //ADD new element
        //_____________________________________________--
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
            try
            {
                answer_StackPanel.Children.Add(TextBx.GetTextBox(answer_StackPanel).stackPanel);
            }
            catch
            {
                MessageBox.Show("Максимальна кількість варіантів = 2!");
            }
        }
        private void AddMachingOption()
        {
            try
            {
                answer_StackPanel.Children.Add(MachingDataGrid.GetMachingDataGrid(answer_StackPanel).stackPanel);
                option_StackPanel.Children.Add(MachingDataGrid.GetMachingDataGrid(option_StackPanel).stackPanel);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Максимальна кількість варіантів!");
            }
        }


        //Save
        //___________________________________________________
        private void SaveOneOption()
        {
            try
            {
                var question = new Model.QuestionsForTest() { id = windowCreateOrUpdateTest.idQuestion, question = question_TextBox.Text, id_test = windowCreateOrUpdateTest.test.id };
                var answers = answer_StackPanel.Children.OfType<StackPanel>();
                bool result = false;
                foreach (var answer in answers)
                {
                    var radioBtn = answer.Children.OfType<RadioButton>().FirstOrDefault();
                    if (radioBtn.IsChecked==true)
                    {
                        result = true;
                    }

                }
                if (result)
                {
                    foreach (var answer in answers)
                    {
                        var textBox = answer.Children.OfType<TextBox>().FirstOrDefault();
                        var radioBtn = answer.Children.OfType<RadioButton>().FirstOrDefault();
                        if (textBox != null && radioBtn != null)
                        {
                            windowCreateOrUpdateTest.singleChoiceAnswers.Add(new SingleChoiceAnswer(textBox.Text, radioBtn.IsChecked.Value, question.id));
                        }
                    }
                    windowCreateOrUpdateTest.questions.Add(question);
                    this.Close();
                }
                else MessageBox.Show("Виберіть правильну відповідь");
            }
            catch 
            {
                MessageBox.Show("Помилка при збережені.\n Спробуйте зберегти знову, виправивши помилку!");
            }
        }
        private void SaveManyOptions()
        {
            try
            {
                var question = new Model.QuestionsForTest() { id = windowCreateOrUpdateTest.idQuestion, question = question_TextBox.Text, id_test = windowCreateOrUpdateTest.test.id };
                var answers = answer_StackPanel.Children.OfType<StackPanel>();
                bool result = false;
                foreach (var answer in answers)
                {
                    var checkBtn = answer.Children.OfType<CheckBox>().FirstOrDefault();
                    if (checkBtn.IsChecked == true)
                    {
                        result = true;
                    }

                }
                if (result)
                {
                    foreach (var answer in answers)
                    {
                        var textBox = answer.Children.OfType<TextBox>().FirstOrDefault();
                        var checkBtn = answer.Children.OfType<CheckBox>().FirstOrDefault();
                        if (textBox != null && checkBtn != null)
                        {
                            windowCreateOrUpdateTest.multipleChoiceAnswers.Add(new MultipleChoiceAnswer(textBox.Text, checkBtn.IsChecked.Value, question.id));
                        }
                    }
                    windowCreateOrUpdateTest.questions.Add(question);
                    this.Close();
                }
                else MessageBox.Show("Виберіть правильну відповідь");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збережені.\n Спробуйте зберегти знову, виправивши помилку!");
            }
        }
        private void SaveOpenOption()
        {
            try
            {
                var checkTextStack = answer_StackPanel.Children.OfType<StackPanel>().FirstOrDefault();
                var checkText = checkTextStack.Children.OfType<TextBox>().FirstOrDefault().Text;
                bool result = false;
                if (checkText!=null && checkText!="" && checkText!=" ")
                {
                    result = true;
                }

                if(result)
                {
                    var question = new Model.QuestionsForTest() { id = windowCreateOrUpdateTest.idQuestion, question = question_TextBox.Text, id_test = windowCreateOrUpdateTest.test.id };
                    var answers = answer_StackPanel.Children.OfType<StackPanel>();
                    string answer1 = null, answer2 = null;

                    foreach (var answer in answers)
                    {
                        var textBox = answer.Children.OfType<TextBox>().FirstOrDefault();
                        if (textBox != null && answer1 == null)
                        {
                            answer1 = textBox.Text;
                        }
                        else if (textBox != null && answer2 == null)
                        {
                            answer2 = textBox.Text;
                        }
                    }
                    windowCreateOrUpdateTest.openAnswers.Add(new OpenAnswer(answer1, answer2, question.id));
                    windowCreateOrUpdateTest.questions.Add(question);
                    this.Close();
                }
                else MessageBox.Show("Введіть першу правильну відповідь");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збережені.\n Спробуйте зберегти знову, виправивши помилку!");
            }
        }

        private void SaveMaching()
        {
            try
            {
                var question = new Model.QuestionsForTest() { id = windowCreateOrUpdateTest.idQuestion, question = question_TextBox.Text, id_test = windowCreateOrUpdateTest.test.id };
                var answersNumber = answer_StackPanel.Children.OfType<StackPanel>();
                var answerLetter = option_StackPanel.Children.OfType<StackPanel>();
                int count = option_StackPanel.Children.Count;
                if (answersNumber!=null && answerLetter!=null)
                {
                    int index = 0;
                    foreach (var aLetter in answerLetter)
                    {
                        windowCreateOrUpdateTest.machingAnswers.Add(new MachingAnswer(null, aLetter.Children.OfType<TextBox>().First().Text, question.id));
                    }
                    foreach (var aNumber in answersNumber)
                    {
                        windowCreateOrUpdateTest.machingAnswers.ElementAt(index).textAnswer = aNumber.Children.OfType<TextBox>().First().Text;
                        index++;
                    }
                }
                windowCreateOrUpdateTest.questions.Add(question);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Помилка при збережені.\n Спробуйте зберегти знову, виправивши помилку!");
            }
        }
        //Button
        //__________________________________________________
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
            if (commands == "One")
            {
                SaveOneOption();
            }
            if (commands == "Many")
            {
                SaveManyOptions();
            }
            if (commands == "Open")
            {
                SaveOpenOption();
            }
            if (commands == "Comb")
            {
                SaveMaching();
            }
           // MessageBox.Show($"{windowCreateOrUpdateTest.questions.Count}");
            windowCreateOrUpdateTest.InitializeListQuestions();
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
