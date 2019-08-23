using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VMulti_Installer
{
    internal class ArchiveTool : IDisposable
    {
        public ArchiveTool()
        {

        }

        private DirectoryInfo CurrentDir => new DirectoryInfo(Directory.GetCurrentDirectory());
        private List<FileInfo> TemporaryFiles { set; get; } = new List<FileInfo>();
        private DirectoryInfo TemporaryDirectory { set; get; }

        /// <summary>
        /// Extracts the archive to a temporary location.
        /// </summary>
        /// <param name="archiveName"></param>
        /// <returns>Directory in which the files are extracted to</returns>
        public DirectoryInfo ExtractArchive(string archiveName)
        {
            if (TemporaryDirectory == null)
                TemporaryDirectory = CurrentDir.CreateSubdirectory("temp");

            var archiveData = GetArchiveResource(archiveName);
            var archive = new FileInfo(TemporaryDirectory + "arch.zip");
            WriteByteStreamToFile(archiveData, archive);
            //TemporaryFiles.Add(archive);

            archive.Extract(TemporaryDirectory);
            TemporaryFiles.AddRange(TemporaryDirectory.GetFiles());

            return TemporaryDirectory;
        }

        private Stream GetArchiveResource(string archiveName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("VMulti_Installer.Archives." + archiveName);
        }

        private void WriteByteStreamToFile(Stream stream, FileInfo file)
        {
            if (file.Exists)
                file.Delete();
            using (var outputStream = file.Create())
            {
                for (int i = 0; i < stream.Length; i++)
                    outputStream.WriteByte((byte)stream.ReadByte());
            }
        }

        public void Dispose()
        {
            foreach (var file in TemporaryFiles)
                if (file.Exists)
                    file.Delete();
            TemporaryDirectory.Delete();
        }
    }
}
