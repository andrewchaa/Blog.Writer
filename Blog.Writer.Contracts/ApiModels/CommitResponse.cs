using System;

namespace Blog.Writer.Contracts.ApiModels
{
    public struct CommitResponse
    {
        public Commit Commit { get; set; }

    }

    public struct Commit
    {
        public Author Author { get; set; }
    }

    public struct Author
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}