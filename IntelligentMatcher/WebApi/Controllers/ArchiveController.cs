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
        public async Task<ActionResult<ArchiveResultModel>> ArchiveLogFiles([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            if (archiveModel == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            if (archiveModel.StartDate == null || archiveModel.EndDate == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            var archiveResult = await _archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(archiveModel.StartDate),
                DateTimeOffset.Parse(archiveModel.EndDate));

            archiveResultModel.Success = archiveResult.WasSuccessful;

            if (!archiveResult.WasSuccessful)
            {
                archiveResultModel.ErrorMessage = archiveResult.ErrorMessage.ToString();
            }

            return Ok(archiveResultModel);
        }

        [HttpPost]
        public async Task<ActionResult<ArchiveResultModel>> ArchiveLogFilesByCategory([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            if (archiveModel == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            if (archiveModel.StartDate == null || archiveModel.EndDate == null || archiveModel.Category == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            var archiveResult = await _archiveManager.ArchiveLogFilesByCategory(DateTimeOffset.Parse(archiveModel.StartDate),
                DateTimeOffset.Parse(archiveModel.EndDate), archiveModel.Category);

            archiveResultModel.Success = archiveResult.WasSuccessful;

            if (!archiveResult.WasSuccessful)
            {
                archiveResultModel.ErrorMessage = archiveResult.ErrorMessage.ToString();

                return BadRequest(archiveResultModel);
            }

            return Ok(archiveResultModel);
        }

        [HttpDelete]
        public async Task<ActionResult<ArchiveResultModel>> DeleteArchivedFiles([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            if (archiveModel == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            if (archiveModel.StartDate == null || archiveModel.EndDate == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            var deleteResult = await _archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(archiveModel.StartDate),
                DateTimeOffset.Parse(archiveModel.EndDate));

            archiveResultModel.Success = deleteResult.WasSuccessful;

            if (!deleteResult.WasSuccessful)
            {
                archiveResultModel.ErrorMessage = deleteResult.ErrorMessage.ToString();

                return BadRequest(archiveResultModel);
            }

            return Ok(archiveResultModel);
        }

        [HttpPost]
        public async Task<ActionResult<ArchiveResultModel>> RecoverLogFiles([FromBody] ArchiveModel archiveModel)
        {
            var archiveResultModel = new ArchiveResultModel();

            if (archiveModel == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            if (archiveModel.StartDate == null || archiveModel.EndDate == null)
            {
                archiveResultModel.Success = false;
                archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return BadRequest(archiveResultModel);
            }

            var recoverResult = await _archiveManager.RecoverLogFiles(DateTimeOffset.Parse(archiveModel.StartDate),
                DateTimeOffset.Parse(archiveModel.EndDate));

            archiveResultModel.Success = recoverResult.WasSuccessful;

            if (!recoverResult.WasSuccessful)
            {
                archiveResultModel.ErrorMessage = recoverResult.ErrorMessage.ToString();

                return BadRequest(archiveResultModel);
            }

            return Ok(archiveResultModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetCategories()
        {
            var categories = await _archiveManager.GetCategories();

            return Ok(categories.SuccessValue);
        }
    }
}
