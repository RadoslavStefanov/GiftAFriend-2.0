using GAF.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GAF.Infrastructure.Data
{
    public class GAFDbContext : IdentityDbContext
    {
        public GAFDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { optionsBuilder.UseSqlServer("DefaultConnection"); }
        }


        public DbSet<Emails> Emails { get; set; }
        public DbSet<FriendRequests> FriendRequests { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<TransferEvents> TransferEvents { get; set; }
        public DbSet<UserInfos> UserInfos { get; set; }

    }
}
