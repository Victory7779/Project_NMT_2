using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Project_NMT_2.AddElementForQuestion
{
    public class TextBx
    {
        private TextBx() { }
        private static TextBx textBx;
        public StackPanel stackPanel { get; set; } = new StackPanel();
        private static readonly object _lock = new object();
        public static TextBx GetTextBox(StackPanel stack)
        {
            if (stack.Children.Count <= 1 || stack.Children == null)
            {
                lock (_lock)
                {
                    if (stack.Children.Count <= 5 || stack.Children == null)
                    {
                        textBx = new TextBx();
                        textBx.stackPanel.Orientation = Orientation.Horizontal;
                        textBx.stackPanel.Margin = new Thickness(0, 0, 0, 10);
                        TextBox textBox = new TextBox();
                        textBox.Height = 20;
                        textBox.Width = 300;
                        textBx.stackPanel.Children.Add(textBox);

                    }
                }
            }
            return textBx;
        }
    }
}
