using CommandLine;

namespace Blog.Writer.Cli
{
    public class Options
    {
        [Option('c', "cmd", Required = false, HelpText = "your command to run")]
        public string Command { get; set; }

        [Option('d', "dir", Required = false, HelpText = "the target directory to put your posts")]
        public string Directory { get; set; }

        [Option('i', "imgdir", Required = false, HelpText = "the path to the image directory")]
        public string ImageDirectory { get; set; }
    }
}