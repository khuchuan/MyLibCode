using Microsoft.EntityFrameworkCore;



#nullable disable

namespace DAL.Contexts
{
    public partial class SalesRepContext : TemplateDbContext
    {
        public SalesRepContext(DbContextOptions<SalesRepContext> options) : base(options)
        {
        }



        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
