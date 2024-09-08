using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using static Project_NMT_2.PatternForWork.Commond_Create_Update_Save_Test;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for WindowTask_Passing.xaml
    /// </summary>
    public partial class WindowTask_Passing : Window
    {
        private ALLTest aLLTest;
        private List<QuestionsForTest> questions;
        private List<SingleChoiceAnswer> singleChoiceAnswers;
        private List<MultipleChoiceAnswer> multipleChoiceAnswers;
        private List<OpenAnswer> openAnswers;
        private List<MachingAnswer> machingAnswers;

        private int currentQuestionIndex = 0;
        private TimeSpan remainingTime;
        private System.Windows.Threading.DispatcherTimer timer;
        public WindowTask_Passing(int id_test)
        {
            InitializeComponent();
            //LoadQuestionsFromServer();
            aLLTest = ServiceDB.GetALLTestWithID(id_test);
            InitializeTestElement();

            // Ініціалізація таймера
            remainingTime = TimeSpan.FromMinutes(aLLTest.Time); //  хвилини на тестування з ALLTests
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeTestElement()
        {
            questions = ServiceDB.GetQuestions(aLLTest.id).ToList();
            singleChoiceAnswers = new List<SingleChoiceAnswer>();
            multipleChoiceAnswers = new List<MultipleChoiceAnswer>();
            openAnswers = new List<OpenAnswer>();
            machingAnswers = new List<MachingAnswer>();
        }

        private void LoadQuestionsFromServer()
        {
          //  questions = ServiceDB.GetQuestions(aLLTest.id).ToList();


            //string url = "https://yourserver.com/api/questions"; // URL до вашого сервера

            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        var response = await client.GetStringAsync(url);
            //        ParseQuestions(response);
            //        DisplayCurrentQuestion();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Помилка завантаження запитань: " + ex.Message);
            //    }
            //}
        }

        // Метод для парсингу текстових даних
        private void ParseQuestions(string data)
        {

            //var questionBlocks = data.Split('|'); // Розділення між питаннями

            //foreach (var block in questionBlocks)
            //{
            //    var parts = block.Split(';');
            //    if (parts.Length >= 2)
            //    {
            //        var question = new QuestionModel
            //        {
            //            QuestionText = parts[0],
            //            Answers = new List<string>(parts[1].Split(',')) // Відповіді розділені комами
            //        };
            //        questions.Add(question);
            //    }
            //}
        }

        private void AnswerForQuestion(QuestionsForTest question)
        {
            singleChoiceAnswers.Clear();
            multipleChoiceAnswers.Clear();
            openAnswers.Clear();
            machingAnswers.Clear();

            singleChoiceAnswers = ServiceDB.GetSingleChoiceAnswersForQuestion(question.id).ToList();
            multipleChoiceAnswers = ServiceDB.GetMultipleChoiceAnswerForQuestion(question.id).ToList();
            openAnswers = ServiceDB.GetOpenAnswerForQuestion(question.id).ToList();
            machingAnswers = ServiceDB.GetMachingAnswerForQuestion(question.id).ToList();
        }
        private void DisplayCurrentQuestion()
        {
            if (questions != null && questions.Count > 0)
            {
                var question = questions[currentQuestionIndex];
                QuestionTextBlock.Text = question.question;
                AnswerForQuestion(question);
                // Очищення панелі з відповідями
                AnswersPanel.Children.Clear();

                //foreach (var answer in question.Answers)
                //{
                //    RadioButton answerOption = new RadioButton
                //    {
                //        Content = answer,
                //        GroupName = "Answers",
                //        FontSize = 16
                //    };
                //    AnswersPanel.Children.Add(answerOption);
                //}
            }
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
            {
                DisplayCurrentQuestion();
            }
            else
            {
                MessageBox.Show("Всі питання пройдені!");
            }
        }

        // Метод для роботи з таймером
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Add(TimeSpan.FromSeconds(-1));
                TimerTextBlock.Text = $"Таймер: {remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Час вичерпано!");
            }
        }
    }

    // Клас моделі для збереження запитання і варіантів відповіді
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
    }
}

