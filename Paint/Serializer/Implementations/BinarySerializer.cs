using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Paint.Serializer.Implementations
{
    class BinarySerializer<T> : ISerializer<T>
    {
        private readonly BinaryFormatter serializer = new BinaryFormatter();

        public BinarySerializer()
        {
            //   var knownTypes = new Type[] { typeof(MatrixTransform), typeof(SolidColorBrush), typeof(Ellipse), typeof(Rectangle), typeof(RoundRectangle), typeof(Polygon), typeof(Polyline) };
     //       serializer = new DataContractJsonSerializer(typeof(T)/*, knownTypes*/);
        }

        public T ReadFromFile(string fileName)
        {
            T data;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                data = (T)serializer.Deserialize(stream);
            }
            return data;
        }

        public void SaveToFile(T data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                serializer.Serialize(stream, data);
            }
        }
    }
}
