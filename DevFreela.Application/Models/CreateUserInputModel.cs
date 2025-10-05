namespace DevFreela.Application.Models;

public class CreateUserInputModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}