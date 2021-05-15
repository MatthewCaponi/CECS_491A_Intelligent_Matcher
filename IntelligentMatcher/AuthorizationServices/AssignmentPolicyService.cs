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
using ScopeModel = BusinessModels.UserAccessControl.ScopeModel;

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

        public async Task<Result<AssignmentPolicyModel>> GetAssignmentPolicyByRole(string role, int priority)
        {
            var assignmentPolices = (await _assignmentPolicyRepository.GetAllAssignmentPolicies()).ToList();
            var roledAssignmentPolicies = assignmentPolices.Where(x => x.RequiredAccountType.ToUpper() == role.ToUpper());
            var assignmentPolicy = roledAssignmentPolicies.Where(x => (x.Priority == (priority)) ||(x.Priority) == 1).FirstOrDefault();

            var assignmentPairingPolicies = (await _assignmentPolicyPairingRepository.GetAllAssignmentPolicyPairings()).ToList();
            var assignmentPairingPolicy = assignmentPairingPolicies.Where(x => x.PolicyId == assignmentPolicy.Id);

            var blScopes = (await _scopeService.GetAllScopes()).ToList();

            var blScopePairings = new List<ScopeModel>();

            foreach(var blScope in blScopes)
            {
                foreach(var policy in assignmentPairingPolicies)
                {
                    if (blScope.Id == policy.ScopeId)
                    {
                        blScopePairings.Add(blScope);
                    }
                }
            }

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
            var assignmentPolicyId = await _assignmentPolicyRepository.CreateAssignmentPolicy(new Models.User_Access_Control.AssignmentPolicyModel()
            {
                Name = assignmentPolicyModel.Name,
                RequiredAccountType = assignmentPolicyModel.RequiredAccountType,
                Priority = assignmentPolicyModel.Priority,
                IsDefault = assignmentPolicyModel.Default
            });

            var blScopes = await _scopeService.GetAllScopes();
            foreach (var scope in assignmentPolicyModel.AssignedScopes)
            {
                var scopeId = scope.Id;
                if (blScopes.Where(x => x.Id == scope.Id).FirstOrDefault() == null)
                {
                    scopeId = await _scopeService.CreateScope(scope);
                }

                await _assignmentPolicyPairingRepository.CreateAssignmentPolicyPairing(new AssignmentPolicyPairingModel()
                {
                    PolicyId = assignmentPolicyId,
                    ScopeId = scope.Id
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
