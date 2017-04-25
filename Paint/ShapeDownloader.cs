using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Win32;
using Paint.Serializer.Implementations;
using Paint.ShapeList.Implementations;
using Shape;
using Shape.Interfaces;
using MyShape = Shape.Shape;

namespace Paint
{
    class ShapeDownloader
    {
        private const string FOLDER_NAME = "ShapeDLL";
        private List<string> dllFiles = new List<string>();
        private FileSystemWatcher watcher = new FileSystemWatcher(FOLDER_NAME);

        private ComboBox comboBox;

        public List<IShapeFactory> Factories { get; set; } = new List<IShapeFactory>();
        public List<MyShape> Shapes { get; set; } = new List<MyShape>();

        public ShapeDownloader(ComboBox comboBox)
        {
            this.comboBox = comboBox;
            
            watcher.Created += (sender, args) => Download();
            watcher.EnableRaisingEvents = true;
        }

        private void DownloadShapeToCombobox(MyShape shape)
        {
            //  var shapeIcon = (System.Windows.Shapes.Shape)type.GetMethod("GetShapeIcon").Invoke(factory, null);
            comboBox.Dispatcher.Invoke(() =>
            {
                var item = new ComboBoxItem { Content = shape.Description };
                if (shape.Description == "UserShape")
                {
                    item.Selected += (sender, args) =>
                    {
                        var openDialog = new OpenFileDialog();
                        if (openDialog.ShowDialog() == true)
                        {
                            try
                            {
                                var serializer = new JsonSerializer<Painter>(this);
                                MainWindow.Param.ShapesList = serializer.ReadFromFile(openDialog.FileName).ShapesList;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("File format is wrong!", "Error");
                            }
                        }
                    };
                }
                comboBox.Items.Add(item);
            });
        }

        public void Download()
        {
            var files = Directory.GetFiles(FOLDER_NAME);
            foreach (var file in files)
            {
                if (new FileInfo(file).Extension == ".dll" && !dllFiles.Contains(file))
                {
                    dllFiles.Add(file);
                    var assembly = Assembly.LoadFrom(file);
                    var type = assembly.GetExportedTypes()[0];
                    var typeName = type.FullName;
                    var factory = (IShapeFactory)assembly.CreateInstance(typeName);
                    Factories.Add(factory);

                    var objects = new object[] {new ShapeParams {PointsX = new double[0], PointsY = new double[0], ShapesList = null}};
                    var shape = (MyShape)type.GetMethod("Create").Invoke(factory, objects);
                    Shapes.Add(shape);
                    DownloadShapeToCombobox(shape);
                }
            }
            comboBox.Dispatcher.Invoke(() => { comboBox.SelectedIndex = 0; });
        }

        public object InvokeMethod(int index, string methodName, object[] param)
        {
            if (index < 0)
                return null;
            var factory = Factories[index];
            var method = factory.GetType().GetMethod(methodName);
            return method.Invoke(factory, param);
        }
    }
}
