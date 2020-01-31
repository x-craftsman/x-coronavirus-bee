using Craftsman.LaunchPad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Infrastructure
{
    public partial class LaunchPadContext : DbContext
    {
        public LaunchPadContext()
        {
        }

        public LaunchPadContext(DbContextOptions<LaunchPadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consumer> Consumer { get; set; }
        //public virtual DbSet<Group> Group { get; set; }
        //public virtual DbSet<Permission> Permission { get; set; }
        //public virtual DbSet<RelGroupPermission> RelGroupPermission { get; set; }
        //public virtual DbSet<RelUserGroup> RelUserGroup { get; set; }
        //public virtual DbSet<RelUserPermission> RelUserPermission { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<User> User { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseMySql("Server=127.0.0.1;User Id=root;Password=North@09170302;Database=launchpad");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.ToTable("consumer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsEnabled)
                    .HasColumnName("is_enabled")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Secret)
                    .HasColumnName("secret")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("int(11)");
            });


            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("token");

                entity.Ignore(e => e.IsUpdated);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccessToken)
                    .HasColumnName("access_token")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ConfusionCode)
                    .HasColumnName("confusion_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ConsumerId)
                    .HasColumnName("consumer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExpirationInterval)
                    .HasColumnName("expiration_interval")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExpirationTime)
                    .HasColumnName("expiration_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refresh_token")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("int(11)");
            });
        }
    }
}
