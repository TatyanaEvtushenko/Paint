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
        enum KindOfPointer { Fill, Stroke }

        private KindOfPointer tempKind;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeColorOfPointer(object obj)
        {

        }

        private void MovePointer(object obj)
        {

        }

        private void ClickPointerButton(object sender, RoutedEventArgs e)
        {
            var content = ((Button)sender).Content.ToString();
            if (content == "Fill")
                tempKind = KindOfPointer.Fill;
            else
                tempKind = KindOfPointer.Stroke;
        }
    }
}
