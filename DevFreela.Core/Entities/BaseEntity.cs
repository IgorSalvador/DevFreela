namespace DevFreela.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted{ get; private set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
        }

        public void SetAsDeleted() => IsDeleted = true;
    }
}
