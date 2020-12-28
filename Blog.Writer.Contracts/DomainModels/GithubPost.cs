using System;
using System.Linq;
using System.Text.RegularExpressions;
using FunctionalWay.Extensions;

namespace Blog.Writer.Contracts.DomainModels
{
    public class GithubPost
    {
        public string Name { get; }
        public DateTime Date { get; }
        public string Contents { get; }

        public string Path => $"posts/{Date:yyyy-MM-dd}-{Name.TrimStart('.')}";
        public string Title { get; }

        public string FrontMatter =>
            $@"---
title: {Title}
date: {Date:s}
categories:
  - technical
---
";
        public GithubPost(string name,
            DateTime date,
            string contents)
        {
            Name = name;
            Date = date;
            Title = contents.Split(Environment.NewLine)
                .First()
                .TrimStart('#',' ')
                .TrimStart('.');
            var embedRegex = new Regex("{% embed url=\"(.*)\" %}", RegexOptions.Compiled);
                
            Contents =  embedRegex
                .Replace(contents, m => $"[{m.Groups[1].Value}]({m.Groups[1].Value})")
                .Split("\n")
                .Skip(3)
                .Pipe(xs => string.Join("\n", xs));
        }
    }
}