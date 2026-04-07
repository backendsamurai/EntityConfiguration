namespace EntityConfiguration.Sample;

public abstract class Entity<TId> where TId: Id
{
    public TId Id { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
