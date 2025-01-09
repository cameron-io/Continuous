using Domain.Entities;

namespace Domain.Services;

public interface IProfileService
{
    Task<Profile> GetProfileIdAsync(int id);
    Task<Profile> GetProfileByUserIdAsync(int appUserId);
    Task<IReadOnlyList<Profile>> ListAllProfilesAsync();
    Task<Experience> GetProfileExperienceByIdAsync(int Id);
    Task<Education> GetProfileEducationByIdAsync(int Id);
    Task<bool> UpsertAsync(Profile profile);
    Task<bool> UpsertExperienceAsync(Experience experience);
    Task<bool> UpsertEducationAsync(Education education);
    Task<bool> DeleteExperienceAsync(int Id);
    Task<bool> DeleteEducationAsync(int Id);
}