using System;
using System.IO;
using DiscManageMaster.Core;

namespace DiscManageMaster.Plugins.TXT
{
    public static class TXT
    {
        public static CommVar.PluginsType Type = CommVar.PluginsType.Text;

        public static readonly string Description = "Extract content from TXT files.";

        public static string GetText(string FileName)
        {
            if (FileName.Substring(FileName.Length - 4, 4).ToLower() != ".txt")
                throw new Exception("File type did not match this Plugins!");
            return File.OpenText(FileName).ReadLine();
        }
    }
}