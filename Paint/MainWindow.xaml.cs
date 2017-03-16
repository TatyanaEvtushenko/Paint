﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using Paint.Factory;
using Paint.Factory.Implementations;
using Paint.Serializer;
using Paint.Serializer.Implementations;
using Paint.ShapeList.Implementations;
using Shape = Paint.Shapes.Shape;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private IShapeFactory factory;
        private Painter painter;
        private ISerializer<Painter> serializer;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private ShapeParams param;
        private Point lastPoint;

        private bool isFillPointer;
        private bool isPaintNow;
        private bool isWidthShape;
        private int lastShapeIndex;
        private int pointsCount;
       
        public MainWindow()
        {
            InitializeComponent();
            InitializeParams();
        }

        private void InitializeParams() 
        {
            isFillPointer = true;
            ComboBoxItemEllipse.IsSelected = true;
            ComboBoxItemDefault.IsSelected = true;
            factory = new EllipseFactory();
            painter = Painter.GetPainter(CanvasPaint);
            serializer = new JsonSerializer<Painter>();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            param = new ShapeParams
            {
                Fill = TextBlockFill.Foreground,
                Stroke = TextBlockContour.Foreground,
                StrokeThickness = ((Line)((ComboBoxItem)ComboBoxStrokeThickness.SelectedItem).Content).StrokeThickness
            };
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
                TextBlockFill.Foreground = param.Fill = button.Background;
            }
            else
            {
                TextBlockContour.SetValue(Grid.ColumnProperty, column);
                TextBlockContour.Foreground = param.Stroke = button.Background;
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
            isPaintNow = false;
            isWidthShape = StackPanelPoints.Visibility == Visibility.Hidden;
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
            var maxHeight = CanvasPaint.ActualHeight;
            var maxWidth = CanvasPaint.ActualWidth;

            ButtonAddShape.IsEnabled = CheckIsStrPositiveNumber(TextBoxAngle.Text) && CheckIsStrPositiveNumber(TextBoxHeight.Text)
            && CheckIsStrPositiveNumber(TextBoxWidth.Text) && CheckIsStrPositiveNumber(TextBoxX.Text) && CheckIsStrPositiveNumber(TextBoxY.Text)
            && Convert.ToDouble(TextBoxX.Text) < maxWidth && Convert.ToDouble(TextBoxY.Text) < maxHeight;

            ShowTempShape();
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

            ShowTempShape();
        }

        private void ShowTempShape()
        {
            if (!ButtonAddShape.IsEnabled || !isPaintNow)
                return;
            if (isWidthShape)
                InitializeWidthShapeParams();
            else
                InitializePointsShapeParams();
            var shape = factory.CreateShapeForDrawing(param);
            CanvasPaint.Children.Add(shape);
        }

        private TextBox CreatePointTextBox()
        {
            var textBox = new TextBox();
            textBox.TextChanged += ChangePointsTextBoxes;
            return textBox;
        }

        private bool CheckIsStrPositiveNumber(string str)
        {
            double num;
            return Double.TryParse(str, out num) && num >= 0;
        }

        private void ChangeStrokeThickness(object sender, SelectionChangedEventArgs e)
        {
            param.StrokeThickness =
                ((Line) ((ComboBoxItem) ComboBoxStrokeThickness.SelectedItem).Content).StrokeThickness;
        }

        private void AddShape(object sender, RoutedEventArgs e)
        {
            if (!ButtonAddShape.IsEnabled)
                return;
            if (isWidthShape)
                InitializeWidthShapeParams();
            else
                InitializePointsShapeParams();
            var shape = factory.Create(param);
            painter.AddNewShapeToList(shape);
            ChangeStepButtonEnableds();
            if (isWidthShape)
                CleanWidthTextBoxes();
            else
                CleanPointsTextBoxes();
        }

        private void InitializeWidthShapeParams()
        {
            param.X = Convert.ToDouble(TextBoxX.Text);
            param.Y = Convert.ToDouble(TextBoxY.Text);
            param.Width = Convert.ToDouble(TextBoxWidth.Text);
            param.Height = Convert.ToDouble(TextBoxHeight.Text);
            param.Angle = Convert.ToDouble(TextBoxAngle.Text);
        }

        private void InitializePointsShapeParams()
        {
            var textBoxesX = StackPanelX.Children.OfType<TextBox>().ToList();
            var textBoxesY = StackPanelY.Children.OfType<TextBox>().ToList();
            int count = textBoxesX.Count - 1;
            param.PointsX = new double[count];
            param.PointsY = new double[count];

            for (int i = 0; i < count; i++)
            {
                param.PointsX[i] = Convert.ToDouble(textBoxesX[i].Text);
                param.PointsY[i] = Convert.ToDouble(textBoxesY[i].Text);
            }
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
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    var newShapeList = serializer.ReadFromFile(openFileDialog.FileName);
                    if (!painter.CanClean ||
                        painter.CanClean &&
                        MessageBox.Show("The current shape list will be deleted. Do you want to continue?") ==
                        MessageBoxResult.OK)
                    ChangeShapeList(newShapeList);
                }
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

        //up
        private void BeginPaintNewShape(object sender, MouseButtonEventArgs e)
        {
            var canvas = (Canvas)sender;
            lastShapeIndex = canvas.Children.Count;
            lastPoint = e.GetPosition(canvas);  
            if (!isPaintNow)
            {
                isPaintNow = true;
                if (!isWidthShape)
                {
                    pointsCount = 1;
                    FillPointsTextBoxes(lastPoint);
                    pointsCount++;
                }
            }
            else
            {
                if (!isWidthShape)
                {
                    AddNewShape(canvas, e);
                    pointsCount++;
                }
            }
        }

        //move
        private void ChangeNewShape(object sender, MouseEventArgs e)
        {
            if (isPaintNow)
            {
                var canvas = (Canvas)sender;
                if (lastShapeIndex >= 0)
                    canvas.Children.RemoveRange(lastShapeIndex, canvas.Children.Count - lastShapeIndex);
                AddNewShape(canvas, e);
            }
        }

        private void AddNewShape(Canvas canvas, MouseEventArgs e)
        {
            var newPoint = e.GetPosition(canvas);
            if (isWidthShape)
                FillWidthTextBoxes(newPoint);
            else
                FillPointsTextBoxes(newPoint);
        }

        private void FillWidthTextBoxes(Point newPoint)
        { 
            double minX, maxX, minY, maxY;
            FindMinAndMax(lastPoint.X, newPoint.X, out minX, out maxX);
            FindMinAndMax(lastPoint.Y, newPoint.Y, out minY, out maxY);
            TextBoxWidth.Text = (maxX - minX).ToString();
            TextBoxHeight.Text = (maxY - minY).ToString();
            TextBoxX.Text = minX.ToString();
            TextBoxY.Text = minY.ToString();
            TextBoxAngle.Text = 0.ToString();
        }

        private void FindMinAndMax(double val1, double val2, out double min, out double max)
        {
            if (val1 > val2)
            {
                min = val2;
                max = val1;
            }
            else
            {
                max = val2;
                min = val1;
            }
        }

        private void FillPointsTextBoxes(Point newPoint)
        {
            if (StackPanelX.Children.Count >= pointsCount)
            {
                ((TextBox) (StackPanelX.Children[pointsCount - 1])).Text = newPoint.X.ToString();
                ((TextBox) (StackPanelY.Children[pointsCount - 1])).Text = newPoint.Y.ToString();
            }
        }

        //down
        private void EndPaintNewShape(object sender, MouseButtonEventArgs e)
        {
            if (isPaintNow && isWidthShape)
            {
                EndPaint();
            }
        }

        //right down
        private void EndPaintPointsShape(object sender, MouseButtonEventArgs e)
        {
            if (isPaintNow && !isWidthShape)
            {
                EndPaint();
            }
        }

        private void EndPaint()
        {
            if (lastShapeIndex >= 0)
                CanvasPaint.Children.RemoveRange(lastShapeIndex, CanvasPaint.Children.Count - lastShapeIndex);
            isPaintNow = false;
            AddShape(ButtonAddShape, new RoutedEventArgs());
        }
    }
}
