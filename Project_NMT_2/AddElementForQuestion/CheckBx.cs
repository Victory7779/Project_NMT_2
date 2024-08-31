using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Project_NMT_2.AddElementForQuestion
{
    public class CheckBx
    {
        private CheckBx() { }
        private static CheckBx checkBx;
        public StackPanel stackPanel { get; set; } = new StackPanel();
        private static readonly object _lock = new object();
        public static CheckBx GetCheckBox(StackPanel stack)
        {
            if (stack.Children.Count <= 5 || stack.Children == null)
            {
                lock (_lock)
                {
                    if (stack.Children.Count <= 5 || stack.Children == null)
                    {
                        checkBx = new CheckBx();
                        checkBx.stackPanel.Orientation = Orientation.Horizontal;
                        checkBx.stackPanel.Margin = new Thickness(0, 0, 0, 10);
                        CheckBox checkBox = new CheckBox();
                        checkBox.Margin = new Thickness(0, 0, 10, 0);
                        TextBox textBox = new TextBox();
                        textBox.Height = 20;
                        textBox.Width = 600;
                        textBox.TextWrapping = TextWrapping.Wrap;
                        checkBx.stackPanel.Children.Add(checkBox);
                        checkBx.stackPanel.Children.Add(textBox);

                    }
                }
            }
            return checkBx;
        }
    }
}
