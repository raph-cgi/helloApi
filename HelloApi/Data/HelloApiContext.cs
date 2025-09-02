using Microsoft.EntityFrameworkCore;
using HelloApi.Entities;

namespace HelloApi.Data
{
    public class HelloApiContext : DbContext
    {
        public HelloApiContext(DbContextOptions<HelloApiContext> options) : base(options)
        {
        }

        public DbSet<TPersonEntity> TPersons { get; set; }
    }
}