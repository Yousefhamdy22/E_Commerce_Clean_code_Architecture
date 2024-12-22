using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;



namespace E_Commerce.Infastructure.Data
{
    public class ECommerceContext :IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>
        , IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    { 
       
    // private readonly IEncryptionProvider _encryptionProvider;
        
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public ECommerceContext(DbContextOptions options) : base(options)
        {
         //  _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }

        public ECommerceContext() { }


        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region Relation Handling
            // (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Categoryid);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                

            //  (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // (One-to-Many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);




            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.shoppingCarts)
                .HasForeignKey(sc => sc.UserId);

            // Configure the relationship between ShoppingCart and ShoppingCartItems
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(sc => sc.CartItems)
                .WithOne(ci => ci.ShoppingCart)
                .HasForeignKey(ci => ci.ShoppingCartId);


            modelBuilder.Entity<ShoppingCartItem>()
            .HasKey(sci => new { sci.ShoppingCartId, sci.ProductId });  // Composite Key

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCart)
                .WithMany(sc => sc.CartItems)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(sci => sci.ProductId);

            modelBuilder.Entity<PaymentDetails>()
            .HasKey(pd => pd.PaymentId);

            modelBuilder.Entity<PaymentDetails>()
                .HasOne(pd => pd.Order)
                .WithMany(o => o.PaymentDetails)
                .HasForeignKey(pd => pd.OrderId);
            #endregion



          //   modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          //  modelBuilder.UseEncryption(_encryptionProvider);



        }
    }
}
