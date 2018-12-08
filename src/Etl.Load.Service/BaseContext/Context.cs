using Etl.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace Etl.Load.Service.BaseContext {
    public class Context : DbContext {
        public Context (DbContextOptions<Context> options) : base (options) { }

        public DbSet<CarEntity> Cars { get; set; }
    }
}