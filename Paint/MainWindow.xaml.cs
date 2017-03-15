using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using Paint.Factory;
using Paint.Factory.Implementations;
using Paint.Serializer;
using Paint.Serializer.Implementations;
using Paint.ShapeList;
using Paint.ShapeList.Implementations;
using Shape = Paint.Shapes.Shape;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private bool isFillPointer;
        private IShapeFactory factory;
        private Painter painter;
        private ISerializer<Painter> serializer;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
   //     private System.Windows.Forms.ColorDialog colorDialog;
       
        public MainWindow()
        {
            InitializeComponent();
            InitializeParams();
        }

        private void InitializeParams()
        {
            isFillPointer = true;
            ComboBoxItemEllipse.IsSelected = true;
            factory = new EllipseFactory();
            painter = Painter.GetPainter(CanvasPaint);
            serializer = new JsonSerializer<Painter>();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
        }

        private void ShowError(Exception exc)
        {
            MessageBox.Show(exc.Message, "ERROR");
        }

        private void ClickPointerTextBlock(object sender, RoutedEventArgs e)
        {
            var content = ((TextBlock)sender).Name;
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
            var column = button.GetValue(Grid.ColumnProperty);
            if (isFillPointer)
            {
                TextBlockFill.SetValue(Grid.ColumnProperty, column);
                TextBlockFill.Foreground = button.Background;
            }
            else
            {
                TextBlockContour.SetValue(Grid.ColumnProperty, column);
                TextBlockContour.Foreground = button.Background;
            }
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
                    CleanWidthTextBoxes();
                    break;
                case "ComboBoxItemPolyline":
                case "ComboBoxItemPolygon":
                    ChangeStackPanelVisibility(StackPanelPoints, StackPanelPosition);
                    CleanPointsTextBoxes();
                    break;
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

        private void CleanPointsTextBoxes()
        {
            StackPanelX.Children.Clear();
            StackPanelY.Children.Clear();
            StackPanelX.Children.Add(CreatePointTextBox());
            StackPanelY.Children.Add(CreatePointTextBox());
        }

        private void ChangeWidthTextBoxes(object sender, TextChangedEventArgs e)
        {
            int maxHeight = (int)CanvasPaint.ActualHeight;
            int maxWidth = (int)CanvasPaint.ActualWidth;

            ButtonAddShape.IsEnabled = CheckIsStrPositiveNumber(TextBoxAngle.Text) && CheckIsStrPositiveNumber(TextBoxHeight.Text)
            && CheckIsStrPositiveNumber(TextBoxWidth.Text) && CheckIsStrPositiveNumber(TextBoxX.Text) && CheckIsStrPositiveNumber(TextBoxY.Text)
            && Convert.ToInt32(TextBoxX.Text) < maxWidth && Convert.ToInt32(TextBoxY.Text) < maxHeight;
        }

        private void ChangePointsTextBoxes(object sender, TextChangedEventArgs e)
        {
            var textBoxesX = StackPanelX.Children.OfType<TextBox>().ToList();
            var textBoxesY = StackPanelY.Children.OfType<TextBox>().ToList();
            bool isCorrect = true;
            int last = textBoxesX.Count - 1;

            for (int i = 0; i < last && isCorrect; i++)
                isCorrect = CheckIsStrPositiveNumber(textBoxesX[i].Text) && CheckIsStrPositiveNumber(textBoxesY[i].Text);

            if (isCorrect && CheckIsStrPositiveNumber(textBoxesX[last].Text) && CheckIsStrPositiveNumber(textBoxesY[last].Text))
            {
                if (last > 0)
                    ButtonAddShape.IsEnabled = true;
                StackPanelX.Children.Add(CreatePointTextBox());
                StackPanelY.Children.Add(CreatePointTextBox());
                ScrollViewerPoints.ScrollToEnd();
            }
            else
                ButtonAddShape.IsEnabled = textBoxesX[last].Text == "" && textBoxesY[last].Text == "";
        }

        private TextBox CreatePointTextBox()
        {
            var textBox = new TextBox();
            textBox.TextChanged += ChangePointsTextBoxes;
            return textBox;
        }

        private bool CheckIsStrPositiveNumber(string str)
        {
            int num;
            return Int32.TryParse(str, out num) && num >= 0;
        }

        private void AddShape(object sender, RoutedEventArgs e)
        {
            var shapeParams = new ShapeParams
            {
                Fill = TextBlockFill.Foreground,
                Stroke = TextBlockContour.Foreground,
                StrokeThickness = ((Line)((ComboBoxItem) ComboBoxStrokeThickness.SelectedItem).Content).StrokeThickness
            };
            var shape = StackPanelPoints.Visibility == Visibility.Hidden ? CreateWidthShape(shapeParams) : CreatePointsShape(shapeParams);
            painter.AddNewShapeToList(shape);
            ChangeStepButtonEnableds();
        }

        private Shape CreateWidthShape(ShapeParams shapeParams)
        {
            shapeParams.X = Convert.ToDouble(TextBoxX.Text);
            shapeParams.Y = Convert.ToDouble(TextBoxY.Text);
            shapeParams.Width = Convert.ToInt32(TextBoxWidth.Text);
            shapeParams.Height = Convert.ToInt32(TextBoxHeight.Text);
            shapeParams.Angle = Convert.ToInt32(TextBoxAngle.Text);
            return factory.Create(shapeParams);
        }

        private Shape CreatePointsShape(ShapeParams shapeParams)
        {
            var textBoxesX = StackPanelX.Children.OfType<TextBox>().ToList();
            var textBoxesY = StackPanelY.Children.OfType<TextBox>().ToList();
            int count = textBoxesX.Count - 1;
            shapeParams.PointsX = new int[count];
            shapeParams.PointsY = new int[count];

            for (int i = 0; i < count; i++)
            {
                shapeParams.PointsX[i] = Convert.ToInt32(textBoxesX[i].Text);
                shapeParams.PointsY[i] = Convert.ToInt32(textBoxesY[i].Text);
            }
            return factory.Create(shapeParams);
        }

        private void SelecteComboBoxItemEllips(object sender, RoutedEventArgs e)
        {
            factory = new EllipseFactory(); 
        }

        private void SelecteComboBoxItemRectangle(object sender, RoutedEventArgs e)
        {
            factory = new RectangleFactory();
        }

        private void SelecteComboBoxItemRoundRectangle(object sender, RoutedEventArgs e)
        {
            factory = new RoundRectangleFactory();
        }

        private void SelecteComboBoxItemPolyline(object sender, RoutedEventArgs e)
        {
            factory = new PolylineFactory();
        }

        private void SelecteComboBoxItemPolygon(object sender, RoutedEventArgs e)
        {
            factory = new PolygonFactory();
        }

        private void GoToForwardStep(object sender, RoutedEventArgs e)
        {
            painter.GoToForwardStep();
            ChangeStepButtonEnableds();
        }

        private void GoToBackStep(object sender, RoutedEventArgs e)
        {
            painter.GoToBackStep();
            ChangeStepButtonEnableds();
        }

        private void CleanAll(object sender, RoutedEventArgs e)
        {
            painter.CleanAll();
            ChangeStepButtonEnableds();
        }

        private void ChangeStepButtonEnableds()
        {
            ButtonClean.IsEnabled = ButtonSave.IsEnabled = painter.CanClean;
            ButtonForward.IsEnabled = painter.CanGoToForwardStep;
            ButtonBack.IsEnabled = painter.CanGoToBackStep;
            ListBoxShapes.ItemsSource = painter.ShapesList.ToList();
        }

        private void OpenShapeList(object sender, RoutedEventArgs e)
        {
                if (openFileDialog.ShowDialog() == true)
                {
                    var newShapeList = serializer.ReadFromFile(openFileDialog.FileName);
                    if (!painter.CanClean ||
                        painter.CanClean &&
                        MessageBox.Show("The current shape list will be deleted. Do you want to continue?") ==
                        MessageBoxResult.Yes)
                    ChangeShapeList(newShapeList);
                }
            try
            {
            }
            catch
            {
                ShowError(new Exception("File format is wrong."));
            }
        }

        private void ChangeShapeList(Painter newShapeList)
        {
            painter.CleanAll();
            painter.ShapesList = newShapeList.ShapesList;
            painter.DrawAll();
            ChangeStepButtonEnableds();
        }

        private void SaveShapeList(object sender, RoutedEventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    serializer.SaveToFile(painter, saveFileDialog.FileName);
                }
            }
            catch(Exception exc)
            {
                ShowError(exc);
            }
        }
    }
}
