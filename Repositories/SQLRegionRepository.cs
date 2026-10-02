using System;
using dev_habit.Data;
using dev_habit.Models;
using Microsoft.EntityFrameworkCore;

namespace dev_habit.Repositories;

public class SQLRegionRepository : IRegionRepository
{
    private readonly DevHabitDbContext _devHabitDbContext;

    public SQLRegionRepository(DevHabitDbContext devHabitDbContext)
    {
        _devHabitDbContext = devHabitDbContext;
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _devHabitDbContext.Regions.AddAsync(region);
        await _devHabitDbContext.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var deleteDomain = await _devHabitDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (deleteDomain == null)
        {
            return null;
        }
        _devHabitDbContext.Regions.Remove(deleteDomain);
        await _devHabitDbContext.SaveChangesAsync();
        return deleteDomain;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await _devHabitDbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        return await _devHabitDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existregionDomain = await _devHabitDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existregionDomain == null)
        {
            return null;
        }

        existregionDomain.Code = region.Code;
        existregionDomain.Name = region.Name;
        existregionDomain.RegionImgURL = region.RegionImgURL;

        await _devHabitDbContext.SaveChangesAsync();
        return existregionDomain;
    }
}
