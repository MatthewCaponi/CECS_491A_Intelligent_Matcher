using BusinessModels;
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
        public async Task<Result<bool>> ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            try
            {
                if(startTime == null || endTime == null)
                {
                    return Result<bool>.Failure(ErrorMessage.Null);
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string logDirectory = $"{projectDirectory}\\logs";

                if (!Directory.Exists(logDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(logDirectory);
                }

                string[] allFiles = Directory.GetFiles(logDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validFiles = new List<string>();

                foreach (var file in allFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.ToUniversalTime().Date && creationTime.Date <= endTime.ToUniversalTime().Date)
                    {
                        validFiles.Add(file);
                    }
                }

                var isSuccessful = await _archiveService.ArchiveLogFiles(validFiles);

                if (isSuccessful)
                {
                    return Result<bool>.Success(isSuccessful);
                }
                else
                {
                    return Result<bool>.Failure(ErrorMessage.NoSuchFilesExist);
                }
            }
            catch (NotSupportedException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
            catch (PathTooLongException)
            {
                return Result<bool>.Failure(ErrorMessage.FolderPathTooLong);
            }
            catch (IOException)
            {
                return Result<bool>.Failure(ErrorMessage.FileError);
            }
            catch (UnauthorizedAccessException)
            {
                return Result<bool>.Failure(ErrorMessage.Forbidden);
            }
            catch (InvalidDataException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
        }

        public async Task<Result<bool>> ArchiveLogFilesByCategory(DateTimeOffset startTime, DateTimeOffset endTime, string category)
        {
            try
            {
                if (startTime == null || endTime == null || category == null)
                {
                    return Result<bool>.Failure(ErrorMessage.Null);
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string categoryDirectory = $"{projectDirectory}\\logs\\{category}";

                // check if the category directory exists
                // if not, return false
                if (!Directory.Exists(categoryDirectory))
                {
                    return Result<bool>.Failure(ErrorMessage.NoSuchFilesExist);
                }

                string[] allFiles = Directory.GetFiles(categoryDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validFiles = new List<string>();

                foreach (var file in allFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.ToUniversalTime().Date && creationTime.Date <= endTime.ToUniversalTime().Date)
                    {
                        validFiles.Add(file);
                    }
                }

                var isSuccessful = await _archiveService.ArchiveLogFiles(validFiles);

                if (isSuccessful)
                {
                    return Result<bool>.Success(isSuccessful);
                }
                else
                {
                    return Result<bool>.Failure(ErrorMessage.NoSuchFilesExist);
                }
            }
            catch (NotSupportedException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
            catch (PathTooLongException)
            {
                return Result<bool>.Failure(ErrorMessage.FolderPathTooLong);
            }
            catch (IOException)
            {
                return Result<bool>.Failure(ErrorMessage.FileError);
            }
            catch (UnauthorizedAccessException)
            {
                return Result<bool>.Failure(ErrorMessage.Forbidden);
            }
            catch (InvalidDataException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
        }

        public async Task<Result<bool>> DeleteArchivedFiles(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            try
            {
                if (startTime == null || endTime == null)
                {
                    return Result<bool>.Failure(ErrorMessage.Null);
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string archiveDirectory = $"{projectDirectory}\\archivedLogs";

                if (!Directory.Exists(archiveDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(archiveDirectory);
                }

                string[] allZipFiles = Directory.GetFiles(archiveDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validZipFiles = new List<string>();

                foreach (var file in allZipFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.ToUniversalTime().Date && creationTime.Date <= endTime.ToUniversalTime().Date)
                    {
                        validZipFiles.Add(file);
                    }
                }

                var isSuccessful = await _archiveService.DeleteArchivedFiles(validZipFiles);

                if (isSuccessful)
                {
                    return Result<bool>.Success(isSuccessful);
                }
                else
                {
                    return Result<bool>.Failure(ErrorMessage.NoSuchFilesExist);
                }
            }
            catch (NotSupportedException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
            catch (PathTooLongException)
            {
                return Result<bool>.Failure(ErrorMessage.FolderPathTooLong);
            }
            catch (IOException)
            {
                return Result<bool>.Failure(ErrorMessage.FileError);
            }
            catch (UnauthorizedAccessException)
            {
                return Result<bool>.Failure(ErrorMessage.Forbidden);
            }
        }

        public async Task<Result<bool>> RecoverLogFiles(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            try
            {
                if (startTime == null || endTime == null)
                {
                    return Result<bool>.Failure(ErrorMessage.Null);
                }

                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string archiveDirectory = $"{projectDirectory}\\archivedLogs";

                if (!Directory.Exists(archiveDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(archiveDirectory);
                }

                string[] allZipFiles = Directory.GetFiles(archiveDirectory, "*.*", SearchOption.AllDirectories);
                List<string> validZipFiles = new List<string>();

                foreach (var file in allZipFiles)
                {
                    DateTimeOffset creationTime = File.GetCreationTimeUtc(file);
                    if (creationTime.Date >= startTime.ToUniversalTime().Date && creationTime.Date <= endTime.ToUniversalTime().Date)
                    {
                        validZipFiles.Add(file);
                    }
                }

                var isSuccessful = await _archiveService.RecoverLogFiles(validZipFiles);

                if (isSuccessful)
                {
                    return Result<bool>.Success(isSuccessful);
                }
                else
                {
                    return Result<bool>.Failure(ErrorMessage.NoSuchFilesExist);
                }
            }
            catch (NotSupportedException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
            catch (PathTooLongException)
            {
                return Result<bool>.Failure(ErrorMessage.FolderPathTooLong);
            }
            catch (IOException)
            {
                return Result<bool>.Failure(ErrorMessage.FileError);
            }
            catch (UnauthorizedAccessException)
            {
                return Result<bool>.Failure(ErrorMessage.Forbidden);
            }
            catch (InvalidDataException)
            {
                return Result<bool>.Failure(ErrorMessage.DataNotSupported);
            }
        }

        public async Task<Result<List<string>>> GetCategories()
        {
            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(currentDirectory).FullName;
                string logDirectory = $"{projectDirectory}\\logs";

                if (!Directory.Exists(logDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(logDirectory);
                }

                var categories = await _folderHandlerService.GetSubFolders(logDirectory);

                return Result<List<string>>.Success(categories);
            }
            catch (PathTooLongException)
            {
                return Result<List<string>>.Success(new List<string>());
            }
            catch (IOException)
            {
                return Result<List<string>>.Success(new List<string>());
            }
            catch (UnauthorizedAccessException)
            {
                return Result<List<string>>.Success(new List<string>());
            }
        }
    }
}
