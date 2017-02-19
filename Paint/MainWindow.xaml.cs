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
        private ShapeDrawer drawer = new EllipseDrawer();

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

        private void ChangeShape(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var item = (ComboBoxItem)comboBox.SelectedItem;
            switch (item.Name)
            {
                case "ComboBoxItemRectangle":
                case "ComboBoxItemRoundRectangle":
                case "ComboBoxItemEllipse":
                    ChangeStackPanelVisibility(StackPanelPosition, StackPanelPoints);
                    CleanShapeTextBoxes();
                    break;
                case "ComboBoxItemPolyline":
                case "ComboBoxItemPolygon":
                    ChangeStackPanelVisibility(StackPanelPoints, StackPanelPosition);
                    break;
            }
        }

        private void ChangeStackPanelVisibility(StackPanel visible, StackPanel hidden)
        {
            visible.Visibility = Visibility.Visible;
            hidden.Visibility = Visibility.Hidden;
        }

        private void CleanShapeTextBoxes()
        {
            TextBoxX.Text = TextBoxY.Text = TextBoxWidth.Text = TextBoxHeight.Text = TextBoxAngle.Text = "";
        }

        private void ChangePositionText(object sender, TextChangedEventArgs e)
        {
            int maxHeight = (int)CanvasPaint.ActualHeight;
            int maxWidth = (int)CanvasPaint.ActualWidth;

            ButtonAddShape.IsEnabled = CheckIsStrPositiveNumber(TextBoxAngle.Text) && CheckIsStrPositiveNumber(TextBoxHeight.Text)
                && CheckIsStrPositiveNumber(TextBoxWidth.Text) && CheckIsStrPositiveNumber(TextBoxX.Text) && CheckIsStrPositiveNumber(TextBoxY.Text)
                && Convert.ToInt32(TextBoxX.Text) < maxWidth && Convert.ToInt32(TextBoxY.Text) < maxHeight;

        }

        private bool CheckIsStrPositiveNumber(string str)
        {
            int num;
            return Int32.TryParse(TextBoxAngle.Text, out num) && num >= 0;
        }

        private void AddShape(object sender, RoutedEventArgs e)
        {
            drawer.Draw();
        }

        private void SelecteComboBoxItemEllips(object sender, RoutedEventArgs e)
        {
            drawer = new EllipseDrawer(); 
        }

        //private void SelecteComboBoxItemRectangle(object sender, RoutedEventArgs e)
        //{
        //    drawer = new RectangleDrawer();
        //}
        //private void SelecteComboBoxItemRoundRectangle(object sender, RoutedEventArgs e)
        //{
        //    drawer = new RoundRectangleDrawer();
        //}
        //private void SelecteComboBoxItemPolyline(object sender, RoutedEventArgs e)
        //{
        //    drawer = new PolylineDrawer();
        //}
        //private void SelecteComboBoxItemPolygon(object sender, RoutedEventArgs e)
        //{
        //    drawer = new PolygonDrawer();
        //}
    }
}
