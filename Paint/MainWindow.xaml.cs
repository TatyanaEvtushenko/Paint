using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private bool isFillPointer, isWidthDrawer;
        private WidthShapeDrawer drawerWidthShape;
        private PointsShapeDrawer drawerPointsShape;
       
        public MainWindow()
        {
            InitializeComponent();
            InitializeParams();
        }

        private void InitializeParams()
        {
            ComboBoxItemEllipse.IsSelected = true;
            isFillPointer = true;
        }

        private void ClickPointerTextBlock(object sender, RoutedEventArgs e)
        {
            var content = ((TextBlock)sender).Name.ToString();
            if (isFillPointer && content != "TextBlockFill" || !isFillPointer && content == "TextBlockFill")
            {
                isFillPointer = content == "TextBlockFill";
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
            var pointer = isFillPointer ? TextBlockFill : TextBlockContour;
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
                    isWidthDrawer = true;
                    break;
                case "ComboBoxItemPolyline":
                case "ComboBoxItemPolygon":
                    isWidthDrawer = false;
                    break;
            }
            ChangeDrawer();
        }

        private void ChangeDrawer()
        {
            if (isWidthDrawer)
            {
                ChangeStackPanelVisibility(StackPanelPosition, StackPanelPoints);
                CleanWidthTextBoxes();
            }
            else
            {
                ChangeStackPanelVisibility(StackPanelPoints, StackPanelPosition);
                CleanPointsTextBoxes();
            }
        }

        private void ChangeStackPanelVisibility(StackPanel visible, StackPanel hidden)
        {
            visible.Visibility = Visibility.Visible;
            hidden.Visibility = Visibility.Hidden;
        }

        private void CleanWidthTextBoxes()
        {
            TextBoxX.Text = TextBoxY.Text = TextBoxWidth.Text = TextBoxHeight.Text = TextBoxAngle.Text = "";
        }

        private void CleanPointsTextBoxes() { }

        private void ChangeWidthTextBoxes(object sender, TextChangedEventArgs e)
        {
            int maxHeight = (int)CanvasPaint.ActualHeight;
            int maxWidth = (int)CanvasPaint.ActualWidth;

            ButtonAddShape.IsEnabled = CheckIsStrPositiveNumber(TextBoxAngle.Text) && CheckIsStrPositiveNumber(TextBoxHeight.Text);
            //&& CheckIsStrPositiveNumber(TextBoxWidth.Text) && CheckIsStrPositiveNumber(TextBoxX.Text) && CheckIsStrPositiveNumber(TextBoxY.Text)
            //&& Convert.ToInt32(TextBoxX.Text) < maxWidth && Convert.ToInt32(TextBoxY.Text) < maxHeight;
        }

        private void ChangePointsTextBoxes(object sender, TextChangedEventArgs e)
        {
        }

        private bool CheckIsStrPositiveNumber(string str)
        {
            int num;
            return Int32.TryParse(TextBoxAngle.Text, out num) && num >= 0;
        }

        private void AddShape(object sender, RoutedEventArgs e)
        {
            var fill = TextBlockFill.Foreground;
            var stroke = TextBlockContour.Foreground;
            var strokeThickness = ((Line)((ComboBoxItem)ComboBoxStrokeThickness.SelectedItem).Content).StrokeThickness;

            var shape = isWidthDrawer ? CreateWidthShape(fill, stroke, strokeThickness) : CreatePointsShape(fill, stroke, strokeThickness);
            shape.Draw(CanvasPaint);
            //add to list
        }

        private Shape CreateWidthShape(Brush fill, Brush stroke, double strokeThickness)
        {
            double x = Convert.ToDouble(TextBoxX.Text);
            double y = Convert.ToDouble(TextBoxY.Text);
            int width = Convert.ToInt32(TextBoxWidth.Text);
            int height = Convert.ToInt32(TextBoxHeight.Text);
            int angle = Convert.ToInt32(TextBoxAngle.Text);
            return drawerWidthShape.Create(x, y, width, height, angle, fill, stroke, strokeThickness);
        }

        private Shape CreatePointsShape(Brush fill, Brush stroke, double strokeThickness)
        {
            int[] pointsX, pointsY;
            return drawerPointsShape.Create(pointsX, pointsY, fill, stroke, strokeThickness);
        }

        private void SelecteComboBoxItemEllips(object sender, RoutedEventArgs e)
        {
            drawerWidthShape = new EllipseDrawer(); 
        }

        private void SelecteComboBoxItemRectangle(object sender, RoutedEventArgs e)
        {
            drawerWidthShape = new RectangleDrawer();
        }

        private void SelecteComboBoxItemRoundRectangle(object sender, RoutedEventArgs e)
        {
            drawerWidthShape = new RoundRectangleDrawer();
        }

        private void SelecteComboBoxItemPolyline(object sender, RoutedEventArgs e)
        {
            drawerPointsShape = new PolylineDrawer();
        }

        private void SelecteComboBoxItemPolygon(object sender, RoutedEventArgs e)
        {
            drawerPointsShape = new PolygonDrawer();
        }
    }
}
