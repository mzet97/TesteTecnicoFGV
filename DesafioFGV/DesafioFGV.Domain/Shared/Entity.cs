namespace DesafioFGV.Domain.Shared;

public abstract class Entity
{
    
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }

    protected Entity(Guid id,
                     DateTime createdAt,
                     DateTime? updatedAt,
                     DateTime? deletedAt,
                     bool isDeleted)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
        IsDeleted = isDeleted;
    }

    protected Entity()
    {
        
    }
}
