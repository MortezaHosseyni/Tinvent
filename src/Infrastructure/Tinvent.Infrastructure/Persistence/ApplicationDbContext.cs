using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tinvent.Domain.Entities;

namespace Tinvent.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Core SaaS Entities
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<User> Users { get; set; }

        // Product & Catalog Entities
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        // Inventory & Warehouse Entities
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventorySnapshot> InventorySnapshots { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
        public DbSet<StockTransferItem> StockTransferItems { get; set; }

        // Customer & Sales Entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Supplier & Purchasing Entities
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This line automatically applies all configurations defined in the assembly.
            // It's a cleaner way than configuring each entity one by one here.
            // We will still define complex relationships manually for clarity.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Convert all enums to strings in the database for readability
            modelBuilder.Entity<Tenant>().Property(t => t.Status).HasConversion<string>();
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
            modelBuilder.Entity<Product>().Property(p => p.Status).HasConversion<string>();
            modelBuilder.Entity<CustomerOrder>().Property(o => o.Status).HasConversion<string>();
            modelBuilder.Entity<CustomerOrder>().Property(o => o.PaymentStatus).HasConversion<string>();
            modelBuilder.Entity<Supplier>().Property(s => s.Status).HasConversion<string>();
            modelBuilder.Entity<PurchaseOrder>().Property(po => po.Status).HasConversion<string>();
            modelBuilder.Entity<StockTransfer>().Property(st => st.Status).HasConversion<string>();
            modelBuilder.Entity<InventoryTransaction>().Property(it => it.TransactionType).HasConversion<string>();


            // === Configure Relationships ===

            // Tenant Relationships
            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Cascade); // If a tenant is deleted, delete their users.

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Products)
                .WithOne(p => p.Tenant)
                .HasForeignKey(p => p.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product Category (Self-referencing relationship)
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.ParentCategory)
                .WithMany(pc => pc.ChildCategories)
                .HasForeignKey(pc => pc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a category if it has children.

            // Warehouse (Manager relationship)
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Manager)
                .WithMany() // A user can manage multiple warehouses, but this side is not tracked on User entity.
                .HasForeignKey(w => w.ManagerUserId)
                .OnDelete(DeleteBehavior.SetNull); // If manager user is deleted, set ManagerUserId to null.

            // Stock Transfer (From/To Warehouse)
            modelBuilder.Entity<StockTransfer>()
                .HasOne(st => st.FromWarehouse)
                .WithMany()
                .HasForeignKey(st => st.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTransfer>()
                .HasOne(st => st.ToWarehouse)
                .WithMany()
                .HasForeignKey(st => st.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // === Configure Composite Keys and Indexes ===

            // Ensure Tenant subdomain is unique
            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.Subdomain)
                .IsUnique();

            // Ensure Product SKU is unique within a Tenant
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.TenantId, p.Sku })
                .IsUnique();

            // Composite key for InventorySnapshot (though we use a Guid Id, this enforces uniqueness)
            modelBuilder.Entity<InventorySnapshot>()
                .HasIndex(inv => new { inv.ProductId, inv.WarehouseId })
                .IsUnique();
        }
    }
}
