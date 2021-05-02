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
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string logDirectory = $"{projectDirectory}\\logs";

            string[] allFiles = Directory.GetFiles(logDirectory, "*.*", SearchOption.AllDirectories);
            List<string> validFiles = new List<string>();

            foreach (var file in allFiles)
            {
                DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                if (creationTime >= startTime && creationTime <= endTime)
                {
                    validFiles.Add(file);
                }
            }

            return _archiveService.ArchiveLogFiles(validFiles);
        }
    }
}
