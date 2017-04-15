using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Xml;

namespace Paint.Serializer.Implementations
{
    class JsonSerializer<T> : ISerializer<T>
    {
        private DataContractJsonSerializer serializer;
        private Type[] knownTypes;

        private void UpdateSerializer(ShapeDownloader shapeDownloader)
        {
            knownTypes = new Type[] { typeof(MatrixTransform), typeof(SolidColorBrush) };
            foreach (var shape in shapeDownloader.Shapes)
            {
                int last = knownTypes.Count();
                Array.Resize(ref knownTypes,  last + 1);
                knownTypes[last] = shape.GetType();
            }
            serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
        }

        private List<string> FindUnknownedTypes(string typeStr, string str)
        {
            var regex = new Regex(typeStr + "(\\w*)");
            var typesForDeleting = new List<string>();
            var matches = regex.Matches(str);
            for (int i = 0; i < matches.Count; i++)
            {
                var typeName = matches[i].Groups[1].ToString();
                if (knownTypes.FirstOrDefault(x => x.Name == typeName) == null)
                    typesForDeleting.Add(matches[i].Groups[1].ToString());
            }
            return typesForDeleting;
        }

        private string DeleteUnknownedTypes(string fileName)
        {
            var jsonStr =  File.ReadAllText(fileName);
            var typeStr = "{\"__type\":\"";
            var typesForDeleting = FindUnknownedTypes(typeStr, jsonStr);
            
            var brackets = new char[] {'{', '}'};
            int beginIndex, endIndex, bracketsCount;
            foreach (var type in typesForDeleting)
            {
                while ((beginIndex = jsonStr.IndexOf(type)) >= 0)
                {
                    beginIndex -= typeStr.Length;
                    bracketsCount = 1;
                    endIndex = beginIndex;
                    while (bracketsCount > 0)
                    {
                        endIndex = jsonStr.IndexOfAny(brackets, endIndex + 1);
                        if (jsonStr[endIndex] == '{')
                            bracketsCount++;
                        else
                            bracketsCount--;
                    }
                    jsonStr = jsonStr.Remove(beginIndex, endIndex - beginIndex + 1 + 1); //еще и запятая
                }
            }
            jsonStr = jsonStr.Substring(0, jsonStr.Length - 2) + ']' + jsonStr[jsonStr.Length - 1];

            var tempFileName = "temp";
            File.WriteAllText(tempFileName, jsonStr);
            return tempFileName;
        }

        public T ReadFromFile(string fileName, ShapeDownloader shapeDownloader)
        {
            UpdateSerializer(shapeDownloader);
            T data;
            var tempFileName = DeleteUnknownedTypes(fileName);
            using (var stream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read))
            {
                data = (T)serializer.ReadObject(stream);
            }
            File.Delete(tempFileName);
            return data;
        }

        public void SaveToFile(T data, string fileName, ShapeDownloader shapeDownloader)
        {
            UpdateSerializer(shapeDownloader);
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                serializer.WriteObject(stream, data);
            }
        }
    }
}
