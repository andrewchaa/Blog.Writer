using System;
using System.Linq;
using System.Text.RegularExpressions;
using FunctionalWay.Extensions;

namespace Blog.Writer.Contracts.DomainModels
{
    public class BlogPost
    {
        private readonly Regex _embedRegex 
            = new Regex("{% embed url=\"(.*)\" %}", RegexOptions.Compiled);

        private readonly Regex _gitbookRegex = 
            new Regex("(\\.\\.\\/)?.gitbook", RegexOptions.Compiled);

        public string Name { get; }
        public DateTime Date { get; }
        public string Group { get; }
        public string Contents { get; }

        public string Path => $"posts/{Date:yyyy-MM-dd}-{Name.TrimStart('.')}";
        public string Title { get; }

        public string FrontMatter =>
            $@"---
title: {Title}
date: {Date:s}
categories:
  - technical
tags:
  {Group}
---
";
        public BlogPost(string name,
            DateTime date,
            string group,
            string contents)
        {
            Name = name;
            Date = date;
            Group = string.IsNullOrEmpty(group)
                ? string.Empty
                : $"- {group}";
            Title = contents.Split(Environment.NewLine)
                .First()
                .TrimStart('#',' ')
                .TrimStart('.')
                .Replace(":", string.Empty);

            Contents = _embedRegex
                .Replace(contents, m => $"[{m.Groups[1].Value}]({m.Groups[1].Value})")
                .Split("\n")
                .Skip(3)
                .Pipe(xs => string.Join("\n", xs))
                .Pipe(x => _gitbookRegex.Replace(x, string.Empty));
        }
    }
}