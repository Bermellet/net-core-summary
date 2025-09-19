using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions options) : base(options)
        {
        }

        protected UniversityDBContext()
        {
        }

        // TODO: Add DbSets (Tables)
        public DbSet<User>? Users { get; set; }
    }
}
