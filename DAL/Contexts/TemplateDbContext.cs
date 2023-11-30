using DAL.SalesModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public partial class TemplateDbContext : DbContext
    {
        public TemplateDbContext(DbContextOptions<SalesContext> options)
        : base(options)
        {
        }
        public TemplateDbContext(DbContextOptions<SalesRepContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PromotionCampaign> PromotionCampaigns { get; set; }
        public virtual DbSet<PromotionDetail> PromotionDetails { get; set; }
        public virtual DbSet<RefundOrder> RefundOrders { get; set; }
        public virtual DbSet<RefundOrder_View> RefundOrder_Views { get; set; }
        public virtual DbSet<RequestSupport> RequestSupports { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionPending> TransactionPendings { get; set; }
        public virtual DbSet<TransactionRequest> TransactionRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=192.168.1.9;Initial Catalog=IRIS_PAYBILL_SALES;User ID=dev;Password=Aa@123$567*");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCard)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Partner)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PromotionCampaign>(entity =>
            {
                entity.ToTable("PromotionCampaign");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PromotionDetail>(entity =>
            {
                entity.ToTable("PromotionDetail");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PackId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefundOrder>(entity =>
            {
                entity.ToTable("RefundOrder");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.PartnerCreateTime).HasColumnType("datetime");

                entity.Property(e => e.PartnerRefundId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseTime).HasColumnType("datetime");

                entity.Property(e => e.ResultCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefundOrder_View>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RefundOrder_View");

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerCreateTime).HasColumnType("datetime");

                entity.Property(e => e.PartnerRefundId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseTime).HasColumnType("datetime");

                entity.Property(e => e.ResultCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RequestSupport>(entity =>
            {
                entity.ToTable("RequestSupport");

                entity.Property(e => e.Attachment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientIp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.Extend1).HasMaxLength(500);

                entity.Property(e => e.Extend2).HasMaxLength(500);

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PackId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Partner)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerCreateTime).HasColumnType("datetime");

                entity.Property(e => e.PayTime).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.ResponseTime).HasColumnType("datetime");

                entity.Property(e => e.ResultCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionPending>(entity =>
            {
                entity.ToTable("TransactionPending");

                entity.HasIndex(e => new { e.Username, e.ServiceId, e.TrackingId }, "UQ_UserId_TransId")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientIp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.Extend1).HasMaxLength(500);

                entity.Property(e => e.Extend2).HasMaxLength(500);

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PackId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Partner)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerCreateTime).HasColumnType("datetime");

                entity.Property(e => e.PayTime).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.ResponseTime).HasColumnType("datetime");

                entity.Property(e => e.ResultCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionRequest>(entity =>
            {
                entity.ToTable("TransactionRequest");

                entity.HasIndex(e => new { e.Username, e.ServiceId, e.TrackingId }, "UQ_UserId_TransId")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientIp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.Extend1).HasMaxLength(500);

                entity.Property(e => e.Extend2).HasMaxLength(500);

                entity.Property(e => e.PackId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Partner)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerCreateTime).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.ResponseTime).HasColumnType("datetime");

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
