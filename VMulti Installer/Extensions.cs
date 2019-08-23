using System.IO;
using System.IO.Compression;

namespace VMulti_Installer
{
    internal static class Extensions
    {
        public static void Extract(this FileInfo file, DirectoryInfo directory) => 
            ZipFile.ExtractToDirectory(file.FullName, directory.FullName);
    }
}
