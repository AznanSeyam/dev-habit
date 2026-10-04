using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dev_habit.Data;

public class DevHabitAuthDbContext : IdentityDbContext
{
    public DevHabitAuthDbContext(DbContextOptions<DevHabitAuthDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerRoleId = "123e4867-89ab-4cde-8f01-23456789abcd";
        var writerRoleId = "f47ac10b-58cc-4372-a567-0e02b2c3d479";
        var roles = new List<IdentityRole>
         {
             new IdentityRole
             {
                 Id = readerRoleId,
                 ConcurrencyStamp=readerRoleId,
                 Name="Reader",
                 NormalizedName="Reader".ToUpper()
             },
             new IdentityRole
             {  Id = writerRoleId,
                 ConcurrencyStamp=writerRoleId,
                 Name="Writer",
                 NormalizedName="Writer".ToUpper()

             }

         };
        builder.Entity<IdentityRole>().HasData(roles);
    }















}
