namespace EntityConfiguration.Sample;

public sealed class Project : Entity<ProjectId>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Author Author { get; set; } = default!;
}
