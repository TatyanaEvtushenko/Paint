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

        private void ClickPointerTextBlock(object sender, RoutedEventArgs e)
        {
            var content = ((TextBlock)sender).Name.ToString();
            if (isTempFillPointer && content != "TextBlockFill" || !isTempFillPointer && content == "TextBlockFill")
            {
                isTempFillPointer = content == "TextBlockFill";
                ChangeActivOfPointers();
            }
        }

        private void ChangeActivOfPointers()
        {
            var fontWeight = TextBlockContour.FontWeight;
            TextBlockContour.FontWeight = TextBlockFill.FontWeight;
            TextBlockFill.FontWeight = fontWeight;
        }

        private void ClickColorButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var pointer = isTempFillPointer ? TextBlockFill : TextBlockContour;
            pointer.Foreground = button.Background;
        }

        private void ChangedShape(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
        }

        private void ChangedEnabled()
        {

        }

        private void AddShape(object sender, RoutedEventArgs e)
        {

        }

        private void DrawShapes(object sender, RoutedEventArgs e)
        {

        }
    }
}
