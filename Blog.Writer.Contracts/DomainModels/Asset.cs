namespace Blog.Writer.Contracts.DomainModels
{
    public class Asset
    {
        public string Name { get; }
        public string Content { get; }

        public Asset(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}