using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data;
using TestEntityFw.Data.Entities;

namespace TestEntityFw.Data.DbContext;

public class BlogDBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(b => b.CommandTimeout(120));
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //OnBeforeSaving();
        try
        {
            Console.WriteLine(ChangeTracker.DebugView.LongView);
            return await base.SaveChangesAsync(cancellationToken);
        }
        //ref: https://learn.microsoft.com/en-us/ef/core/saving/concurrency?tabs=data-annotations#resolving-concurrency-conflicts
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var databaseValues = await entry.GetDatabaseValuesAsync(cancellationToken);
                if (databaseValues == null)
                {
                    throw;
                }
                // Refresh the original values to bypass next concurrency check
                entry.OriginalValues.SetValues(databaseValues);
            }

            Console.WriteLine(ChangeTracker.DebugView.LongView);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }



}
