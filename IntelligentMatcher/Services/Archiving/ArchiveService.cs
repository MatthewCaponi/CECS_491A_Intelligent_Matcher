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
                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string targetPath = $"{projectDirectory}\\archived";

                if (files.Count <= 0)
                {
                    return false;
                }

                //create the archive directory if it does not exist
                if (!Directory.Exists(targetPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(targetPath);
                }

                foreach (string file in files)
                {
                    var fileName = Path.GetFileName(file);
                    string targetFile = Path.Combine(targetPath, fileName);
                    if (File.Exists(targetFile))
                    {
                        File.Delete(targetFile);
                    }
                    File.Move(file, targetFile);
                    //File.Delete(file);
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
