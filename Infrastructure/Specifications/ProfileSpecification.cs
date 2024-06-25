using Domain.Entities;

namespace Infrastructure.Specifications
{
    public class ProfileSpecification : BaseSpecification<Profile>
    {
        public ProfileSpecification() : base()
        {
            AddInclude(o => o.AppUser);
            AddInclude(o => o.Education);
            AddInclude(o => o.Experience);
            AddInclude(o => o.Social);
        }
        public ProfileSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(o => o.AppUser);
            AddInclude(o => o.Education);
            AddInclude(o => o.Experience);
            AddInclude(o => o.Social);
        }
    }
}