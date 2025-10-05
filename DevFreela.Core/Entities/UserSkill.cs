namespace DevFreela.Core.Entities;

public class UserSkill : BaseEntity
{
    public int IdUser { get; private set; }
    public User User { get; private set; } = null!;
    public int IdSkill { get; private set; }
    public Skill Skill { get; private set; } = null!;

    public UserSkill(int idUser, int idSkill)
    {
        IdUser = idUser;
        IdSkill = idSkill;
    }
}