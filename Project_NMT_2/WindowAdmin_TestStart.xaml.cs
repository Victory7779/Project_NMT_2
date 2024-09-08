using Project_NMT_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for WindowAdmin_TestStart.xaml
    /// </summary>
    public partial class WindowAdmin_TestStart : Window
    {
        // Window for Test Template
        private WindowCreateOrUpdateTest createOrUpdateTest;

        //For Binding ListView
        private List<ALLTest> aLLTest { get; set; }
        private List<UserPersonalInfomation> users { get; set; }
        private List<Reviews> reviews { get; set; }
        public WindowAdmin_TestStart()
        {
            InitializeComponent();
            InitializeALLTest();
            InitializeUsers();
            InitializeRewiew();
        }

        private void InitializeRewiew()
        {
            reviews = new List<Reviews>();
            reviews = ServiceDB.GetAllReviews().ToList();
            try
            {
                
                if (reviews!=null)
                {
                    reviews_ListView.ItemsSource = reviews;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeUsers()
        {
            users = new List<UserPersonalInfomation>();
            users = ServiceDB.GetAllUsers().ToList();
            try
            {
                if (users!=null)
                {
                    users_ListView.ItemsSource = users;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeALLTest()
        {
            aLLTest = new List<ALLTest>();
            aLLTest = ServiceDB.GetALLTests().ToList();
            try
            {
                if (aLLTest != null)
                {
                    test_ListView.ItemsSource = aLLTest;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
            
        }

        //USER
        //___________________________________________________________________
        private void refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            InitializeUsers();
            users_ListView.Items.Refresh();

            InitializeRewiew();
            reviews_ListView.Items.Refresh();

        }

        private void blocking_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var review = reviews_ListView.SelectedItem as Reviews;
                var user = ServiceDB.GetUserPersonal(review.id_user);
                int block = user.blocking;
                if (block<3)
                {
                    block++;
                    ServiceDB.UserPersonalBlocking(block, review.id_user);
                    ServiceDB.DeleteReviews(review.id);
                    reviews.Remove(review);
                    reviews_ListView.ItemsSource = reviews;
                    reviews_ListView.Items.Refresh();
                    MessageBox.Show($"Користувач {user.Name} заблокован. {block}");
                }
                else { MessageBox.Show($"{block}"); }

            }
            catch(Exception ex)
            {

            }
        }

        private void search_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = users_ListView.SelectedItem as UserPersonalInfomation;

                reviews = ServiceDB.GetReviewsOneUsers(user.id).ToList();
                reviews_ListView.ItemsSource = reviews;
                reviews_ListView.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        //TESTs
        //____________________________________________________________________________________
        private void createBtn_Click(object sender, RoutedEventArgs e)//Work
        {
            try
            {
                if (createOrUpdateTest == null || PresentationSource.FromVisual(createOrUpdateTest) == null)
                {
                    createOrUpdateTest = new WindowCreateOrUpdateTest(this, "create", new ALLTest());
                    createOrUpdateTest.Show();
                    this.Close();
                }
                else createOrUpdateTest.Activate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)///Пока под вопросам
        {
            try
            {
                if (createOrUpdateTest == null || PresentationSource.FromVisual(createOrUpdateTest) == null)
                {
                    createOrUpdateTest = new WindowCreateOrUpdateTest(this, "update", (test_ListView.SelectedItem as ALLTest));
                    createOrUpdateTest.Show();
                    this.Close();
                }
                else createOrUpdateTest.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e) //WORK!!!!
        {
            if (test_ListView.SelectedItem != null)
            {
                //Окно сообщения
                //-------------------------------------------------
                string messageBoxText = "Ви впевнені?";
                string caption = "Видалення теста";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                //-------------------------------------------------
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        ServiceDB.DeleteALLTest((test_ListView.SelectedItem as ALLTest).id);
                        aLLTest.Remove(test_ListView.SelectedItem as ALLTest);
                        test_ListView.Items.Refresh();
                        MessageBox.Show("Тест видалений!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else MessageBox.Show("Не вибрали тест!");
           
        }

        private void exit_Btn_Click(object sender, RoutedEventArgs e)// Work
        {
            try
            {
                var exit = MessageBox.Show("Ви дійсно хочете завершити програму?", "Вихід з програми", MessageBoxButton.YesNo);
                if (exit==MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void subjectForTest_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //WORK
        {
            string subjectText = (sender as ComboBox).SelectedItem as string;
            if(subjectText!=null || subjectText.Length==0)
            {
                InitializeALLTest();
                test_ListView.Items.Refresh();
            }
            int subjectId = ServiceDB.IdSchoolSubject(subjectText);
            var allTests_Subject = ServiceDB.GetTestWithSubject(subjectId);
            aLLTest = allTests_Subject as List<ALLTest>;
            if (aLLTest!=null)
            {
                test_ListView.ItemsSource = aLLTest;
                test_ListView.Items.Refresh();
            }
            else { MessageBox.Show($"Тести з предмету {subjectText}\n ВІДСУТНІ"); }

        }
    }
}
