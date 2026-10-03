using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dev_habit.Data;

public class DevHabitAuthDbContext : IdentityDbContext
{
    public DevHabitAuthDbContext(DbContextOptions<DevHabitAuthDbContext> options) : base(options)
    {

    }
}
