using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimProcessor.Models
{
    public interface IClaimEvaluator
    {
        decimal EvaluateClaim(Claim claim, Policy policy);  
    }
}
