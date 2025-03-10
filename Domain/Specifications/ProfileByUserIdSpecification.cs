using Domain.Entities;

namespace Domain.Specifications;

public class ProfileByUserIdSpecification : BaseSpecification<Profile>
{
    public ProfileByUserIdSpecification(int appUserId) : base(x => x.AppUserId == appUserId)
    {
        AddInclude(o => o.AppUser);
        AddInclude(o => o.Education);
        AddInclude(o => o.Experience);
        AddInclude(o => o.Social);
    }
}