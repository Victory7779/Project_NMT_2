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
using System.Windows.Threading;

namespace Project_NMT_2
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static WindowAdmin_TestStart windowAdmin_TestStart;
        System.Windows.Threading.DispatcherTimer timer;
        public  MainWindow()
        {

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            InitializeComponent();
           //InitializeDB();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (timer_Label.Content == null) timer_Label.Content = "60";
            string tL = (int.Parse(timer_Label.Content.ToString()) - 1).ToString();
            timer_Label.Content = tL;
        }
        

        public static void InitializeDB()
        {
            var db = new TestNMT();
            // For UserPersonalInfomation
            List<UserPersonalInfomation> users = new List<UserPersonalInfomation>()
            {
                new UserPersonalInfomation("Марія", 16),
                new UserPersonalInfomation("Alex", 15),
                new UserPersonalInfomation("Тоня", 17)
            };
            db.UserPersonalInfomations.AddRange(users);
            db.SaveChanges();

            //InitializationUser
            List<InitializationUser> initializationUsers = new List<InitializationUser>()
            {
                new InitializationUser("mary@gmail.com", "1111", 1),
                new InitializationUser("alex@gmail.com", "abcd123", 2),
                new InitializationUser("tona@gmail.com", "457As", 3)
            };
            db.InitializationUsers.AddRange(initializationUsers);
            db.SaveChanges();


            //Reviews
            List<Reviews> reviews = new List<Reviews>()
            {
                new Reviews("Круто!!!", 1),
                new Reviews("Но чего-то не хватает", 1),
                new Reviews("Побольше заданий.", 1),
                new Reviews("Непогано все.", 2),
                new Reviews("Дякую за цей додаток", 2)
            };
            db.Reviews.AddRange(reviews);
            db.SaveChanges();


            //SubjectUsers
            List<SubjectUsers> subjects = new List<SubjectUsers>()
            {
                new SubjectUsers(true, true, true, 1),
                new SubjectUsers(true, false, true, 2),
                new SubjectUsers(false, true, false, 3)
            };
            db.SubjectUsers.AddRange(subjects);
            db.SaveChanges();

            //RatingUsers
            List<RatingUsers> ratingUsers = new List<RatingUsers>()
            {
                new RatingUsers(0, 0, 0, 1),
                new RatingUsers(0, 0, 0, 2),
                new RatingUsers(0, 0, 0, 3)
            };
            db.RatingUsers.AddRange(ratingUsers);
            db.SaveChanges();

            //UkrainianSchoolPerformance
            List<UkrainianSchoolPerformance> ukrainianSchoolPerformances = new List<UkrainianSchoolPerformance>()
            {
                new UkrainianSchoolPerformance(0, 1),
                new UkrainianSchoolPerformance(0, 2)
            };
            db.UkrainianSchoolPerformances.AddRange(ukrainianSchoolPerformances);
            db.SaveChanges();


            //MathematicsSchoolPerformance
            List<MathematicsSchoolPerformance> mathematicsSchoolPerformances = new List<MathematicsSchoolPerformance>()
            {
                new MathematicsSchoolPerformance(0, 1),
                new MathematicsSchoolPerformance(0, 3)
            };
            db.MathematicsSchoolPerformances.AddRange(mathematicsSchoolPerformances);
            db.SaveChanges();


            //HistorySchoolPerformance
            List<HistorySchoolPerformance> historySchoolPerformances = new List<HistorySchoolPerformance>()
            {
                new HistorySchoolPerformance(0, 2),
                new HistorySchoolPerformance(0, 1)
            };
            db.HistorySchoolPerformances.AddRange(historySchoolPerformances);
            db.SaveChanges();

            //InitializationAdmin
            List<InitializationAdmin> admins = new List<InitializationAdmin>()
            {
                new InitializationAdmin("admin@gmail.com", "admin777")
            };
            db.InitializationAdmins.AddRange(admins);
            db.SaveChanges();

            ////For SchoolSubjects
            List<SchoolSubjects> schools = new List<SchoolSubjects>()
            {
                new SchoolSubjects("Українська мова"),
                new SchoolSubjects("Математика"),
                new SchoolSubjects("Історія")
            };
            db.SchoolSubjects.AddRange(schools);
            db.SaveChanges();

            //ALLTest
            List<ALLTest> aLLTests = new List<ALLTest>()
            {
                new ALLTest("Українська мова - НМТ2024 (демоваріант)", 60, 30, 1),
                new ALLTest("Математика - НМТ2024 (демоваріант)", 60, 22, 2),
                new ALLTest("Історія України - НМТ2024 (демоваріант)", 60, 30, 3)
            };
            db.ALLTests.AddRange(aLLTests);
            db.SaveChanges();


            //PassedTest
            List<PassedTest> passedTests = new List<PassedTest>()
            {
                new PassedTest("Українська мова - НМТ2024", 25, 30, 57, 1, 3)
            };
            db.PassedTests.AddRange(passedTests);
            db.SaveChanges();


            //QuestionsForTest
            List<QuestionsForTest> questions = new List<QuestionsForTest>()
            {

                // Українська мова 
                new QuestionsForTest("Звук [д] треба вимовляти на місці пропуску в усіх словах рядка", 1), //1
                new QuestionsForTest("Правильно виділено букви на позначення наголошених голосних у всіх словах, ОКРІМ", 1),//2
                new QuestionsForTest("Правила уживання великої букви у власних назвах дотримано в усіх рядках, ОКРІМ", 1),//3
                new QuestionsForTest("Букву 'и' треба писати на місці пропуску в усіх словах рядка", 1),//4
                new QuestionsForTest("Апострофа НЕ ТРЕБА писати на місці пропуску в обох словах рядка", 1),//5
                new QuestionsForTest("Правильно написано всі займенники рядка", 1),//6
                new QuestionsForTest("Букву 'и' треба писати на місці пропуску в усіх словах рядка", 1),//7
                new QuestionsForTest("НЕМАЄ орфографічних помилок у реченні", 1),//8
                new QuestionsForTest("Помилково утворено слово", 1),//9
                new QuestionsForTest("Потребує редагування вислів у рядку", 1),//10
                new QuestionsForTest("Редагування потребує речення", 1),//11
                new QuestionsForTest("НЕПРАВИЛЬНО оформлено чужу мову в рядку", 1),//12
                new QuestionsForTest("Прочитайте речення (цифра позначає попередній розділовий знак)." +
                                     "\n\nБарви київських мозаїк –(1) це одне з тих див,(2) яке захоплювало і,(3) мабуть,(4)\n" +
                                     "ніколи не перестане захоплювати всіх,(5) хто по-справжньому цінує прекрасне,(6) не\n " +
                                     "розгублене у віках мистецтво,(7) але ж тут майстерність не лише в кольорах…" +
                                     "\n\nНЕПРАВИЛЬНО обґрунтовано вживання розділових знаків у рядку", 1),//13
                new QuestionsForTest("Лексичної помилки НЕМАЄ в реченні", 1),//14
                new QuestionsForTest("Українським відповідником іншомовного слова 'диференціація' є", 1),//15
                new QuestionsForTest("Невдовзі мама з ______ виїхали на заміську трасу, до ______ залишалося\n" +
                                     "п’ятдесят чотири ______ .", 1),//16
                new QuestionsForTest("Запрошуємо на тренінг з письменницької майстерності, де Вас навчать ________\n" +
                                     "побудови якісного художнього тексту. На заняттях працюємо в зошитах, створених\n " +
                                     "________ культової книги «Гра престолів».", 1),//17
                new QuestionsForTest("Трембіта – ___________ музичний інструмент у світі: \n" +
                                     "її розмір може сягати від двох з половиною до ___________.", 1),//18
                new QuestionsForTest("Сашко __________ однокласників подякував __________ за цікаву поїздку.", 1),//19
                new QuestionsForTest("На практичних заняттях студенти опанували ________ вимови ________.", 1),//20
                new QuestionsForTest("I. Однак, чиста вода в Україні є: скуштувати таку можна \n" +
                                     "на Полтавщині, де майже всю її беруть з підземних джерел.\n\n" +
                                     "II. Оскільки очисні споруди розраховано на І і ІІ класи \n" +
                                     "забруднення, то 80 % проб питної води не відповідають \n" +
                                     "вимогам стандарту.\n\n" +
                                     "III. Вода, як відомо, відіграє найважливішу роль у житті людини, \n" +
                                     "проте нині 80 % українців змушені споживати неякісну воду, \n" +
                                     "тому що беруть її з поверхневих джерел, екологічний стан \n" +
                                     "яких щороку стає гіршим.\n\n" +
                                     "IV. Далека від нормативних вимог і вода з Дніпра, яку \n" +
                                     "використовує майже 75 % населення України.\n\n" +
                                     "V. Це відбувається через погане очищення стічних побутових і \n" +
                                     "промислових вод, наслідком якого є надмірне насичення \n" +
                                     "водойм органікою і наближення їх до ІІІ класу забруднення.\n\n" +
                                     "Первісний текст буде відновлено, якщо речення розташувати в послідовності, \n" +
                                     "запропонованій у рядку", 1),//21
                new QuestionsForTest("I. Однак, чиста вода в Україні є: скуштувати таку можна \n" +
                                    "на Полтавщині, де майже всю її беруть з підземних джерел.\n\n" +
                                    "II. Оскільки очисні споруди розраховано на І і ІІ класи \n" +
                                    "забруднення, то 80 % проб питної води не відповідають \n" +
                                    "вимогам стандарту.\n\n" +
                                    "III. Вода, як відомо, відіграє найважливішу роль у житті людини, \n" +
                                    "проте нині 80 % українців змушені споживати неякісну воду, \n" +
                                    "тому що беруть її з поверхневих джерел, екологічний стан \n" +
                                    "яких щороку стає гіршим.\n\n" +
                                    "IV. Далека від нормативних вимог і вода з Дніпра, яку \n" +
                                    "використовує майже 75 % населення України.\n\n" +
                                    "V. Це відбувається через погане очищення стічних побутових і \n" +
                                    "промислових вод, наслідком якого є надмірне насичення \n" +
                                    "водойм органікою і наближення їх до ІІІ класу забруднення.\n\n" +
                                    "Однорідними підметами ускладнено речення, позначене", 1),//22
                new QuestionsForTest("I. Однак, чиста вода в Україні є: скуштувати таку можна \n" +
                                    "на Полтавщині, де майже всю її беруть з підземних джерел.\n\n" +
                                    "II. Оскільки очисні споруди розраховано на І і ІІ класи \n" +
                                    "забруднення, то 80 % проб питної води не відповідають \n" +
                                    "вимогам стандарту.\n\n" +
                                    "III. Вода, як відомо, відіграє найважливішу роль у житті людини, \n" +
                                    "проте нині 80 % українців змушені споживати неякісну воду, \n" +
                                    "тому що беруть її з поверхневих джерел, екологічний стан \n" +
                                    "яких щороку стає гіршим.\n\n" +
                                    "IV. Далека від нормативних вимог і вода з Дніпра, яку \n" +
                                    "використовує майже 75 % населення України.\n\n" +
                                    "V. Це відбувається через погане очищення стічних побутових і \n" +
                                    "промислових вод, наслідком якого є надмірне насичення \n" +
                                    "водойм органікою і наближення їх до ІІІ класу забруднення.\n\n" +
                                    "Пунктуаційна помилка є в реченні, позначеному", 1),//23
                new QuestionsForTest("I. Однак, чиста вода в Україні є: скуштувати таку можна \n" +
                                    "на Полтавщині, де майже всю її беруть з підземних джерел.\n\n" +
                                    "II. Оскільки очисні споруди розраховано на І і ІІ класи \n" +
                                    "забруднення, то 80 % проб питної води не відповідають \n" +
                                    "вимогам стандарту.\n\n" +
                                    "III. Вода, як відомо, відіграє найважливішу роль у житті людини, \n" +
                                    "проте нині 80 % українців змушені споживати неякісну воду, \n" +
                                    "тому що беруть її з поверхневих джерел, екологічний стан \n" +
                                    "яких щороку стає гіршим.\n\n" +
                                    "IV. Далека від нормативних вимог і вода з Дніпра, яку \n" +
                                    "використовує майже 75 % населення України.\n\n" +
                                    "V. Це відбувається через погане очищення стічних побутових і \n" +
                                    "промислових вод, наслідком якого є надмірне насичення \n" +
                                    "водойм органікою і наближення їх до ІІІ класу забруднення.\n\n" +
                                    "Вставна конструкція є в реченні, позначеному", 1),//24
                new QuestionsForTest("I. Однак, чиста вода в Україні є: скуштувати таку можна \n" +
                                    "на Полтавщині, де майже всю її беруть з підземних джерел.\n\n" +
                                    "II. Оскільки 'очисні' споруди розраховано на І і ІІ класи \n" +
                                    "забруднення, то 80 % проб питної води не відповідають \n" +
                                    "вимогам стандарту.\n\n" +
                                    "III. Вода, як відомо, відіграє найважливішу роль у житті людини, \n" +
                                    "проте нині 80 % українців змушені споживати неякісну воду, \n" +
                                    "тому що беруть її з поверхневих джерел, 'екологічний' стан \n" +
                                    "яких щороку стає гіршим.\n\n" +
                                    "IV. 'Далека' від нормативних вимог і вода з Дніпра, яку \n" +
                                    "використовує майже 75 % населення України.\n\n" +
                                    "V. Це відбувається через 'погане' очищення стічних побутових і \n" +
                                    "промислових вод, наслідком якого є 'надмірне' насичення \n" +
                                    "водойм органікою і наближення їх до ІІІ класу забруднення.\n\n" +
                                    "Частиною складеного іменного присудка є слово", 1),//25
                new QuestionsForTest("Приєднайте до фрагмента речення (1–4) відповідний за значенням фразеологізм (А – Д).", 1),//26
                new QuestionsForTest("З’ясуйте, якою частиною мови (А – Д) є виділені в реченні слова (1 – 4) \n" +
                                    "(цифра позначає наступне слово).\n\n" +
                                    "Виділені слова в реченні\n\n" +
                                    "Дослідження (1)вчених доводять, що людину, яка не привчила (2)себе працювати \n" +
                                    "напружено, (3)долаючи звичку (4)зайвий раз відпочити, чекає обмеженість мислення, \n" +
                                    "відмова від усього творчого.", 1),//27
                new QuestionsForTest("Доберіть приклад (А – Д) до кожної умови вживання тире (1–4).", 1),//28
                new QuestionsForTest("Поєднайте виділені фрагменти простих (1–4) і складних (А – Д) речень за подібністю \n" +
                                     "синтаксичного значення. Зразок такого поєднання показано на схемі.", 1),//29
                new QuestionsForTest("Доберіть приклад (А – Д) до кожного типу складного речення (1–4).", 1),//30

                //Математика
                new QuestionsForTest("Question1?", 2), //1 - > id=31
                 new QuestionsForTest("Question2?", 2), //1 - > id=32
            };
            db.QuestionsForTests.AddRange(questions);
            db.SaveChanges();


            //SingleChoiceAnswer
            List<SingleChoiceAnswer> singleChoiceAnswers = new List<SingleChoiceAnswer>()
            {
                //Українська мова
                //1
                new SingleChoiceAnswer("ві..бити, проїз..ний", false, 1),
                new SingleChoiceAnswer("фу..бол, зорепа..", true, 1),
                new SingleChoiceAnswer("б..жола, з..авна", false, 1),
                new SingleChoiceAnswer("по..якував, ..ружба", false, 1),
                //2
                new SingleChoiceAnswer("к'у'рятина у ф'о'льзі, кр'о'їти фартух", false, 2),
                new SingleChoiceAnswer("укра'ї'нський літ'о'пис, добов'і' в'и'трати", false, 2),
                new SingleChoiceAnswer("заірж'а'віле св'е'рдло, привезт'и' р'а'зом", false, 2),
                new SingleChoiceAnswer("чергов'е' упод'о'бання, пі'а'ла з піц'е'рії", true, 2),
                //3
                new SingleChoiceAnswer("езопова мова, бульвар Академіка Вернадського", false, 3),
                new SingleChoiceAnswer("День соборності, Шекспірівський стиль", true, 3),
                new SingleChoiceAnswer("Національна опера України, Нобелівська премія", false, 3),
                new SingleChoiceAnswer("гора Ай-Петрі, кінофільм «Поводир»", false, 3),
                //4
                new SingleChoiceAnswer("по-лицарськ.., по-вовч.., завбільшк.., трич..", false, 4),
                new SingleChoiceAnswer("навкулачк.., заоч.., по-корейськ.., навпак..", false, 4),
                new SingleChoiceAnswer("по-лисяч.., почаст.., пошепк.., навколішк..", true, 4),
                new SingleChoiceAnswer("по-людськ.., завглибшк.., трох.., насторож..", false, 4),
                //5
                new SingleChoiceAnswer("дев..ятеро ластів..яток", false, 5),
                new SingleChoiceAnswer("сер..йозна деб..ютантка", true, 5),
                new SingleChoiceAnswer("р..ятівна ін..єкція", false, 5),
                new SingleChoiceAnswer("слов..янське плем..я", false, 5),
                //6
                new SingleChoiceAnswer("дехто, ніякий, скільки-небудь, абищо", true, 6),
                new SingleChoiceAnswer("будь-хто, аніхто, хто-зна-що, ніскільки", false, 6),
                new SingleChoiceAnswer("якийсь, будь-який, аби-хто, що-небудь", false, 6),
                new SingleChoiceAnswer("нічий, хто-небудь, де-який, будь-чий", false, 6),
                //7
                new SingleChoiceAnswer("височ..на, відгр..міти, вогн..ще", true, 7),
                new SingleChoiceAnswer("власн.ця, кош..чок, пост..лити", false, 7),
                new SingleChoiceAnswer("заб..рати, ч..рговий, тр..вожний", false, 7),
                new SingleChoiceAnswer("печ..во, шел..стіння, др..жати", false, 7),
                //8
                new SingleChoiceAnswer("Фізичний гарт і патриотичне виховання юнного покоління \n" +
                                        "українців сягає давніх часів.", false, 8),
                new SingleChoiceAnswer("Одним з основних завданнь підготовки молоді є виховання свідомих, \n" +
                                       "самодостатніх, повновартістних громадян.", false, 8),
                new SingleChoiceAnswer("Важливу роль у такому всебічному вихованні відіграє неполітична й \n" +
                                       "позаконфесійна скаутська організація України «Пласт».", true, 8),
                new SingleChoiceAnswer("Похідне від «Пласт» слово «пластун» є вітповідником англійского «scout» (розвідник): \n" +
                                       "сáме так козаки називали своїх розвідників.", false, 8),
                //9
                new SingleChoiceAnswer("виконавиця", false, 9),
                new SingleChoiceAnswer("гардеробщиця", true, 9),
                new SingleChoiceAnswer("кравчиня", false, 9),
                new SingleChoiceAnswer("вихователька", false, 9),
                //10
                new SingleChoiceAnswer("винайняв квартиру в центрі", false, 10),
                new SingleChoiceAnswer("запросив до вітальні", false, 10),
                new SingleChoiceAnswer("розповів про відрядження", false, 10),
                new SingleChoiceAnswer("приїхав вісім років назад", true, 10),
                //11
                new SingleChoiceAnswer("Письменник прагнув розповісти про внутрішній світ \n" +
                                       "Івана Дідуха й чому він емігрує.", true, 11),
                new SingleChoiceAnswer("Першим розповів європейцям про Єгипет невтомний \n" +
                                       "мандрівник та історик Геродот.", false, 11),
                new SingleChoiceAnswer("Працюючи з художнім словом, Емма Андієвська виходить \n" +
                                       "за межі стандарту й буденщини.", false, 11),
                new SingleChoiceAnswer("За дорученням Київської археографічної комісії \n" +
                                       "Шевченко робив замальовки пам’яток української \n" +
                                       "історії й архітектури.", false, 11),
                new SingleChoiceAnswer("У людства завжди були й будуть питання, на які воно, \n" +
                                       "вочевидь, відповіді не знайде ніколи.", false, 11),
                //12
                new SingleChoiceAnswer("«Київська міська рада пропонуватиме обмежити використання \n" +
                                       "в магазинах поліетиленових пакетів», – повідомляє пресслужба відомства.", false, 12),
                new SingleChoiceAnswer("Посадовець зазначає: «Погоджено проєкт рішення про зменшення \n" +
                                       "використання поліетиленових пакетів у місцях продажу товарів».", false, 12),
                new SingleChoiceAnswer("«Продавці мають запропонувати альтернативу поліетиленовій продукції, \n" +
                                       "– наголошує розробник проєкту, а далі пояснює: – Це можуть бути полімерні \n" +
                                       "чи паперові екопакети, багаторазові торбинки тощо».", false, 12),
                new SingleChoiceAnswer("«Щороку в масштабах ЄС на смітники вивозять вісім мільярдів поліетиленових \n" +
                                       "упакувань, – мотивують це рішення чиновники, – тобто на одного європейця \n" +
                                       "припадає 200 таких одноразових пакетів за рік».", false, 12),
                new SingleChoiceAnswer("«Частина пакетів потрапляє до світового океану з катастрофічними наслідками \n" +
                                       "для флори та фауни» – додає автор проєкту Костянтин Яловий.", true, 12),
                //13
                new SingleChoiceAnswer("тире 1 – між підметом і присудком", false, 13),
                new SingleChoiceAnswer("коми 2, 5 – між частинами складного речення, що пов’язані підрядним зв’язком", false, 13),
                new SingleChoiceAnswer("коми 3, 4 – при вставному слові", false, 13),
                new SingleChoiceAnswer("кома 6 – при відокремленому означенні", true, 13),
                new SingleChoiceAnswer("кома 7 – між частинами складного речення, що пов’язані сурядним зв’язком", false, 13),
                //14
                new SingleChoiceAnswer("На лекціях з етики професор розповідав студентам, як тактичніше вчинити \n" +
                                        "в тій чи тій не надто приємній ситуації.", false, 14),
                new SingleChoiceAnswer("Імунологи прогнозують, що останні місяці року будуть удачними для \n" +
                                        "укріплення здоров’я.", false, 14),
                new SingleChoiceAnswer("На симпозіум поїде чисельна група науковців, які брали участь у досліді цієї теми.", false, 14),
                new SingleChoiceAnswer("На конференцію запросили різних за фахом лікарів: від хірургів до реабілітації.", false, 14),
                new SingleChoiceAnswer("У тендітних руках балерини – оберемок червоних троянд, і це найвища \n" +
                                       "оцінка її роботи в новому спектаклі.", true, 14),
                //15
                new SingleChoiceAnswer("розчленування", true, 15),
                new SingleChoiceAnswer("уподібнення", false, 15),
                new SingleChoiceAnswer("ототожнення", false, 15),
                new SingleChoiceAnswer("розуміння", false, 15),
                new SingleChoiceAnswer("нововведення", false, 15),
                //16
                new SingleChoiceAnswer("Ігором, Житомиру, кілометрів", false, 16),
                new SingleChoiceAnswer("Ігорем, Житомира, кілометри", true, 16),
                new SingleChoiceAnswer("Ігорєм, Житомиру, кілометра", false, 16),
                new SingleChoiceAnswer("Ігорьом, Житомира, кілометра", false, 16),
                new SingleChoiceAnswer("Ігорем, Житомиру, кілометри", false, 16),
                //17
                new SingleChoiceAnswer("усіх секретів, по мотивам", false, 17),
                new SingleChoiceAnswer("усіма секретами, по мотивах", false, 17),
                new SingleChoiceAnswer("усім секретам, за мотивами", false, 17),
                new SingleChoiceAnswer("усім секретам, по мотивам", false, 17),
                new SingleChoiceAnswer("усіх секретів, за мотивами", true, 17),
                //18
                new SingleChoiceAnswer("найбільш довгий, восьми метри", false, 18),
                new SingleChoiceAnswer("самий довший, восьмох метрів", false, 18),
                new SingleChoiceAnswer("найдовший, восьми метрів", true, 18),
                new SingleChoiceAnswer("самий довгий, вісьми метра", false, 18),
                new SingleChoiceAnswer("довгіший, восьмох метра", false, 18),
                //19
                new SingleChoiceAnswer("на прохання, екскурсовода", false, 19),
                new SingleChoiceAnswer("за проханням, екскурсовода", false, 19),
                new SingleChoiceAnswer("за проханням, екскурсоводу", false, 19),
                new SingleChoiceAnswer("по проханню, екскурсовода", false, 19),
                new SingleChoiceAnswer("на прохання, екскурсоводові", true, 19),
                //20
                new SingleChoiceAnswer("правила, важкими французькими звуками", false, 20),
                new SingleChoiceAnswer("правилами, важким французьким звукам", false, 20),
                new SingleChoiceAnswer("правил, важкими французькими звуками", false, 20),
                new SingleChoiceAnswer("правилами, важких французьких звуків", false, 20),
                new SingleChoiceAnswer("правила, важких французьких звуків", true, 20),
                //21
                new SingleChoiceAnswer("III, I, V, IV, II", false, 21),
                new SingleChoiceAnswer("III, V, II, IV, I", true, 21),
                new SingleChoiceAnswer("III, IV, II, V, I", false, 21),
                new SingleChoiceAnswer("III, I, IV, II, V", false, 21),
                new SingleChoiceAnswer("III, II, I, IV, V", false, 21),
                //22
                new SingleChoiceAnswer("I", false, 22),
                new SingleChoiceAnswer("II", false, 22),
                new SingleChoiceAnswer("III", false, 22),
                new SingleChoiceAnswer("IV", false, 22),
                new SingleChoiceAnswer("V", true, 22),
                //23
                new SingleChoiceAnswer("I", true, 23),
                new SingleChoiceAnswer("II", false, 23),
                new SingleChoiceAnswer("III", false, 23),
                new SingleChoiceAnswer("IV", false, 23),
                new SingleChoiceAnswer("V", false, 23),
                //24
                new SingleChoiceAnswer("I", false, 24),
                new SingleChoiceAnswer("II", false, 24),
                new SingleChoiceAnswer("III", true, 24),
                new SingleChoiceAnswer("IV", false, 24),
                new SingleChoiceAnswer("V", false, 24),
                //25
                new SingleChoiceAnswer("очисні", false, 25),
                new SingleChoiceAnswer("екологічний", false, 25),
                new SingleChoiceAnswer("далека", true, 25),
                new SingleChoiceAnswer("погане", false, 25),
                new SingleChoiceAnswer("надмірне", false, 25)
            };
            db.SingleChoiceAnswers.AddRange(singleChoiceAnswers);
            db.SaveChanges();


            //MultipleChoiceAnswer
            List<MultipleChoiceAnswer> multipleChoiceAnswers = new List<MultipleChoiceAnswer>()
            {
                new MultipleChoiceAnswer("answer1", true, 31),
                new MultipleChoiceAnswer("answer2", false, 31),
                new MultipleChoiceAnswer("answer3", true, 31),
                new MultipleChoiceAnswer("answer4", true, 31)
            };
            db.MultipleChoiceAnswers.AddRange(multipleChoiceAnswers);
            db.SaveChanges();


            //MachingAnswer
            List<MachingAnswer> machingAnswers = new List<MachingAnswer>()
            {
                //Украінська мова
                //26
                new MachingAnswer("Екскурсовод знає наше місто [...].", "як свої п’ять пальців.", 26),
                new MachingAnswer("Схожі Даринка із сестрою [...].", "як дві краплі води.", 26),
                new MachingAnswer("Раптовий приїзд гостей був [...].", "як грім з ясного неба.", 26),
                new MachingAnswer("Офіціант крутився на зміні [...].", "як білка в колесі.", 26),
                new MachingAnswer(" ", "як гриби після дощу.", 26),
                //27
                new MachingAnswer("(1)вчених", "іменник", 27),
                new MachingAnswer("(2)себе", "займенник", 27),
                new MachingAnswer("(3)долаючи", "форма дієслова (дієприслівник)", 27),
                new MachingAnswer("(4)зайвий", "прикметник", 27),
                new MachingAnswer(" ", "форма дієслова (дієприкметник)", 27),
                //28
                new MachingAnswer("тире в неповному реченні", "Зацвіла тендітно-біла конвалія в гаю, а \n" +
                                                              "сонячні кульбабки – біля ставка.", 28),
                new MachingAnswer("тире при відокремленій прикладці", "Найбільше у світі любив він осінь – \n" +
                                                                      "природи солодку зрілість.", 28),
                new MachingAnswer("тире при однорідних членах речення", "Зображення флори і фауни: квітів, \n" +
                                                                        "дерев, тварин – свідчать про обожнення \n" +
                                                                        "природи нашими предками.", 28),
                new MachingAnswer("тире між підметом і присудком", "Класична пластика, і контур строгий, \n" +
                                                                   "і логіки залізна течія – це справжньої \n" +
                                                                   "поезії дороги.", 28),
                new MachingAnswer(" ", "Дівчина внесла оберемок майористих \n" +
                                      "квітів – до цього похмура кімната враз \n" +
                                       "ожила.", 28),
                //29
                new MachingAnswer("Доволі багато людей, можливо, через \n" +
                                    "невдоволеність звичайним \n" +
                                    "спілкуванням хоче поговорити з \n" +
                                    "віртуальним співрозмовником – \n" +
                                    "чатботом.", 
                                    "Чатботи не замінять звичного людського \n" +
                                    "спілкування, адже вони не навчать \n" +
                                    "нового, не нададуть психологічної \n" +
                                    "підтримки.", 29),
                new MachingAnswer("Такі «співрозмовники» відповідають \n" +
                                    "монотонно й шаблонно заздалегідь \n" +
                                    "запрограмованим текстом.",
                                    "Репліки діалогової системи Meena \n" +
                                    "налаштовано так, що вони \n" +
                                    "максимально схожі на реальні.", 29),
                new MachingAnswer("У світі активно використовують діалогові \n" +
                                "програми задля зменшення в літніх \n" +
                                "людей почуття самотності.",
                                "Учені активно шукають способи \n" +
                                "вдосконалення таких програм, аби ті \n" +
                                "могли спілкуватися з нами так само \n" +
                                "вільно, як і люди.", 29),
                new MachingAnswer("Meena – це розроблена Google \n" +
                                "діалогова система, побудована на \n" +
                                "основі нейромережі з 2,6 млрд \n" +
                                "параметрів.",
                                "Більшість чатботів, з якими ми маємо \n" +
                                "справу, лише пропонує вибрати \n" +
                                "репліку із запрограмованих варіантів.", 29),
                new MachingAnswer(" ", "Якщо Meena або інші подібні програми \n" +
                                "такого ж рівня ввійдуть у нашу \n" +
                                "реальність, то постане питання про \n" +
                                "коректне використання їх.", 29),
                //30
                new MachingAnswer("складносурядне", "Красна пташка своїм пір’ям, а людина \n" +
                                                    "– знанням.", 30),
                new MachingAnswer("складнопідрядне", "Що в пісні співається, те в житті \n" +
                                                      "збувається.", 30),
                new MachingAnswer("безсполучникове", "Люби музику: вона облагороджує твої \n" +
                                                    "думки і почуття.", 30),
                new MachingAnswer("з різними видами зв’язку", "Небо вночі чисте, і видно, як падають \n" +
                                                              "зорі.", 30),
                new MachingAnswer(" ", "Голос у нього гучний і спочатку завжди впевнений.", 30)
            };
            db.MachingAnswers.AddRange(machingAnswers);
            db.SaveChanges();


            //OpenAnswer
            List<OpenAnswer> openAnswers = new List<OpenAnswer>()
            {
                new OpenAnswer("answer", "Answer", 32)
            };
            db.OpenAnswers.AddRange(openAnswers);
            db.SaveChanges();
            MessageBox.Show("ADD");
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (windowAdmin_TestStart == null || PresentationSource.FromVisual(windowAdmin_TestStart)==null)
            {
                windowAdmin_TestStart = new WindowAdmin_TestStart();
                windowAdmin_TestStart.Show();
                this.Close();
            }
            else windowAdmin_TestStart.Activate();
        }
    }
}
