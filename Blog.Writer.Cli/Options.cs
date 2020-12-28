using CommandLine;

namespace Blog.Writer.Cli
{
    public class Options
    {
        [Option('c', "cmd", Required = false, HelpText = "your command to run")]
        public string Command { get; set; }
    }
}