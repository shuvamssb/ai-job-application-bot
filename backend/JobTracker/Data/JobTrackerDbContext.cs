using Microsoft.EntityFrameworkCore;
using JobTracker.Models;

namespace JobTracker.Data
{
    public class JobTrackerDbContext : DbContext
    {
        public JobTrackerDbContext(DbContextOptions<JobTrackerDbContext> options) : base(options) { }

        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
/* What is this doing?

It tells Entity Framework Core (EF Core) how to connect to SQL Server.
It tells EF Core that JobApplication should be a table in the database.
The DbContextOptions let .NET Core inject the connection string dynamically.*/