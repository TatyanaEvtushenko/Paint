namespace Paint.Serializer
{
    interface ISerializer<T>
    {
        void SaveToFile(T data, string fileName, ShapeDownloader shapeDownloader);
        T ReadFromFile(string fileName, ShapeDownloader shapeDownloader);
    }
}
