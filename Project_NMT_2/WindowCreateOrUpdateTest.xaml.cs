﻿using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Project_NMT_2
{
    ///Patern Command
    /////________________________________________
    ///Command - interface
    ///______________________________________________
    public interface ITest
    {
        void Execute();
    }
    //Commands - real
    //Command - for print information about test into Window
    class PrintInfTest : ITest
    {
        private Receiver _receiver;
        private string _title;
        private int _time;
        private int _subject;
        private List<QuestionsForTest> _questions = new List<QuestionsForTest>();
        public PrintInfTest(Receiver receiver, string title, int time, int subject, List<QuestionsForTest> questions)
        {
            _receiver = receiver;
            _title = title;
            _time = time;
            _subject = subject;
            _questions = questions;
        }
        public void Execute()
        {
            this._receiver.TitlePrint(_title);
            this._receiver.TimePrint(_time);
            this._receiver.SubjectPrint(_subject);
            //this._receiver.QuestionsPrint(_questions);
        }
    }
    //Command - for save information in class ALLTest ////WORK///
    class AddInfTest : ITest
    {
        private Receiver _receiver;
        private string _title;
        private int _time;
        private string _subject;
        private List<QuestionsForTest> _questions;
        private ALLTest _aLLTest;
        public AddInfTest(Receiver receiver, string title, int time, string subject, List<QuestionsForTest> questions, ALLTest aLLTest)
        {
            _receiver = receiver;
            _title = title;
            _time = time;
            _subject = subject;
            _questions = questions;
            _aLLTest = aLLTest;
        }

        public void Execute()
        {
            if (_title != null && _title != "" && _title != " ")
            {
                this._receiver.TitleAddClassALLTests(_title);
            }
            else MessageBox.Show("Введіть назву!");
            if (_time>0 && _time<10000)
            {
                this._receiver.TimeAddClassALLTests(_time);
            }
            else MessageBox.Show("Некоректний час!");
            if (_subject!=null && _subject!="" && _subject!=" ")
            {
                this._receiver.SubjectAddClassALLTests(_subject);
            }
            else MessageBox.Show("Данний предмет не існує!");
            if (_questions!=null)
            {
               this._receiver.CountQAddClassALLTests(_questions);
            }
        }
    }  
    //Command - for save information if test was saved
    class UpdateInfoTest : ITest
    {
        private Receiver _receiver;
        private ALLTest _aLLTest;
        private List<QuestionsForTest> _questions;
        private int _id_test;
        public UpdateInfoTest(Receiver receiver, ALLTest aLLTest, List<QuestionsForTest> questions, int id_test)
        {
            _receiver = receiver;
            _aLLTest = aLLTest;
            _questions = questions;
            _id_test = id_test;
        }

        public void Execute()
        {
                if (_aLLTest.Title!=null && _aLLTest.Title!="" && _aLLTest.Title!=" ")
                {
                    _receiver.TitleUpdateInDB(_aLLTest);
                }
                if (_aLLTest.Time!=null && _aLLTest.Time>0 && _aLLTest.Time<10000)
                {
                    _receiver.TimeUpdateInDB(_aLLTest);
                }
                if (_aLLTest.CountQ!=null)
                {
                    _receiver.CountQUpdateInDB(_aLLTest);
                }
                if(_aLLTest.id_subject!=null)
                {
                    _receiver.SubjectUpdateInDB(_aLLTest);
                }
                if(_questions!=null)
                {
               // _receiver.QuestionsUpdateInDB(_questions, _id_test);
                }
        }
    }
    //Command - for save new test ///WORK///
    class SaveInfoTest : ITest
    {
        private Receiver _receiver;
        private List<QuestionsForTest> _questions;
        private List<SingleChoiceAnswer> _singles;
        private List<MultipleChoiceAnswer> _multiples;
        private List<OpenAnswer> _opens;
        private List<MachingAnswer> _machings;
        private ALLTest _test;
        public SaveInfoTest(Receiver receiver, List<QuestionsForTest> questions, ALLTest test, List<SingleChoiceAnswer> singles, List<MultipleChoiceAnswer> multiples, List<OpenAnswer> opens, List<MachingAnswer> machings)
        {
            _receiver = receiver;
            _questions = questions;
            _test = test;
            _singles = singles;
            _multiples = multiples;
            _opens = opens;
            _machings = machings;
        }

        public void Execute()
        {
            if (_test.id!=null && _test.Title!=null && _test.Time!=null && _test.CountQ!=null && _test.id_subject!=null)
            {
                _receiver.TestSaveInDB(_test);
                if (_questions != null)
                {
                    try
                    {
                        _receiver.QuestionsSaveinDB(_questions, _singles, _multiples, _opens, _machings);
                        MessageBox.Show("Тест успішно збережено!");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else MessageBox.Show("Тест збережено без питань!");
            }
            else
            {
                MessageBox.Show("Зберегти не вийшло! \n Якісь дані не були правильно заповнені. ");
            }
        }
    }


    //Receiver - получатель, содержить информацию и действия обработки информации
    //________________________________________________________________________
    public class Receiver
    {
        private WindowCreateOrUpdateTest window;
        public Receiver(WindowCreateOrUpdateTest window)
        {
            this.window = window;
        }

        //private ALLTest tempTest { get; set; } = new ALLTest();
        //Print
        public void TitlePrint(string title)
        {
            window.timeTest_TestBox.Text = title;
        }
        public void TimePrint(int time)
        {
            window.timeTest_TestBox.Text = time.ToString();
        }
        public void SubjectPrint(int id_subject)
        {
            string subject = ServiceDB.StringSchoolSubject(id_subject);
            window.subjectForTest_ComboBox.SelectedValue = subject;
        }
        public void QuestionsPrint(IEnumerable<QuestionsForTest> questions)
        {
            window.question_ListView.ItemsSource = questions;
        }

        //Update=Save in DB
        public void TitleUpdateInDB(ALLTest aLLTest)
        {
            try
            {
                ServiceDB.UpdateALLTest_Title(aLLTest);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TimeUpdateInDB(ALLTest aLLTest)
        {
            try
            {
                ServiceDB.UpdateALLTest_Time(aLLTest);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SubjectUpdateInDB(ALLTest aLLTest)
        {
            try
            {
                ServiceDB.UpdateALLTest_IdSubject(aLLTest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //public void QuestionsUpdateInDB(List<QuestionsForTest> questions, int id_test)
        //{
        //    try
        //    {
        //        ServiceDB.DeleteQuestions(id_test);
        //        ServiceDB.AddQuestions(questions);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        public void CountQUpdateInDB(ALLTest aLLTest)
        {
            try
            {
                ServiceDB.UpdateALLTest_CountQ(aLLTest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Save in class ALLTest//WORK
        public void TitleAddClassALLTests(string title)
        {
            try
            {
                 window.test.Title = title;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TimeAddClassALLTests(int time)
        {
            try
            {
               window.test.Time = time;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SubjectAddClassALLTests(string subject)
        {
            try
            {
                int id_subject = ServiceDB.IdSchoolSubject(subject);
                if (id_subject != 0)
                {
                    window.test.id_subject = id_subject;
                }
                else { MessageBox.Show("Вибраний предмет не існує!"); }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CountQAddClassALLTests(List<QuestionsForTest> questions)
        {
            try
            {
                window.test.CountQ = 0;
                if (questions != null) window.test.CountQ = questions.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Save in DB new test //WORK
        public void TestSaveInDB(ALLTest aLLTest)
        {
            try
            {
                ServiceDB.AddALLTestSQL(aLLTest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void QuestionsSaveinDB(List<QuestionsForTest> questions,
            List<SingleChoiceAnswer> singles, List<MultipleChoiceAnswer> multiples,
            List<OpenAnswer> opens, List<MachingAnswer> machings)
        {
            try
            {
                ServiceDB.AddQuestions(questions);
                if (singles != null)
                {
                    foreach (var single in singles)
                    {
                        ServiceDB.AddSingleChoiceAnswers(single);
                    }
                }
                if (multiples != null)
                {
                    foreach (var multiple in multiples)
                    {
                        ServiceDB.AddMultipleChoiceAnswer(multiple);
                    }
                }
                if (machings != null)
                {
                    foreach (var maching in machings)
                    {
                        ServiceDB.AddMachingAnswer(maching);
                    }
                }
                if (opens != null)
                {
                    foreach (var open in opens)
                    {
                        ServiceDB.AddOpenAnswer(open);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    //Inviker - отправитель, содержит поочередность вызова команд
    //____________________________________________________________--
    public class Invoker
    {
        private ITest _onStart;
        private ITest _onMiddel;
        private ITest _onFinish;
        public void SetOnStart(ITest start)
        {
            _onStart = start;
        }
        public void SetOnMiddel(ITest middel)
        {
            _onMiddel = middel;
        }
        public void SetOnFinish(ITest finish)
        {
            _onFinish = finish;
        }
        public void WorkWithTest()
        {
            if (_onStart is ITest)
            {
                _onStart.Execute();
            }
            if (_onMiddel is ITest)
            {
                _onMiddel.Execute();
            }
            if (_onFinish is ITest)
            {
                _onFinish.Execute();
            }
        }
    }


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
        public ALLTest test { get; set; } = new ALLTest();
        public int idQuestion { get; set; }
        //Work with test
        //____________________________________________
        private Invoker _workWithTest { get; set; } = new Invoker();
        private Receiver _receiver { get; set; }


        //Main
        //___________________________________________________________________
        public WindowCreateOrUpdateTest(WindowAdmin_TestStart admin_TestStart)
        {
            _receiver = new Receiver(this);
            _windowAdmin_TestStart = admin_TestStart;
            test.id = ServiceDB.LastTest().id+1;
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
            int id_q = ServiceDB.IdSchoolSubject(subjectForTest_ComboBox.SelectedValue.ToString());
            MessageBox.Show(id_q.ToString());
        }

        //Button - Save Test
        //_________________________________________________________________
        private void saveTest_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _workWithTest.SetOnStart(new AddInfTest(_receiver, titleTest_TestBox.Text, int.Parse(timeTest_TestBox.Text), subjectForTest_ComboBox.SelectedItem.ToString(), questions, test));
                _workWithTest.SetOnFinish(new SaveInfoTest(_receiver, questions, test, singleChoiceAnswers, multipleChoiceAnswers, openAnswers, machingAnswers));
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
