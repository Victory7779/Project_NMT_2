using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace tasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private List<QuestionModel> questions;
        private int currentQuestionIndex = 0;
        private TimeSpan remainingTime;
        private System.Windows.Threading.DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            LoadQuestionsFromServer();

            // Ініціалізація таймера
            remainingTime = TimeSpan.FromMinutes(60); // 60 хвилин на тестування
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Метод для завантаження запитань з сервера
        private async void LoadQuestionsFromServer()
        {
            string url = "https://yourserver.com/api/questions"; // URL до вашого сервера

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(url);
                    ParseQuestions(response);
                    DisplayCurrentQuestion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження запитань: " + ex.Message);
                }
            }
        }

        // Метод для парсингу текстових даних
        private void ParseQuestions(string data)
        {
            questions = new List<QuestionModel>();

            var questionBlocks = data.Split('|'); // Розділення між питаннями

            foreach (var block in questionBlocks)
            {
                var parts = block.Split(';');
                if (parts.Length >= 2)
                {
                    var question = new QuestionModel
                    {
                        QuestionText = parts[0],
                        Answers = new List<string>(parts[1].Split(',')) // Відповіді розділені комами
                    };
                    questions.Add(question);
                }
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (questions != null && questions.Count > 0)
            {
                var question = questions[currentQuestionIndex];
                QuestionTextBlock.Text = question.QuestionText;

                // Очищення панелі з відповідями
                AnswersPanel.Children.Clear();

                foreach (var answer in question.Answers)
                {
                    RadioButton answerOption = new RadioButton
                    {
                        Content = answer,
                        GroupName = "Answers",
                        FontSize = 16
                    };
                    AnswersPanel.Children.Add(answerOption);
                }
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

