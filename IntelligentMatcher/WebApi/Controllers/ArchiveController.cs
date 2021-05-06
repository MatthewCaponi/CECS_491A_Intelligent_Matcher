using Archiving;
using BusinessModels;
using ControllerModels.ArchiveModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchiveManager _archiveManager;

        public ArchiveController(IArchiveManager archiveManager)
        {
            _archiveManager = archiveManager;
        }

        [HttpPost]
        public ArchiveResultModel ArchiveLogFiles([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var archiveSuccess = _archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(archiveModel.StartDate),
                    DateTimeOffset.Parse(archiveModel.EndDate));

                archiveResultModel.Success = archiveSuccess;

                if (!archiveResultModel.Success)
                {
                    archiveResultModel.ErrorMessage = ErrorMessage.NoSuchFilesExist.ToString();
                }

                return archiveResultModel;
            }
            catch (IOException)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = "Archive Failed! Storage might not have enough memory.";

                return archiveResultModel;
            }
        }

        [HttpPost]
        public ArchiveResultModel DeleteArchivedFiles([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var deleteSuccess = _archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(archiveModel.StartDate),
                    DateTimeOffset.Parse(archiveModel.EndDate));

                archiveResultModel.Success = deleteSuccess;

                if (!archiveResultModel.Success)
                {
                    archiveResultModel.ErrorMessage = ErrorMessage.NoSuchFilesExist.ToString();
                }

                return archiveResultModel;
            }
            catch (IOException)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = "Deletion Failed! Attempt to delete a file went wrong.";

                return archiveResultModel;
            }
        }

        [HttpPost]
        public ArchiveResultModel RecoverLogFiles([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var recoverSuccess = _archiveManager.RecoverLogFiles(DateTimeOffset.Parse(archiveModel.StartDate),
                    DateTimeOffset.Parse(archiveModel.EndDate));

                archiveResultModel.Success = recoverSuccess;

                if (!archiveResultModel.Success)
                {
                    archiveResultModel.ErrorMessage = ErrorMessage.NoSuchFilesExist.ToString();
                }

                return archiveResultModel;
            }
            catch (IOException)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = "Recovery Failed! Storage might not have enough memory.";

                return archiveResultModel;
            }
        }
    }
}
