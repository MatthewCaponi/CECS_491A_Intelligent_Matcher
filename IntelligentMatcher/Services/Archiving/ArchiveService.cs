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
        private const string textExtension = ".txt";
        private const string jsonExtension = ".json";

        public bool ArchiveLogFiles(List<string> files)
        {
            try
            {
                if (files.Count <= 0)
                {
                    return false;
                }

                string zipFile = $"{ (DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.zip";

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string targetPath = $"{projectDirectory}\\archivedLogs";

                string zipPath = Path.Combine(targetPath, zipFile);

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
                try
                {
                    string currentDirectory = Environment.CurrentDirectory;
                    string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                    string targetPath = $"{projectDirectory}\\archived";

                    //create the archive directory if it does not exist
                    if (Directory.Exists(targetPath))
                    {
                        string[] allZips = Directory.GetFiles(targetPath);

                        File.Delete(allZips[0]);
                    }

                    throw new IOException(e.Message, e.InnerException);
                }
                catch
                {
                    throw new IOException(e.Message, e.InnerException);
                }
            }
        }

        public bool RecoverLogFiles(List<String> zipFiles)
        {
            try
            {
                if (zipFiles.Count <= 0)
                {
                    return false;
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string archiveDirectory = $"{projectDirectory}\\archived";
                string recoverDirectory = $"{projectDirectory}\\logs\\Recovered";

                //create the recover directory if it does not exist
                if (!Directory.Exists(recoverDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(recoverDirectory);
                }

                foreach (string file in zipFiles)
                {
                    if (File.Exists(file))
                    {
                        var fileName = Path.GetFileName(file);
                        string zipFile = Path.Combine(archiveDirectory, fileName);

                        using (ZipArchive archiving = ZipFile.OpenRead(zipFile))
                        {
                            foreach(ZipArchiveEntry log in archiving.Entries)
                            {
                                if (log.FullName.EndsWith(jsonExtension, StringComparison.OrdinalIgnoreCase))
                                {
                                    var jsonDirectory = $"{recoverDirectory}\\json";

                                    if (!Directory.Exists(jsonDirectory))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(jsonDirectory);
                                    }

                                    var jsonPath = Path.Combine(jsonDirectory, log.FullName);

                                    log.ExtractToFile(jsonPath, true);
                                }

                                if (log.FullName.EndsWith(textExtension, StringComparison.OrdinalIgnoreCase)){
                                    var textDirectory = $"{recoverDirectory}\\text";

                                    if (!Directory.Exists(textDirectory))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(textDirectory);
                                    }

                                    var textPath = Path.Combine(textDirectory, log.FullName);

                                    if(textPath.StartsWith(textDirectory, StringComparison.Ordinal))
                                    {
                                        log.ExtractToFile(textPath, true);
                                    }
                                }
                            }
                        }

                        File.Delete(file);
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
