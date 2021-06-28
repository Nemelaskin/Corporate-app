using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Models.Context
{
    public class ModelsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ChatList> ChatLists { get; set; }
        public DbSet<User_ChatList> User_ChatLists { get; set; }
        public DbSet<MessageList> MessageLists { get; set; }

        public ModelsContext(DbContextOptions<ModelsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User_ChatList>().HasKey(x => new { x.UserId, x.ChatListId});

        }
    }
}
