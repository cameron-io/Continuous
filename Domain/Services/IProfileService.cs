using Domain.Entities;

namespace Domain.Services;

public interface IProfileService
{
    Task<Profile> GetProfileIdAsync(int id);
    Task<Profile> GetProfileByUserIdAsync(int appUserId);
    Task<IReadOnlyList<Profile>> ListAllProfilesAsync();
    Task<Experience> GetProfileExperienceByIdAsync(int Id);
    Task<Education> GetProfileEducationByIdAsync(int Id);
    Task<bool> Upsert(Profile profile);
    Task<bool> UpsertExperience(Experience experience);
    Task<bool> UpsertEducation(Education education);
    Task<bool> DeleteExperience(int Id);
    Task<bool> DeleteEducation(int Id);
}