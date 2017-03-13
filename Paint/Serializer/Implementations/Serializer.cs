using System.IO;
using System.Runtime.Serialization.Json;

namespace Paint.Serializer.Implementations
{
    class JsonSerializer<T> : ISerializer<T>
    {
        private readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

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
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {
                serializer.WriteObject(stream, data);
            }
        }
    }
}
