using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLG06WNC;

public partial class DbG06wncContext : DbContext
{
    public DbG06wncContext()
    {
    }

    public DbG06wncContext(DbContextOptions<DbG06wncContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FileCategory> FileCategories { get; set; }

    public virtual DbSet<ResourceFile> ResourceFiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:G06WNC");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IAccountId).HasName("PK__Account__B681F016745C5B38");

            entity.ToTable("Account");

            entity.Property(e => e.IAccountId).HasColumnName("iAccountID");
            entity.Property(e => e.DBirthofdate)
                .HasColumnType("datetime")
                .HasColumnName("dBirthofdate");
            entity.Property(e => e.IRoleId).HasColumnName("iRoleID");
            entity.Property(e => e.SAvatar)
                .HasColumnType("ntext")
                .HasColumnName("sAvatar");
            entity.Property(e => e.SEmail)
                .HasColumnType("ntext")
                .HasColumnName("sEmail");
            entity.Property(e => e.SName)
                .HasColumnType("ntext")
                .HasColumnName("sName");
            entity.Property(e => e.SPassword)
                .HasColumnType("ntext")
                .HasColumnName("sPassword");
            entity.Property(e => e.SPhone)
                .HasColumnType("ntext")
                .HasColumnName("sPhone");

            entity.HasOne(d => d.IRole).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IRoleId)
                .HasConstraintName("FK_RolesAccount");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.ICategoryId).HasName("PK__Category__342A082C87DBF986");

            entity.ToTable("Category");

            entity.Property(e => e.ICategoryId).HasColumnName("iCategoryID");
            entity.Property(e => e.STitle)
                .HasColumnType("ntext")
                .HasColumnName("sTitle");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.IContentId).HasName("PK__Content__7FB67E4EBC14F5C3");

            entity.ToTable("Content");

            entity.Property(e => e.IContentId).HasColumnName("iContentID");
            entity.Property(e => e.DCreatedate)
                .HasColumnType("datetime")
                .HasColumnName("dCreatedate");
            entity.Property(e => e.ICategoryId).HasColumnName("iCategoryID");
            entity.Property(e => e.SImage)
                .HasColumnType("ntext")
                .HasColumnName("sImage");
            entity.Property(e => e.SMainbody)
                .HasColumnType("ntext")
                .HasColumnName("sMainbody");
            entity.Property(e => e.SSource)
                .HasColumnType("ntext")
                .HasColumnName("sSource");
            entity.Property(e => e.STitle)
                .HasColumnType("ntext")
                .HasColumnName("sTitle");

            entity.HasOne(d => d.ICategory).WithMany(p => p.Contents)
                .HasForeignKey(d => d.ICategoryId)
                .HasConstraintName("FK_CategoryContent");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.IFeedbackId).HasName("PK__Feedback__4A6AA8F7BD98F518");

            entity.ToTable("Feedback");

            entity.Property(e => e.IFeedbackId).HasColumnName("iFeedbackID");
            entity.Property(e => e.IAccountId).HasColumnName("iAccountID");
            entity.Property(e => e.IFeedbackdate)
                .HasColumnType("datetime")
                .HasColumnName("iFeedbackdate");
            entity.Property(e => e.SContent)
                .HasColumnType("ntext")
                .HasColumnName("sContent");
            entity.Property(e => e.SResponse)
                .HasColumnType("ntext")
                .HasColumnName("sResponse");

            entity.HasOne(d => d.IAccount).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.IAccountId)
                .HasConstraintName("FK_FeedbackAccount");
        });

        modelBuilder.Entity<FileCategory>(entity =>
        {
            entity.HasKey(e => e.IFileCategoryId).HasName("PK__FileCate__7B6F2EC200F8AA71");

            entity.ToTable("FileCategory");

            entity.Property(e => e.IFileCategoryId).HasColumnName("iFileCategoryID");
            entity.Property(e => e.STitle)
                .HasColumnType("ntext")
                .HasColumnName("sTitle");
        });

        modelBuilder.Entity<ResourceFile>(entity =>
        {
            entity.HasKey(e => e.IResourceFileId).HasName("PK__Resource__9F0924CD60D1C77E");

            entity.ToTable("ResourceFile");

            entity.Property(e => e.IResourceFileId).HasColumnName("iResourceFileID");
            entity.Property(e => e.DUploaddate)
                .HasColumnType("datetime")
                .HasColumnName("dUploaddate");
            entity.Property(e => e.ICategoryId).HasColumnName("iCategoryID");
            entity.Property(e => e.SDescription)
                .HasColumnType("ntext")
                .HasColumnName("sDescription");
            entity.Property(e => e.SFilename)
                .HasColumnType("ntext")
                .HasColumnName("sFilename");

            entity.HasOne(d => d.ICategory).WithMany(p => p.ResourceFiles)
                .HasForeignKey(d => d.ICategoryId)
                .HasConstraintName("FK_FileCategoryResourceFile");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IRoleId).HasName("PK__Roles__D69F8CBE3AA3D277");

            entity.Property(e => e.IRoleId).HasColumnName("iRoleID");
            entity.Property(e => e.SRolename)
                .HasColumnType("ntext")
                .HasColumnName("sRolename");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
