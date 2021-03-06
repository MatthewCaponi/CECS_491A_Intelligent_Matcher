﻿using AuthorizationServices;
using BusinessModels.UserAccessControl;
using Cross_Cutting_Concerns;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IScopeClaimRepository _scopeClaimRepository;
        private readonly IUserScopeClaimRepository _userScopeClaimRepository;

        public ClaimsService(IClaimRepository claimRepository, IScopeRepository scopeRepository,
            IScopeClaimRepository scopeClaimRepository, IUserScopeClaimRepository userScopeClaimRepository)
        {
            _claimRepository = claimRepository;
            _scopeRepository = scopeRepository;
            _scopeClaimRepository = scopeClaimRepository;
            _userScopeClaimRepository = userScopeClaimRepository;
        }

        public async Task<List<ClaimModel>> GetAllClaims()
        {
            try
            {
                var claims = await _claimRepository.GetAllClaims();

                List<BusinessModels.UserAccessControl.ClaimModel> claimList =
                    new List<BusinessModels.UserAccessControl.ClaimModel>();

                foreach (var claim in claims)
                {
                    claimList.Add(ModelConverterService.ConvertTo(claim, new BusinessModels.UserAccessControl.ClaimModel()));
                }

                return claimList;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("No Claims Found.", e.InnerException);
            }
        }

        public async Task<List<UserClaimModel>> GetAllUserClaims()
        {
            try
            {
                var userScopeClaims = await _userScopeClaimRepository.GetAllUserScopeClaims();

                List<BusinessModels.UserAccessControl.UserClaimModel> userClaimList =
                    new List<BusinessModels.UserAccessControl.UserClaimModel>();

                foreach (var userScopeClaimModel in userScopeClaims)
                {
                    var scopeClaimModel = await _scopeClaimRepository.GetScopeClaimById(userScopeClaimModel.Id);

                    var claimModel = await _claimRepository.GetClaimById(scopeClaimModel.ClaimId);

                    var userClaim = new BusinessModels.UserAccessControl.UserClaimModel(claimModel.Type, null);

                    userClaimList.Add(userClaim);
                }

                return userClaimList;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("No User Claims Found.", e.InnerException);
            }
        }

        public async Task<ClaimModel> GetClaim(int id)
        {
            try
            {
                var dataClaim = await _claimRepository.GetClaimById(id);
                var claim = ModelConverterService.ConvertTo(dataClaim, new BusinessModels.UserAccessControl.ClaimModel());

                return claim;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Claim Not Found.", e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Claim was Null.", e.InnerException);
            }
        }

        public async Task<int> CreateClaim(ClaimModel claimModel)
        {
            try
            {
                var dataClaim = ModelConverterService.ConvertTo(claimModel, new Models.User_Access_Control.ClaimModel());
                var claimId = await _claimRepository.CreateClaim(dataClaim);

                return claimId;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Claim could not be created.", e.InnerException);
            }
        }

        public async Task<bool> UpdateClaim(ClaimModel claimModel)
        {
            try
            {
                var dataClaim = ModelConverterService.ConvertTo(claimModel, new Models.User_Access_Control.ClaimModel());
                var changesMade = await _claimRepository.UpdateClaim(dataClaim);

                if(changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Claim could not be updated.", e.InnerException);
            }
        }

        public async Task<bool> DeleteClaim(int id)
        {
            try
            {
                var changesMade = await _claimRepository.DeleteClaim(id);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Claim could not be deleted.", e.InnerException);
            }
        }
    }
}
