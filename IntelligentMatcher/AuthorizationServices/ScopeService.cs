using AuthorizationServices;
using BusinessModels.UserAccessControl;
using Cross_Cutting_Concerns;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public class ScopeService : IScopeService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IScopeClaimRepository _scopeClaimRepository;
        private readonly IUserScopeClaimRepository _userScopeClaimRepository;

        public ScopeService(IClaimRepository claimRepository, IScopeRepository scopeRepository,
            IScopeClaimRepository scopeClaimRepository, IUserScopeClaimRepository userScopeClaimRepository)
        {
            _claimRepository = claimRepository;
            _scopeRepository = scopeRepository;
            _scopeClaimRepository = scopeClaimRepository;
            _userScopeClaimRepository = userScopeClaimRepository;
        }

        public async Task<List<ScopeModel>> GetAllScopes()
        {
            try
            {
                var scopes = await _scopeRepository.GetAllScopes();

                List<BusinessModels.UserAccessControl.ScopeModel> scopeList =
                    new List<BusinessModels.UserAccessControl.ScopeModel>();

                foreach (var dataScopeModel in scopes)
                {
                    var scopeModel = ModelConverterService.ConvertTo(dataScopeModel,
                        new BusinessModels.UserAccessControl.ScopeModel());

                    scopeModel.Claims = new List<BusinessModels.UserAccessControl.ClaimModel>();

                    var scopeClaims = await _scopeClaimRepository.GetAllScopeClaims();

                    foreach (var dataScopeClaimModel in scopeClaims)
                    {
                        if (scopeModel.Id == dataScopeClaimModel.ScopeId)
                        {
                            var dataClaimModel = await _claimRepository.GetClaimById(dataScopeClaimModel.ClaimId);

                            var claimModel = ModelConverterService.ConvertTo(dataClaimModel,
                                new BusinessModels.UserAccessControl.ClaimModel());

                            scopeModel.Claims.Add(claimModel);
                        }
                    }

                    scopeList.Add(scopeModel);
                }

                return scopeList;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("No Scopes Found.", e.InnerException);
            }
        }

        public async Task<List<UserClaimModel>> GetAllUserScopes()
        {
            try
            {
                var userScopeClaims = await _userScopeClaimRepository.GetAllUserUserScopeClaims();

                List<BusinessModels.UserAccessControl.UserClaimModel> userScopeList =
                    new List<BusinessModels.UserAccessControl.UserClaimModel>();

                foreach (var userScopeClaimModel in userScopeClaims)
                {
                    var scopeClaimModel = await _scopeClaimRepository.GetScopeClaimById(userScopeClaimModel.ScopeClaimId);

                    var scopeModel = await _scopeRepository.GetScopeById(scopeClaimModel.ScopeId);

                    var userScope = new BusinessModels.UserAccessControl.UserClaimModel(scopeModel.Name, null);

                    userScopeList.Add(userScope);
                }

                return userScopeList;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("No User Scopes Found.", e.InnerException);
            }
        }

        public async Task<ScopeModel> GetScope(int id)
        {
            try
            {
                var dataScope = await _scopeRepository.GetScopeById(id);
                var scope = ModelConverterService.ConvertTo(dataScope, new BusinessModels.UserAccessControl.ScopeModel());
                scope.Claims = new List<BusinessModels.UserAccessControl.ClaimModel>();

                var scopeClaims = await _scopeClaimRepository.GetAllScopeClaims();

                foreach (var dataScopeClaimModel in scopeClaims)
                {
                    if (scope.Id == dataScopeClaimModel.ScopeId)
                    {
                        var dataClaimModel = await _claimRepository.GetClaimById(dataScopeClaimModel.ClaimId);

                        var claimModel = ModelConverterService.ConvertTo(dataClaimModel,
                            new BusinessModels.UserAccessControl.ClaimModel());

                        scope.Claims.Add(claimModel);
                    }
                }

                return scope;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Scope Not Found.", e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Scope was Null.", e.InnerException);
            }
        }

        public async Task<int> CreateScope(ScopeModel scopeModel)
        {
            try
            {
                var dataScope = ModelConverterService.ConvertTo(scopeModel, new Models.User_Access_Control.ScopeModel());
                var scopeId = await _scopeRepository.CreateScope(dataScope);

                return scopeId;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Scope could not be created.", e.InnerException);
            }
        }

        public async Task<bool> UpdateScope(ScopeModel scopeModel)
        {
            try
            {
                var dataScope = ModelConverterService.ConvertTo(scopeModel, new Models.User_Access_Control.ScopeModel());
                var changesMade = await _scopeRepository.UpdateScope(dataScope);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Scope could not be updated.", e.InnerException);
            }
        }

        public async Task<bool> DeleteScope(int id)
        {
            try
            {
                var changesMade = await _scopeRepository.DeleteScope(id);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Scope could not be deleted.", e.InnerException);
            }
        }
    }
}
