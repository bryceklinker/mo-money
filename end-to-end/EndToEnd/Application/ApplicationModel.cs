namespace EndToEnd.Application
{
    public class ApplicationModel
    {
        public string Name { get; }
        public string Path { get; }
        public int Port { get; }

        public ApplicationModel(string name, string path, int port)
        {
            Path = path;
            Port = port;
            Name = name;
        }
    }
}