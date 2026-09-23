using System;
using dev_habit.Models;
using Microsoft.EntityFrameworkCore;

namespace dev_habit.Data;
public class DevHabitDbContext : DbContext
{
    public DevHabitDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}
