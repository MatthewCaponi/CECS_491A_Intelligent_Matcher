using AuthorizationServices;
using BusinessModels;
using BusinessModels.UserAccessControl;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentPolicyModel = BusinessModels.UserAccessControl.AssignmentPolicyModel;

namespace UserAccessControlServices
{
    public class AssignmentPolicyService : IAssignmentPolicyService
    {
        private readonly IAssignmentPolicyRepository _assignmentPolicyRepository;
        private readonly IAssignmentPolicyPairingRepository _assignmentPolicyPairingRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IScopeService _scopeService;

        public AssignmentPolicyService(IAssignmentPolicyRepository assignmentPolicyRepository, IAssignmentPolicyPairingRepository assignmentPolicyPairingRepository,
            IScopeRepository scopeRepository, IScopeService scopeService)
        {
            _assignmentPolicyRepository = assignmentPolicyRepository;
            _assignmentPolicyPairingRepository = assignmentPolicyPairingRepository;
            _scopeRepository = scopeRepository;
            _scopeService = scopeService;
        }
        public List<string> ConfigureAssignmentPolicy(string accountType)
        {
            var scopes = new List<string>();
            if (accountType == "admin")
            {
                scopes.Add("user_management:write");
                scopes.Add("user_management:delete");
                scopes.Add("user_profile:write");
                scopes.Add("listing:write");
                scopes.Add("messaging.channel:read");
                scopes.Add("messaging.channel:write");
                scopes.Add("messaging.channel.owner:delete");
                scopes.Add("archiving:write");
                scopes.Add("friends_list:read");
                scopes.Add("friends_list:write");
                scopes.Add("friends_list:delete");
            }
            if (accountType == "user")
            {
                scopes.Add("user_profile:read");
                scopes.Add("user_profile.owner:write");
                scopes.Add("listing:read");
                scopes.Add("llisting.owner.write");
                scopes.Add("messaging:read");
                scopes.Add("messaging.channel:read");
                scopes.Add("messaging:write");
                scopes.Add("messaging.channel:write");
                scopes.Add("messaging.channel:delete");
                scopes.Add("friends_list:read");
                scopes.Add("friends_list:write");
                scopes.Add("friends_list:delete");
            }

            return scopes;
        }

        public async Task<Result<AssignmentPolicyModel>> GetAssignmentPolicyByRole(string role, int priority)
        {
            var assignmentPolices = (await _assignmentPolicyRepository.GetAllAssignmentPolicies()).ToList();
            var roledAssignmentPolicies = assignmentPolices.Where(x => x.RequiredAccountType == role);
            var assignmentPolicy = roledAssignmentPolicies.Where(x => (x.Priority == (priority)) ||(x.Priority) == 1).FirstOrDefault();

            var assignmentPairingPolicies = (await _assignmentPolicyPairingRepository.GetAllAssignmentPolicyPairings()).ToList();
            var assignmentPairingPolicy = assignmentPairingPolicies.Where(x => x.PolicyId == assignmentPolicy.Id);

            var blScopes = (await _scopeService.GetAllScopes()).ToList();

            var blScopePairings = blScopes.Where(x => x.Id == (assignmentPairingPolicies.Where(y => y.ScopeId == x.Id).FirstOrDefault().ScopeId)).ToList();

            var assignmentPolicyModel = new AssignmentPolicyModel()
            {
                Name = assignmentPolicy.Name,
                Default = assignmentPolicy.IsDefault,
                RequiredAccountType = role,
                AssignedScopes = blScopePairings,
                Priority = assignmentPolicy.Priority
            };

            return Result<AssignmentPolicyModel>.Success(assignmentPolicyModel);
        }

        public async Task<Result<int>> CreateAssignmentPolicy(AssignmentPolicyModel assignmentPolicyModel)
        {
            var assignmentPolicy = _assignmentPolicyRepository.CreateAssignmentPolicy(new Models.User_Access_Control.AssignmentPolicyModel()
            {
                Name = assignmentPolicyModel.Name,
                RequiredAccountType = assignmentPolicyModel.RequiredAccountType,
                Priority = assignmentPolicyModel.Priority,
                IsDefault = assignmentPolicyModel.Default
            });

            var blScopes = await _scopeService.GetAllScopes();
            var assignmentPolicyId = -1;
            foreach (var scope in assignmentPolicyModel.AssignedScopes)
            {
                if (blScopes.Where(x => x.Id == scope.Id).FirstOrDefault() == null)
                {
                    await _scopeService.CreateScope(scope);
                }

                assignmentPolicyId = await _assignmentPolicyRepository.CreateAssignmentPolicy(new Models.User_Access_Control.AssignmentPolicyModel()
                {
                    Name = assignmentPolicyModel.Name,
                    RequiredAccountType = assignmentPolicyModel.RequiredAccountType,
                    IsDefault = assignmentPolicyModel.Default,
                    Priority = assignmentPolicyModel.Priority
                });

                blScopes.ForEach(x => new AssignmentPolicyPairingModel
                {
                    PolicyId = assignmentPolicyId,
                    ScopeId = x.Id
                });

            }

            return Result<int>.Success(assignmentPolicyId);
        }

        public async Task<Result<bool>> DeleteAssignmentPolicyModel(int id)
        {
            var assignmentPolicy = await _assignmentPolicyRepository.GetAssignmentPolicyById(id);

            var assignentPolicyPairings = (await _assignmentPolicyPairingRepository.GetAllAssignmentPolicyPairings()).ToList();
            var assignmentPolicyPairings = assignentPolicyPairings.Where(x => x.PolicyId == id).ToList();

            assignentPolicyPairings.ForEach(x => _assignmentPolicyPairingRepository.DeleteAssignmentPolicyPairing(x.Id));

            return Result<bool>.Success(true);
        }
    }
}
