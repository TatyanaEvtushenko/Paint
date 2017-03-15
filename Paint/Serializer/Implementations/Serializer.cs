using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Media;
using Paint.Shapes.PointShapes.Implementations;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Serializer.Implementations
{
    class JsonSerializer<T> : ISerializer<T>
    {
        private readonly DataContractJsonSerializer serializer;

        public JsonSerializer()
        {
            var knownTypes = new Type[] { typeof(MatrixTransform), typeof(SolidColorBrush), typeof(Ellipse), typeof(Rectangle), typeof(RoundRectangle), typeof(Polygon), typeof(Polyline) };
            serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
        }

        public T ReadFromFile(string fileName)
        {
            T data;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                data = (T)serializer.ReadObject(stream);
            }
            return data;
        }

        public void SaveToFile(T data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                serializer.WriteObject(stream, data);
            }
        }
    }
}
