using NUnit.Framework;
using Moq;
using System;
using InsuranceClaimProcessor.Models;
using InsuranceClaimProcessor.Services;
using System.Security.Cryptography.X509Certificates;
namespace InsuranceClaimProcessorTests
{
    public class Tests
    {
        [TestFixture]
        public class ClaimProcessorTests
        {
            private ClaimProcessor _claimProcessor;
            private Mock<IPolicyRepository> _policyRepositoryMock;
            private Mock<IClaimEvaluator> _claimEvaluatorMock;
            [SetUp]
            public void Setup()
            {
                _policyRepositoryMock = new Mock<IPolicyRepository>();
                _claimEvaluatorMock = new Mock<IClaimEvaluator>();
                _claimProcessor = new ClaimProcessor(_policyRepositoryMock.Object, _claimEvaluatorMock.Object);
            }
            [Test]
            public void SubmitClaim_ApprovesClaim_WhenAmountWithinLimit()
            {
                var policy = new Policy
                {
                    PolicyId = "123",
                    Type = InsuranceType.Health,
                    MaxClaimedAmount = 1000,
                    IsActive = true
                };
                var claim = new Claim
                {
                    PolicyId = "123",
                    ClaimId = "C001",
                    Type = InsuranceType.Health,
                    ClaimedAmount = 500
                };
                _policyRepositoryMock.Setup(repo => repo.GetPolicyById(claim.PolicyId)).Returns(policy);
                _claimEvaluatorMock.Setup(evaluator => evaluator.EvaluateClaim(claim, policy)).Returns(500);
                var result = _claimProcessor.SubmitClaim(claim);
                Assert.IsTrue(result.IsApproved);
                Assert.AreEqual(500, result.ApprovedAmount);
            }

            [Test]
            public void SubmitClaim_RejectsClaim_WhenAmountExceedsLimit()
            {
                var policy = new Policy
                {
                    PolicyId = "123",
                    Type = InsuranceType.Health,
                    MaxClaimedAmount = 1000,
                    IsActive = true
                };
                var claim = new Claim
                {
                    PolicyId = "123",
                    ClaimId = "C001",
                    Type = InsuranceType.Health,
                    ClaimedAmount = 1500
                };
                _policyRepositoryMock.Setup(repo => repo.GetPolicyById(claim.PolicyId)).Returns(policy);
                _claimEvaluatorMock.Setup(evaluator => evaluator.EvaluateClaim(claim, policy)).Returns(0);
                var result = _claimProcessor.SubmitClaim(claim);
                Assert.IsFalse(result.IsApproved);
                Assert.AreEqual(0, result.ApprovedAmount);
            }
            [Test]
            public void SubmitClaim_ThrowsException_WhenPolicyIsInactive()
            {
                var claim = new Claim
                {
                    PolicyId = "123",
                    ClaimId = "C001",
                    Type = InsuranceType.Health,
                    ClaimedAmount = 500
                };
                _policyRepositoryMock.Setup(repo => repo.GetPolicyById(claim.PolicyId)).Returns((Policy)null);
                Assert.Throws<InvalidOperationException>(() => _claimProcessor.SubmitClaim(claim));
            }
            [Test]
            public void SubmitClaim_ThrowsException_WhenPolicyDoesNotExist()
            {
                var claim = new Claim
                {
                    PolicyId = "123",
                    ClaimId = "C001",
                    Type = InsuranceType.Health,
                    ClaimedAmount = 500
                };
                _policyRepositoryMock.Setup(repo => repo.GetPolicyById(claim.PolicyId)).Returns((Policy)null);
                Assert.Throws<InvalidOperationException>(() => _claimProcessor.SubmitClaim(claim));
            }
        
        }
    }
}