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
    /// 


    ///Patern Command
    ///Command - interface
    ///
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
    //Command - for save information in class ALLTest
    class AddInfTest : ITest
    {
        private Receiver _receiver;
        private string _title;
        private int _time;
        private string _subject;
        private List<QuestionsForTest> _questions;
        public AddInfTest(Receiver receiver, string title, int time, string subject, List<QuestionsForTest> questions)
        {
            _receiver = receiver;
            _title = title;
            _time = time;
            _subject = subject;
            _questions = questions;
        }

        public void Execute()
        {
            this._receiver.IdAddClassALLTests();
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
               // this._receiver.CountQAddClassALLTests(_questions);
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
    //Command - for save new test
    class SaveInfoTest : ITest
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
    //Receiver - получатель, содержить информацию и действия обработки информации
    public class Receiver
    {
        private WindowCreateOrUpdateTest window;
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

        //public void QuestionsPrint(IEnumerable<QuestionsForTest> questions)
        //{
        //    window.question_ListView.ItemsSource = questions;
        //}

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

        //Save in class ALLTest
        public void IdAddClassALLTests()
        {
            try
            {
                var lastTest = ServiceDB.LastTest();
                window.test.id = lastTest.id + 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
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
        //public void CountQAddClassALLTests(List<QuestionsForTest> questions)
        //{
        //    try
        //    {
        //        if (questions != null)
        //        {
        //            window.test.CountQ = questions.Count();
        //        }
        //        else window.test.CountQ = 0;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //Save in DB new test
        public void TestSaveFinDB(ALLTest aLLTest)
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
        //public void QuestionsSaveinDB(List<QuestionsForTest> questions)
        //{
        //    try
        //    {
        //        ServiceDB.AddQuestions(questions);
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }




    public partial class WindowCreateOrUpdateTest : Window
    {
        //WINDOW
        //Window - Menu questions
        protected WindowMenuCreateTest _menuCreateTest;
        // Window Admin Test
        private WindowAdmin_TestStart _windowAdmin_TestStart;

        //
        //Count questions
        //private static int CountQ { get; set; } = 0;
        //Dictionary for questions with one 0ption
        public Dictionary<QuestionsForTest, SingleChoiceAnswer> questionWithOneOption { get; set; } = new Dictionary<QuestionsForTest, SingleChoiceAnswer>();
        //Dictionary for questions with many 0ptions
        public Dictionary<QuestionsForTest, MultipleChoiceAnswer> questionWithManyOptions { get; set; } = new Dictionary<QuestionsForTest, MultipleChoiceAnswer>();
        //Dictionary for questions with open 0ption
        public Dictionary<QuestionsForTest, OpenAnswer> questionWithOpenOption { get; set; } = new Dictionary<QuestionsForTest, OpenAnswer>();
        //Dictionary for questions with maching 0ption
        public Dictionary<QuestionsForTest, MachingAnswer> questionWithMaching { get; set; } = new Dictionary<QuestionsForTest, MachingAnswer>();

        //Test General Infomation for table ALLTests
        public ALLTest test { get; set; }

        public WindowCreateOrUpdateTest(WindowAdmin_TestStart admin_TestStart)
        {
            _windowAdmin_TestStart = admin_TestStart;
            InitializeComponent();
        }



        /// <summary>
        ///Button -  ADD Questions
        /// </summary>
        private void addQuestion_Btn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (_admin_TestStart.create_Btn.IsPressed == true)
            //    {
            //        AddTestForDB();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        //Button - Delete Question
        private void deleteQuestion_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        //Button - Save Test
        private void saveTest_Btn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Erorr");
            //}

        }

        //Button - Exit with this Window
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


        //
        //Fill test
        //
        private  void InformationGeneralTest()
        {
            //try
            //{
            //    int id_subject=0;
            //    if (subjectForTest_ComboBox.SelectedItem == null)
            //    {
            //        MessageBox.Show("Виберіть предмет");
            //        return;
            //    }
            //    id_subject = ServiceDB.IdSchoolSubject((subjectForTest_ComboBox.SelectedItem as SchoolSubjects).subject);

            //    if (titleTest_TestBox.Text == " ")
            //    { 
            //        MessageBox.Show("Напишіть назву теста.");
            //        return;
            //    }
            //    if (int.Parse(timeTest_TestBox.Text.Substring(0,2))<=24 && int.Parse(timeTest_TestBox.Text.Substring(0, 2)) <= 0 && timeTest_TestBox.Text.Substring(2,1)==":")
            //    {
                    
            //    }

            //    test = new ALLTest(titleTest_TestBox.Text, int.Parse(timeTest_TestBox.Text), CountQ, id_subject);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        //If click button CreateTest
        //private void AddTestForDB()
        //{
        //    try
            //{
            //        if (test != null)
            //        {
            //            if ((_menuCreateTest == null || PresentationSource.FromVisual(_menuCreateTest) == null))
            //            {
            //                _menuCreateTest = new WindowMenuCreateTest();
            //                _menuCreateTest.Show();
            //            }
            //            else
            //            {
            //                _menuCreateTest.Activate();
            //                //MessageBox.Show("Вікно вже відкрито або\n Ви не заповнили обов'язкові поля.");
            //            }
            //        }
            //        else MessageBox.Show("Вікно вже відкрито або\n Ви не заповнили обов'язкові поля.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //}

        //}
        private  void UpdateTestWithDB()
        {

        }

        


    }
}
