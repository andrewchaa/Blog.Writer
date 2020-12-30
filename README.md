# Blog.Writer

I use [GitBook](https://www.gitbook.com/) to write down whatever in my mind, especially when I write code. This is a little tool to convert my GitBook posts to Jekyll-based markdown posts. I used to have Wordpress blog but much preferred github page blog. Code highlighting is much better. At the sametime, I didn't like the steps I have to take to publish on github page. I have to write it down, commit the change, and push it to Github. Also, though Markdown has really simple syntax, still it was cumbersome to write them by hands. I was looking for a nice markdown editor and the editor in Gitbook was oustanding. 

This is my attempt to use Gitbook as my blog writer. 

Usage: 

go to Cli directory
then

```bash
dotnet run --cmd write --dir <your posts directory> --imgdir <your image asset directory> 

# example
dotnet run --cmd write --dir ../../andrewchaa.github.io/_posts --imgdir ../../andrewchaa.github.io/assets  
```
