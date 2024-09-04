using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Project_NMT_2
{
    /// <summary>
    /// Interaction logic for UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        int? ID_user = null;
        public UserMainWindow(int id_user)
        {
            InitializeComponent();
            ID_user = id_user;
            Show_NotShowTabitem();
            
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
        
    }
}
