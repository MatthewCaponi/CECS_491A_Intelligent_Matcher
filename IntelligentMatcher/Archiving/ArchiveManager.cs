using Services.Archiving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Archiving
{
    public class ArchiveManager : IArchiveManager
    {
        private readonly IArchiveService _archiveService;

        public ArchiveManager(IArchiveService archiveService)
        {
            _archiveService = archiveService;
        }
        public bool ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime)
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

                return _archiveService.ArchiveLogFiles(validFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        public bool DeleteArchivedFiles(DateTimeOffset startTime, DateTimeOffset endTime)
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

                return _archiveService.DeleteArchivedFiles(validZipFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        public bool RecoverLogFiles(DateTimeOffset startTime, DateTimeOffset endTime)
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

                return _archiveService.RecoverLogFiles(validZipFiles);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }
    }
}
