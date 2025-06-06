using System;
using InsuranceClaimProcessor.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimProcessor.Services
{
    public class ClaimProcessor
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IClaimEvaluator _claimEvaluator;
        public ClaimProcessor(IPolicyRepository policyRepository, IClaimEvaluator claimEvaluator)
        {
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
            _claimEvaluator = claimEvaluator ?? throw new ArgumentNullException(nameof(claimEvaluator));
        }
        public Claim SubmitClaim(Claim claim)
        {
            if (claim == null) throw new ArgumentNullException(nameof(claim));
            if (string.IsNullOrEmpty(claim.PolicyId)) throw new ArgumentException("PolicyId cannot be null or empty.", nameof(claim.PolicyId));
            
            var policy = _policyRepository.GetPolicyById(claim.PolicyId);
            if (policy == null || !policy.IsActive)
            {
                throw new InvalidOperationException($"Policy with ID {claim.PolicyId} is not active or does not exist.");
            }
            claim.ApprovedAmount = _claimEvaluator.EvaluateClaim(claim, policy);
            claim.IsApproved = claim.ApprovedAmount > 0;
            
            return claim;
        }
    }
}
