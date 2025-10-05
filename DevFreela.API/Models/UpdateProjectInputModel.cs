using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Models
{
    public class UpdateProjectInputModel
    {
        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdFrelancer { get; set; }
        public decimal TotalCost { get; set; }
    }
}

