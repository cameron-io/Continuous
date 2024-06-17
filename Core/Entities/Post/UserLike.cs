namespace Core.Entities;

public class UserLike : BaseEntity
{
    public Post TargetPost { get; set; } = null!;
    public int TargetPostId { get; set; }
    
    public AppUser SourceUser { get; set; } = null!;
    public string SourceUserId { get; set; }
}