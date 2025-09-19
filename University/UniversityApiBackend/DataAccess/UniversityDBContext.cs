using Microsoft.EntityFrameworkCore;

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
    }
}
