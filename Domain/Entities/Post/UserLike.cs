namespace Domain.Entities;

public class UserLike : BaseEntity
{
    public Post TargetPost { get; set; } = null!;
    public int TargetPostId { get; set; }
    
    public AppUser SourceUser { get; set; } = null!;
    public int SourceUserId { get; set; }
}