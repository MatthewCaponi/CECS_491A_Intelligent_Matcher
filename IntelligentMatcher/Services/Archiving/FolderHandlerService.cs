using BusinessModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Services.Archiving
{
    public class FolderHandlerService : IFolderHandlerService
    {
        public async Task<List<string>> GetSubFolders(string path)
        {
            try
            {
                List<string> subFolders = new List<string>();

                if (Directory.Exists(path))
                {
                    var folders = Directory.GetDirectories(path);

                    foreach (var folder in folders)
                    {
                        DirectoryInfo directory = new DirectoryInfo(folder);
                        subFolders.Add(directory.Name);
                    }
                }

                return subFolders;
            }
            catch (PathTooLongException e)
            {
                throw new PathTooLongException(e.Message, e.InnerException);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new UnauthorizedAccessException(e.Message, e.InnerException);
            }
        }
    }
}
