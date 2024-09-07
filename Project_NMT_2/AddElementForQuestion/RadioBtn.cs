using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Project_NMT_2.AddElementForQuestion
{
    public class RadioBtn
    {
        private RadioBtn() { }
        private static RadioBtn radioBtn;
        public StackPanel stackPanel { get; set; } = new StackPanel();
        private static readonly object _lock = new object();
        public static RadioBtn GetRadioBtn(StackPanel stack)
        {
            if (stack.Children.Count <= 5 || stack.Children == null)
            {
                lock (_lock)
                {
                    if (stack.Children.Count <= 5 || stack.Children == null)
                    {
                        radioBtn = new RadioBtn();
                        radioBtn.stackPanel.Orientation = Orientation.Horizontal;
                        radioBtn.stackPanel.Margin = new Thickness(0, 0, 0, 10);
                        RadioButton radioButton = new RadioButton();
                        radioButton.GroupName = "Choice";
                        radioButton.Margin = new Thickness(0, 0, 10, 0);
                        TextBox textBox = new TextBox();
                        textBox.Height = 20;
                        textBox.Width = 600;
                        textBox.TextWrapping = TextWrapping.Wrap;
                        radioBtn.stackPanel.Children.Add(radioButton);
                        radioBtn.stackPanel.Children.Add(textBox);

                    }
                }
            }
            return radioBtn;
        }
    }
}
