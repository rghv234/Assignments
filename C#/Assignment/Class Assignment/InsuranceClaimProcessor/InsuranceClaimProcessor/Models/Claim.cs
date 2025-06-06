using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimProcessor.Models
{
    public class Claim
    {
        public string ClaimId { get; set; } 
        public string PolicyId { get; set; }
        public decimal ClaimedAmount { get; set; }
        public InsuranceType Type { get; set; }
        public decimal ApprovedAmount { get; set; }
        public bool IsApproved { get; set; }
    }
}
