using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Services.Archiving
{
    public class ArchiveService : IArchiveService
    {
        public bool ArchiveLogFiles(List<string> files)
        {
            try
            {
                string zipFile = $"{ (DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.zip";

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string targetPath = $"{projectDirectory}\\archived";

                string zipPath = Path.Combine(targetPath, zipFile);

                if (files.Count <= 0)
                {
                    return false;
                }

                //create the archive directory if it does not exist
                if (!Directory.Exists(targetPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(targetPath);
                }

                if (!File.Exists(zipPath))
                {
                    var newZip = File.Create(zipPath);
                    newZip.Close();
                }

                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        var fileName = Path.GetFileName(file);
                        string targetFile = Path.Combine(targetPath, fileName);
                        using (FileStream zipToAdd = new FileStream(zipPath, FileMode.Open))
                        {
                            using (ZipArchive archiving = new ZipArchive(zipToAdd, ZipArchiveMode.Update))
                            {
                                ZipArchiveEntry archiveEntry = archiving.CreateEntry(fileName);
                                using (StreamWriter writer = new StreamWriter(archiveEntry.Open()))
                                {
                                    writer.WriteLine(File.ReadAllText(file));
                                    File.Delete(file);
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }
    }
}
