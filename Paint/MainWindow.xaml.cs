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

namespace Paint
{
    public partial class MainWindow : Window
    {
        private bool isTempFillPointer = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickPointerButton(object sender, RoutedEventArgs e)
        {
            var content = ((Button)sender).Content.ToString();
            isTempFillPointer = content == "Fill";
            ChangeActivPointerButton();
        }

        private void ChangeActivPointerButton()
        {
            var fontWeight = ButtonContour.FontWeight;
            ButtonContour.FontWeight = ButtonFill.FontWeight;
            ButtonFill.FontWeight = fontWeight;
        }

        private void ClickColorButton(object sender, RoutedEventArgs e)
        {
            var color = ((Button)sender).Background;
            if (isTempFillPointer)
                ChangePointer(color, TextBlockFillPointer);
            else
                ChangePointer(color, TextBlockContourPointer);
        }

        private void ChangePointer(Brush color, TextBlock pointer)
        {
            pointer.Foreground = color;
        }
    }
}
