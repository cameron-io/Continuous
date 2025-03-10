using Domain.Entities;
using Domain.Specifications;

namespace Domain.Services;

public class ProfileService(IUnitOfWork unitOfWork) : IProfileService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyList<Profile>> ListAllProfilesAsync()
    {
        var spec = new ProfileSpecification();
        return await _unitOfWork.Repository<Profile>().ListAsync(spec);
    }

    public async Task<Profile> GetProfileIdAsync(int Id)
    {
        var spec = new ProfileSpecification(Id);
        return await _unitOfWork.Repository<Profile>().GetEntityWithSpecAsync(spec);
    }

    public async Task<Profile> GetProfileByUserIdAsync(int appUserId)
    {
        var spec = new ProfileByUserIdSpecification(appUserId);
        return await _unitOfWork.Repository<Profile>().GetEntityWithSpecAsync(spec);
    }
    
    public async Task<Experience> GetProfileExperienceByIdAsync(int Id)
    {
        return await _unitOfWork.Repository<Experience>().GetByIdAsync(Id);
    }

    public async Task<Education> GetProfileEducationByIdAsync(int Id)
    {
        return await _unitOfWork.Repository<Education>().GetByIdAsync(Id);
    }

    public async Task<bool> UpsertAsync(Profile profile)
    {
        _unitOfWork.Repository<Profile>().Upsert(profile);
        if (await _unitOfWork.Complete()) return true;
        return false;
    }

    public async Task<bool> DeleteExperienceAsync(int Id)
    {
        var experience = await GetProfileExperienceByIdAsync(Id);
        _unitOfWork.Repository<Experience>().Delete(experience);
        if (await _unitOfWork.Complete()) return true;
        return false;
    }
    
    public async Task<bool> DeleteEducationAsync(int Id)
    {
        var Education = await GetProfileEducationByIdAsync(Id);
        _unitOfWork.Repository<Education>().Delete(Education);
        if (await _unitOfWork.Complete()) return true;
        return false;
    }

    public async Task<bool> UpsertExperienceAsync(Experience experience)
    {
        _unitOfWork.Repository<Experience>().Upsert(experience);
        if (await _unitOfWork.Complete()) return true;
        return false;
    }

    public async Task<bool> UpsertEducationAsync(Education education)
    {
        _unitOfWork.Repository<Education>().Upsert(education);
        if (await _unitOfWork.Complete()) return true;
        return false;
    }
}