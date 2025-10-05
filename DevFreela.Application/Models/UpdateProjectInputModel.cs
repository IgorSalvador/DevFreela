namespace DevFreela.Application.Models
{
    public class UpdateProjectInputModel
    {
        public int IdProject { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IdFreelancer { get; set; }
        public decimal TotalCost { get; set; }
    }
}

