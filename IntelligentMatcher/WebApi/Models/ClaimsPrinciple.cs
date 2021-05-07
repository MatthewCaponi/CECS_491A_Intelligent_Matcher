using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ClaimsPrinciple
    {
        public int Id { get; set; }
        public List<UserClaimModel> Claims { get; set; }
    }
}
