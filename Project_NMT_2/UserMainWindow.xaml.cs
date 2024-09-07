using Project_NMT_2.Model;
using System;
using Project_NMT_2;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Collections.ObjectModel;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        int? ID_user = null;

        private BitmapImage _userImage;
        public BitmapImage UserImage
        {
            get { return _userImage; }
            set
            {
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private int _userAge;
        public int UserAge
        {
            get { return _userAge; }
            set
            {
                _userAge = value;
                OnPropertyChanged(nameof(UserAge));
            }
        }
        public UserMainWindow(int id_user)
        {
            InitializeComponent();
            ID_user = id_user;
            Show_NotShowTabitem();
           
            DataContext = this;  // Устанавливаем DataContext
            LoadUserData(id_user);
            ShowListofSubjects();



        }
        public ObservableCollection<string> SubjectsList { get; set; }
        public void ShowListofSubjects()
        {

            SubjectsList = new ObservableCollection<string>();
            var subjects = DBservice.GetSubjects((int)ID_user);
            bool Ukrainian = false;
            bool Mathematics = false;
            bool History = false;
            if (subjects.HasValue)
            {
                Ukrainian = subjects.Value.Ukrainian;
                Mathematics = subjects.Value.Mathematics;
                History = subjects.Value.History;


            }
            if (Ukrainian == true) SubjectsList.Add("Українська мова");
            if (Mathematics == true) SubjectsList.Add("Математика");
            if (History == true) SubjectsList.Add("Історія");
            DataContext = this;



        }
        public void Show_NotShowTabitem()
        {
            var subjects = DBservice.GetSubjects((int)ID_user);
            bool Ukrainian = false;
            bool Mathematics = false;
            bool History = false;

            if (subjects.HasValue)
            {
                Ukrainian = subjects.Value.Ukrainian;
                Mathematics = subjects.Value.Mathematics;
                History = subjects.Value.History;

               
            }

            ///UKRAINIAN
            #region UKRAINIAN
            if (Ukrainian)
            {
                if (!SubjectstabControl.Items.Contains(UkrTabItem))
                {
                    SubjectstabControl.Items.Add(UkrTabItem);
                }
            }
            else
            {
                if (SubjectstabControl.Items.Contains(UkrTabItem))
                {
                    SubjectstabControl.Items.Remove(UkrTabItem);
                }
            }
            #endregion

            ///MATH
            #region Mathematics
            if (Mathematics)
            {
                if (!SubjectstabControl.Items.Contains(MathTabItem))
                {
                    SubjectstabControl.Items.Add(MathTabItem);
                }
            }
            else
            {
                if (SubjectstabControl.Items.Contains(MathTabItem))
                {
                    SubjectstabControl.Items.Remove(MathTabItem);
                }
            }
            #endregion

            ///History
            #region History
            if (History)
            {
                if (!SubjectstabControl.Items.Contains(HistoryTabItem))
                {
                    SubjectstabControl.Items.Add(HistoryTabItem);
                }
            }
            else
            {
                if (SubjectstabControl.Items.Contains(HistoryTabItem))
                {
                    SubjectstabControl.Items.Remove(HistoryTabItem);
                }
            } 
            #endregion
        }

        private void LoadUserData(int id_user)
        {
            UserPersonalInfomation userData = DBservice.GetUserData(id_user); // Получаем данные пользователя из базы данных

            if (userData != null)
            {
                UserName = userData.Name;
                UserAge = userData.Age;

                // Преобразование фото
                if (userData.Photo != null)
                {
                    using (MemoryStream ms = new MemoryStream(userData.Photo))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = ms;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        UserImage = bitmap; // Привязываем изображение
                    }
                }
                else
                {
                    UserImage = null; // Если фотографии нет, устанавливаем null
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
