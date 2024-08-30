using Project_NMT_2.Model;
using Project_NMT_2.PatternForWork;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_NMT_2
{
    public partial class WindowCreateOrUpdateTest : Window
    {
        //WINDOW
        //Window - Menu questions
        //________________________________________________
        protected WindowMenuCreateTest _menuCreateTest;
        // Window Admin Test
        //______________________________________________________
        public WindowAdmin_TestStart _windowAdmin_TestStart;
        private WindowAdmin_TestStart windowAdmin_Test;

        public List<QuestionsForTest> questions { get; set; } = new List<QuestionsForTest>();
        public List<SingleChoiceAnswer> singleChoiceAnswers { get; set; } = new List<SingleChoiceAnswer>();
        public List<MultipleChoiceAnswer> multipleChoiceAnswers { get; set; } = new List<MultipleChoiceAnswer>();
        public List<OpenAnswer> openAnswers { get; set; } = new List<OpenAnswer>();
        public List<MachingAnswer> machingAnswers { get; set; } = new List<MachingAnswer>();
        public ALLTest test { get; set; }
        public int idQuestion { get; set; }
        //Work with test
        //____________________________________________
        private Commond_Create_Update_Save_Test.Invoker _workWithTest { get; set; } = new Commond_Create_Update_Save_Test.Invoker();
        private Commond_Create_Update_Save_Test.Receiver _receiver { get; set; }


        //Main
        //___________________________________________________________________
        public WindowCreateOrUpdateTest(WindowAdmin_TestStart admin_TestStart, string commond, ALLTest testChoose)
        {
            _receiver = new Commond_Create_Update_Save_Test.Receiver(this);
            _windowAdmin_TestStart = admin_TestStart;
            test = testChoose;
            if (commond== "create")
            {   
                test.id = ServiceDB.LastTest().id + 1;
            }
            if(commond== "update")
            {
                PrintInformationForTest();
            }
            idQuestion = ServiceDB.LastQuestion().id;
            InitializeComponent();
            
        }
        public void InitializeListQuestions()
        {
            try
            {
                if ( questions.Count==1)
                {
                    question_ListView.ItemsSource = questions;
                }
                else if(questions.Count>1)
                {
                    question_ListView.Items.Refresh();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PrintInformationForTest()///Пока под вопросам
        {
            MessageBox.Show(test.Title);
           // timeTest_TestBox = new System.Windows.Controls.TextBox();
            //titleTest_TestBox.Text = test.Title;
            //_workWithTest.SetOnMiddel(new Commond_Create_Update_Save_Test.PrintInfTest(_receiver, test.Title, test.Time, test.id_subject, questions));
            //_workWithTest.WorkWithTest();
        }

        ///Button -  ADD Questions
        /// _____________________________________________________________
        private void addQuestion_Btn_Click(object sender, RoutedEventArgs e)//WORK//
        {
            try
            {
                if (_menuCreateTest == null || PresentationSource.FromVisual(_menuCreateTest) == null)
                {
                    idQuestion++;
                    _menuCreateTest = new WindowMenuCreateTest(this);
                    _menuCreateTest.Show();
                }
                else
                {
                    _menuCreateTest.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Button - Delete Question
        //________________________________________________________
        private void deleteQuestion_Btn_Click(object sender, RoutedEventArgs e)
        {
            var questionDelete = question_ListView.SelectedItem as QuestionsForTest;
            questions.Remove(questionDelete);
            question_ListView.Items.Refresh();
            
        }

        //Button - Save Test
        //_________________________________________________________________
        private void saveTest_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _workWithTest.SetOnStart(new Commond_Create_Update_Save_Test.AddInfTest(_receiver, titleTest_TestBox.Text, int.Parse(timeTest_TestBox.Text), subjectForTest_ComboBox.SelectedItem.ToString(), questions, test));
                _workWithTest.SetOnFinish(new Commond_Create_Update_Save_Test.SaveInfoTest(_receiver, questions, test, singleChoiceAnswers, multipleChoiceAnswers, openAnswers, machingAnswers));
                _workWithTest.WorkWithTest();
                if (windowAdmin_Test == null)
                {
                    windowAdmin_Test = new WindowAdmin_TestStart();
                    windowAdmin_Test.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr");
            }

        }

        //Button - Exit with this Window
        private void exit_Btn_Click(object sender, RoutedEventArgs e) //Work
        {
            try
            {
                var exit = MessageBox.Show("Ви хочете вийти з сторінки?", "Вихід з сторінки", MessageBoxButton.YesNo);
                if (windowAdmin_Test==null && exit==MessageBoxResult.Yes)
                {
                    windowAdmin_Test = new WindowAdmin_TestStart();
                    windowAdmin_Test.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }




    }
}
