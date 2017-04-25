namespace Paint.Serializer
{
    interface ISerializer<T>
    {
        void SaveToFile(T data, string fileName);
        T ReadFromFile(string fileName);
    }
}
