using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;

namespace Paint
{
    static class ConfigurationSettings
    {
        private const string CONFIG_FILE_NAME = "Settings.config";

        public static void AddSettings()
        {
            var xdoc = new XDocument(
                new XElement("window",
                    new XElement("size",
                        new XElement("width", 1150),
                        new XElement("height", 750)
                    ),
                    new XElement("location",
                        new XElement("x", 20),
                        new XElement("y", 20)
                    ),
                    new XElement("canva",
                        new XElement("background", "kotiki.jpg")
                    )
                )
            );
            xdoc.Save(CONFIG_FILE_NAME);
        }

        public static void Download(MainWindow wind)
        {
            var xdoc = XDocument.Load(CONFIG_FILE_NAME);
            var window = xdoc.Element("window");
            if (window != null)
            {
                var property = window.Element("size");
                if (property != null)
                {
                    wind.Width = Convert.ToDouble(property.Element("width").Value);
                    wind.Height = Convert.ToDouble(property.Element("height").Value);
                }
                property = window.Element("location");
                if (property != null)
                {
                    wind.Left = Convert.ToDouble(property.Element("x").Value);
                    wind.Top = Convert.ToDouble(property.Element("y").Value);
                }
                var imageFileName = window.Element("canva")?.Element("background")?.Value;
                if (File.Exists(imageFileName))
                {
                    ImageSource imageSource = new BitmapImage(new Uri(imageFileName));
                    wind.CanvasPaint.Background = new ImageBrush() { ImageSource = imageSource, Stretch = Stretch.UniformToFill };
                }
            }
        }

        public static void Save(MainWindow wind)
        {
            var xdoc = XDocument.Load(CONFIG_FILE_NAME);
            var window = xdoc.Element("window");
            if (window != null)
            {
                var property = window.Element("size");
                if (property != null)
                {
                    property.SetElementValue("width", (int)wind.Width);
                    property.SetElementValue("height", (int)wind.Height);
                }
                property = window.Element("location");
                if (property != null)
                {
                    property.SetElementValue("x", (int)wind.Left);
                    property.SetElementValue("y", (int)wind.Top);
                }
                var imageSource = ((ImageBrush) wind.CanvasPaint.Background).ImageSource;
                var imageFileName = imageSource.ToString().Replace("file:///", "");
                window.Element("canva")?.SetElementValue("background", imageFileName);
            }
            xdoc.Save(CONFIG_FILE_NAME);
        }
    }
}
