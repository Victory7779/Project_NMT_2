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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           // Console.OutputEncoding = Encoding.UTF8;
            InitializeComponent();
            //InitializeDB();
        }

        public void InitializeDB()
        {
            var db = new TestNMT();
            List<UserPersonalInfomation> users = new List<UserPersonalInfomation>()
            {
                new UserPersonalInfomation("Bob", 22),
                new UserPersonalInfomation("Alex", 25),
                new UserPersonalInfomation("Tom", 27)
            };
            db.UserPersonalInfomations.AddRange(users);
            db.SaveChanges();
            //UserPersonalInfomation user1 = new UserPersonalInfomation() { Name = "Tom", Age = 27 };
            //UserPersonalInfomation user2 = new UserPersonalInfomation() { Name = "Bob", Age = 25 };
            //UserPersonalInfomation user3 = new UserPersonalInfomation() { Name = "Alex", Age = 37 };
            //db.UserPersonalInfomations.Add(user1);
            //db.UserPersonalInfomations.Add(user2);
            //db.UserPersonalInfomations.Add(user3);

            List<InitializationUser> initializationUsers = new List<InitializationUser>()
            {
                new InitializationUser("bob@gmail.com", "12645fgsakl", 1),
                new InitializationUser("alex@gmail.com", "13645dssasa", 2),
                new InitializationUser("tom@gmail.com", "6454jsjjdh", 3)
            };
            db.InitializationUsers.AddRange(initializationUsers);
            db.SaveChanges();
            List<Reviews> reviews = new List<Reviews>()
            {
                new Reviews("review1", 1),
                new Reviews("review1", 1),
                new Reviews("review1", 1),
                new Reviews("review1", 2),
                new Reviews("review1", 2)
            };
            db.Reviews.AddRange(reviews);
            db.SaveChanges();
            List<SubjectUsers> subjects = new List<SubjectUsers>()
            {
                new SubjectUsers(true, true, true, 1),
                new SubjectUsers(true, false, true, 2),
                new SubjectUsers(false, true, false, 3)
            };
            db.SubjectUsers.AddRange(subjects);
            db.SaveChanges();
            List<RatingUsers> ratingUsers = new List<RatingUsers>()
            {
                new RatingUsers(10, 25, 75, 1),
                new RatingUsers(32, 0, 4, 2),
                new RatingUsers(0, 54, 0, 3)
            };
            db.RatingUsers.AddRange(ratingUsers);
            db.SaveChanges();
            List<UkrainianSchoolPerformance> ukrainianSchoolPerformances = new List<UkrainianSchoolPerformance>()
            {
                new UkrainianSchoolPerformance(94, 1),
                new UkrainianSchoolPerformance(65, 2),
                new UkrainianSchoolPerformance(78, 1),
                new UkrainianSchoolPerformance(84, 1),
                new UkrainianSchoolPerformance(94, 1),
                new UkrainianSchoolPerformance(100, 2)
            };
            db.UkrainianSchoolPerformances.AddRange(ukrainianSchoolPerformances);
            db.SaveChanges();
            List<MathematicsSchoolPerformance> mathematicsSchoolPerformances = new List<MathematicsSchoolPerformance>()
            {
                new MathematicsSchoolPerformance(54, 1),
                new MathematicsSchoolPerformance(64, 3),
                new MathematicsSchoolPerformance(74, 3),
                new MathematicsSchoolPerformance(84, 3),
                new MathematicsSchoolPerformance(94, 1),
                new MathematicsSchoolPerformance(100, 1),
            };
            db.MathematicsSchoolPerformances.AddRange(mathematicsSchoolPerformances);
            db.SaveChanges();
            List<HistorySchoolPerformance> historySchoolPerformances = new List<HistorySchoolPerformance>()
            {
                new HistorySchoolPerformance(100, 2),
                new HistorySchoolPerformance(100, 2),
                new HistorySchoolPerformance(100, 2),
                new HistorySchoolPerformance(100, 1),
                new HistorySchoolPerformance(100, 1),
            };
            db.HistorySchoolPerformances.AddRange(historySchoolPerformances);
            db.SaveChanges();
            List<InitializationAdmin> admins = new List<InitializationAdmin>()
            {
                new InitializationAdmin("jerry@gmail.com", "dskljdkdn55")
            };
            db.InitializationAdmins.AddRange(admins);
            db.SaveChanges();
            List<SchoolSubjects> schools = new List<SchoolSubjects>()
            {
                new SchoolSubjects("Ukr"),
                new SchoolSubjects("Math"),
                new SchoolSubjects("History")
            };
            db.SchoolSubjects.AddRange(schools);
            db.SaveChanges();
            List<ALLTest> aLLTests = new List<ALLTest>()
            {
                new ALLTest("Test1 ", new DateTime().AddMinutes(60), 4, 1)
            };
            db.ALLTests.AddRange(aLLTests);
            db.SaveChanges();
            List<PassedTest> passedTests = new List<PassedTest>()
            {
                new PassedTest("Test1", 4, 4, new DateTime().AddMinutes(57), 1, 3)
            };
            db.PassedTests.AddRange(passedTests);
            db.SaveChanges();
            List<QuestionsForTest> questions = new List<QuestionsForTest>()
            {
                new QuestionsForTest("question1", 1),
                new QuestionsForTest("question2", 1),
                new QuestionsForTest("question3", 1),
                new QuestionsForTest("question4", 1)
            };
            db.QuestionsForTests.AddRange(questions);
            db.SaveChanges();
            List<SingleChoiceAnswer> singleChoiceAnswers = new List<SingleChoiceAnswer>()
            {
                new SingleChoiceAnswer("answer1", false, 1),
                new SingleChoiceAnswer("answer2", false, 1),
                new SingleChoiceAnswer("answer3", true, 1),
                new SingleChoiceAnswer("answer4", false, 1)
            };
            db.SingleChoiceAnswers.AddRange(singleChoiceAnswers);
            db.SaveChanges();
            List<MultipleChoiceAnswer> multipleChoiceAnswers = new List<MultipleChoiceAnswer>()
            {
                new MultipleChoiceAnswer("answer1", true, 2),
                new MultipleChoiceAnswer("answer2", false, 2),
                new MultipleChoiceAnswer("answer3", true, 2),
                new MultipleChoiceAnswer("answer4", true, 2)
            };
            db.MultipleChoiceAnswers.AddRange(multipleChoiceAnswers);
            db.SaveChanges();
            List<MachingAnswer> machingAnswers = new List<MachingAnswer>()
            {
                new MachingAnswer("answer1", "answerA", 3),
                new MachingAnswer("answer2", "answerB", 3),
                new MachingAnswer("answer3", "answerC", 3),
                new MachingAnswer("answer4", "answerD", 3)
            };
            db.MachingAnswers.AddRange(machingAnswers);
            db.SaveChanges();
            List<OpenAnswer> openAnswers = new List<OpenAnswer>()
            {
                new OpenAnswer("answer", "Answer", 4)
            };
            db.OpenAnswers.AddRange(openAnswers);
            db.SaveChanges();
            MessageBox.Show("ADD");
        }
    }
}
