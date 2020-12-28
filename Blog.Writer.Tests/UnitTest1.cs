using System;
using Blog.Writer.Contracts.DomainModels;
using NUnit.Framework;

namespace Blog.Writer.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_remove_embed_directive()
        {
            // arrange
            var input = @"
#### Flatten queries, returning list of lists

{% embed url=""https://stackoverflow.com/questions/958949/difference-between-select-and-selectmany"" %}
";
            var contents = @"
#### Flatten queries, returning list of lists

[https://stackoverflow.com/questions/958949/difference-between-select-and-selectmany](https://stackoverflow.com/questions/958949/difference-between-select-and-selectmany)
";                
            // act
            var post = new GithubPost("name", 
                DateTime.Today,
                input);
            
            Assert.That(post.Contents, Is.EqualTo(contents));
        }
    }
}