using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimProcessor.Models
{
    public interface IPolicyRepository
    {
        Policy GetPolicyById(string policyId);
    }
}
