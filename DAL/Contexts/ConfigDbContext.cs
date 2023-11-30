using DAL.ConfigModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public partial class ConfigDbContext : DbContext
    {
        //public ConfigDbContext()
        //{
        //}

        public ConfigDbContext(DbContextOptions<ConfigDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IPv4Location> IPv4Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=192.168.1.9;Initial Catalog=IRIS_PAYBILL_SALESCONFIG;User ID=dev;Password=Aa@123$567*");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IPv4Location>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("IPv4Location");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
