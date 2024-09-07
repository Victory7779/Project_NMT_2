using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace testNMTconfonmity
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public List<string> UserAnswers { get; set; }
        public string QuestionType { get; set; }
    }

    public partial class MainWindow : Window
    {
        private QuestionModel question;
        private List<string> userAnswers;
        private DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();

            // Ініціалізація таймера
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Якщо таймер пустий, ініціалізуємо з 60
            if (timer_Label.Text == null)
                timer_Label.Text = "60";  // Використовуємо властивість Text замість Content

            // Зменшуємо значення на 1 і оновлюємо відображення
            int tL = int.Parse(timer_Label.Text) - 1;  // Використовуємо Text для отримання значення
            timer_Label.Text = tL.ToString();  // Використовуємо Text для встановлення значення

            // Якщо час закінчився, зупиняємо таймер
            if (tL <= 0)
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Час вичерпано!");
            }
        }

        // Метод для завантаження питання
        public MainWindow(QuestionModel question) : this()
        {
            this.question = question;
            this.userAnswers = new List<string>();
            DisplayMatchingQuestion();
        }

        private void DisplayMatchingQuestion()
        {
            // Приклад заповнення текстових полів
            // Припустимо, що AnswersPanel і QuestionTextBlock визначені в XAML
            QuestionTextBlock.Text = question.QuestionText;
            AnswersPanel.Children.Clear();

            for (int i = 0; i < question.Answers.Count; i++)
            {
                TextBox userAnswerBox = new TextBox
                {
                    Name = $"UserAnswer_{i}",
                    Width = 200,
                    Margin = new Thickness(5)
                };
                AnswersPanel.Children.Add(userAnswerBox);
            }
        }

        private void SaveAndNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            userAnswers.Clear();
            foreach (var child in AnswersPanel.Children)
            {
                if (child is TextBox textBox)
                {
                    userAnswers.Add(textBox.Text);
                }
            }

            question.UserAnswers = new List<string>(userAnswers);
            LoadNextQuestionFromServer();
        }

        private async void LoadNextQuestionFromServer()
        {
            string url = "https://yourserver.com/api/nextQuestion";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(url);
                    QuestionModel nextQuestion = ParseQuestion(response);
                    this.question = nextQuestion;
                    DisplayMatchingQuestion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження питання: " + ex.Message);
                }
            }
        }

        private QuestionModel ParseQuestion(string data)
        {
            var parts = data.Split(';');
            if (parts.Length >= 3)
            {
                return new QuestionModel
                {
                    QuestionText = parts[0],
                    Answers = new List<string>(parts[1].Split(',')),
                    QuestionType = parts[2]
                };
            }
            return null;
        }
    }
}
