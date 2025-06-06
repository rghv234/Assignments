using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimProcessor.Models
{
    public class Policy
    {
        public string PolicyId { get; set; }
        public InsuranceType Type { get; set; }
        public decimal MaxClaimedAmount { get; set; }
        public bool IsActive { get; set; }

    }
}
