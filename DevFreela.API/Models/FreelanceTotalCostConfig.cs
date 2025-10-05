using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Models
{
    public class FreelanceTotalCostConfig : ControllerBase
    {
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }

    }
}
