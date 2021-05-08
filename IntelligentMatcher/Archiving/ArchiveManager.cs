using Services.Archiving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archiving
{
    public class ArchiveManager : IArchiveManager
    {
        private readonly IArchiveService _archiveService;
        private readonly IFolderHandlerService _folderHandlerService;

        public ArchiveManager(IArchiveService archiveService, IFolderHandlerService folderHandlerService)
        {
            _archiveService = archiveService;
            _folderHandlerService = folderHandlerService;
        }
        public async Task<bool> ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            try
            {
                if(startTime == null || endTime == null)
                {
                    return false;
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string logDirectory = $"{projectDirectory}\\logs";

                string[] allFiles = Directory.GetFiles(logDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validFiles = new List<string>();

                foreach (var file in allFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.Date && creationTime.Date <= endTime.Date)
                    {
                        validFiles.Add(file);
                    }
                }

                return await _archiveService.ArchiveLogFiles(validFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> ArchiveLogFilesByCategory(DateTimeOffset startTime, DateTimeOffset endTime, string category)
        {
            try
            {
                if (startTime == null || endTime == null || category == null)
                {
                    return false;
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string categoryDirectory = $"{projectDirectory}\\logs\\{category}";

                // check if the category directory exists
                // if not, return false
                if (!Directory.Exists(categoryDirectory))
                {
                    return false;
                }

                string[] allFiles = Directory.GetFiles(categoryDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validFiles = new List<string>();

                foreach (var file in allFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.Date && creationTime.Date <= endTime.Date)
                    {
                        validFiles.Add(file);
                    }
                }

                return await _archiveService.ArchiveLogFiles(validFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> DeleteArchivedFiles(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            try
            {
                if (startTime == null || endTime == null)
                {
                    return false;
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string archiveDirectory = $"{projectDirectory}\\archivedLogs";

                string[] allZipFiles = Directory.GetFiles(archiveDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validZipFiles = new List<string>();

                foreach (var file in allZipFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.Date && creationTime.Date <= endTime.Date)
                    {
                        validZipFiles.Add(file);
                    }
                }

                return await _archiveService.DeleteArchivedFiles(validZipFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> RecoverLogFiles(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            try
            {
                if (startTime == null || endTime == null)
                {
                    return false;
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string archiveDirectory = $"{projectDirectory}\\archivedLogs";

                string[] allZipFiles = Directory.GetFiles(archiveDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validZipFiles = new List<string>();

                foreach (var file in allZipFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.Date && creationTime.Date <= endTime.Date)
                    {
                        validZipFiles.Add(file);
                    }
                }

                return await _archiveService.RecoverLogFiles(validZipFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        public async Task<List<string>> GetCategories()
        {
            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string logDirectory = $"{projectDirectory}\\logs";

                var categories = await _folderHandlerService.GetSubFolders(logDirectory);

                return categories;
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }
    }
}
