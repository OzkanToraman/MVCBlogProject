namespace MVC.Blog.DAL.Context
{ 
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Data;

public partial class BlogContext : DbContext
    {
        public BlogContext()
            : base("name=BlogContext")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<MediaUpload> MediaUpload { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Message)
                .WithOptional(e => e.Kullanici)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Post)
                .WithOptional(e => e.Kullanici)
                .HasForeignKey(e => e.UserId);
        }
    }
}
