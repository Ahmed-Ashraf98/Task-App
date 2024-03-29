﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FBWeb.Models;

#nullable disable

namespace FBWeb.Data
{
    public partial class FlutterbookContext : DbContext
    {
        public FlutterbookContext()
        {
        }

        public FlutterbookContext(DbContextOptions<FlutterbookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactTable> ContactTable { get; set; }
        public virtual DbSet<EventTable> EventTable { get; set; }
        public virtual DbSet<NoteTable> NoteTable { get; set; }
        public virtual DbSet<TaskTable> TaskTable { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FlutterBook;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AI");

            modelBuilder.Entity<ContactTable>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK_Contact");

                entity.Property(e => e.ImgUrl).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContactTable)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ContactTable_User");
            });

            modelBuilder.Entity<EventTable>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK_Event_1");

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventTable_User");
            });

            modelBuilder.Entity<NoteTable>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK_Note_1");

                entity.Property(e => e.NoteText).IsUnicode(false);

                entity.Property(e => e.NoteTitle).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NoteTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoteTable_User");
            });

            modelBuilder.Entity<TaskTable>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK_Task_1");

                entity.Property(e => e.TextTask).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TaskTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTable_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}