using Archiving;
using AuthorizationPolicySystem;
using AuthorizationResolutionSystem;
using BusinessModels;
using ControllerModels.ArchiveModels;
using IdentityServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Access_Information;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArchiveController : ApiBaseController
    {
        private readonly IArchiveManager _archiveManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;

        public ArchiveController(IArchiveManager archiveManager, ITokenService tokenService,
            IAuthorizationResolutionManager authorizationResolutionManager, IAuthorizationPolicyManager authorizationPolicyManager)
        {
            _archiveManager = archiveManager;
            _tokenService = tokenService;
            _authorizationResolutionManager = authorizationResolutionManager;
            _authorizationPolicyManager = authorizationPolicyManager;
        }

        [HttpPost]
        public async Task<ActionResult<ArchiveResultModel>> ArchiveLogFiles([FromBody] ArchiveModel archiveModel)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.archiving.ToString(),
                Role.admin.ToString(), true, true);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var archiveSuccess = await _archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(archiveModel.StartDate),
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
        public async Task<ActionResult<ArchiveResultModel>> ArchiveLogFilesByCategory([FromBody] ArchiveModel archiveModel)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.archiving.ToString(),
                Role.admin.ToString(), true, true);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null || archiveModel.Category == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var archiveSuccess = await _archiveManager.ArchiveLogFilesByCategory(DateTimeOffset.Parse(archiveModel.StartDate),
                    DateTimeOffset.Parse(archiveModel.EndDate), archiveModel.Category);

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

        [HttpDelete]
        public async Task<ActionResult<ArchiveResultModel>> DeleteArchivedFiles([FromBody] ArchiveModel archiveModel)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.archiving.ToString(),
                Role.admin.ToString(), true, true);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var deleteSuccess = await _archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(archiveModel.StartDate),
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
        public async Task<ActionResult<ArchiveResultModel>> RecoverLogFiles([FromBody] ArchiveModel archiveModel)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.archiving.ToString(),
                Role.admin.ToString(), true, true);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            var archiveResultModel = new ArchiveResultModel();

            try
            {
                if (archiveModel.StartDate == null || archiveModel.EndDate == null)
                {
                    archiveResultModel.Success = false;
                    archiveResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return archiveResultModel;
                }

                var recoverSuccess = await _archiveManager.RecoverLogFiles(DateTimeOffset.Parse(archiveModel.StartDate),
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

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetCategories()
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.archiving.ToString(),
                Role.admin.ToString(), true, false);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            try
            {
                return await _archiveManager.GetCategories();
            }
            catch (IOException)
            {
                return BadRequest();
            }
        }
    }
}
