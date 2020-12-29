namespace Blog.Writer.Contracts.ApiModels
{
    public class PostResponse
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ItemType Type { get; set; }
        public string Content { get; set; }
        public string Sha { get; set; }
    }
}