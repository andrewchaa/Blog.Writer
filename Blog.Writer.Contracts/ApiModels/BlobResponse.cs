namespace Blog.Writer.Contracts.ApiModels
{
    public struct BlobResponse
    {
        public string Sha { get; set; }
        public int Size { get; set; }
        public string Content { get; set; }
    }
}