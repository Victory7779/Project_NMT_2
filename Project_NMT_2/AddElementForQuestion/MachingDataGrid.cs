using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Project_NMT_2.AddElementForQuestion
{
   public class MachingDataGrid
    {
        private MachingDataGrid() { }
        private static MachingDataGrid maching;
        public StackPanel stackPanel { get; set; } = new StackPanel();
        private static readonly object _lock = new object();
        public static MachingDataGrid GetMachingDataGrid(StackPanel stack)
        {
            try
            {
                if (stack.Name == "answer_StackPanel") return AddNumber(stack);
                if (stack.Name == "option_StackPanel") return AddLetter(stack);
                else return null;
            }
            catch { return null; }
        }
        private static MachingDataGrid AddNumber(StackPanel stack)
        {
            if (stack.Children.Count < 5 || stack.Children == null)
            {
                lock (_lock)
                {
                    if (stack.Children.Count < 5 || stack.Children == null)
                    {
                        maching = new MachingDataGrid();
                        maching.stackPanel.Orientation = Orientation.Horizontal;
                        maching.stackPanel.Margin = new Thickness(0, 0, 0, 10);
                        TextBox textNumber = new TextBox();
                        int num = stack.Children.Count;
                        num++;
                        textNumber.Text = $"{num}";
                        textNumber.FontSize = 20;
                        textNumber.FontWeight = FontWeights.DemiBold;
                        textNumber.Background = Brushes.Transparent;
                        textNumber.BorderBrush= Brushes.Transparent;
                        textNumber.IsReadOnly = true;

                        TextBox textBox = new TextBox();
                        textBox.Height = 20;
                        textBox.Width = 300;
                        textBox.TextWrapping = TextWrapping.Wrap;
                        textBox.Margin =new Thickness(0, 0, 15, 0);
                        maching.stackPanel.Children.Add(textNumber);
                        maching.stackPanel.Children.Add(textBox);

                    }
                }
            }
            return maching;
        }

        private static MachingDataGrid AddLetter(StackPanel stack)
        {
            if (stack.Children.Count < 6 || stack.Children == null)
            {
                lock (_lock)
                {
                    if (stack.Children.Count < 6 || stack.Children == null)
                    {
                        maching = new MachingDataGrid();
                        maching.stackPanel.Orientation = Orientation.Horizontal;
                        maching.stackPanel.Margin = new Thickness(0, 0, 0, 10);


                        TextBox textBox = new TextBox();
                        textBox.Height = 20;
                        textBox.Width = 300;
                        textBox.TextWrapping = TextWrapping.Wrap;
                        textBox.Margin = new Thickness(0, 0, 15, 0);
                        maching.stackPanel.Children.Add(textBox);

                    }
                }
            }
            return maching;
        }


    }
}
