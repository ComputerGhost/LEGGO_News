﻿using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Some global configurations that we want are:
            // 1. Tables are the type name.
            // 2. Columns named "Id" are primary keys.

            modelBuilder.Entity<Character>()
                .Property("Id")
                .UseIdentityColumn();

            modelBuilder.Entity<Lead>()
                .Property("Id")
                .UseIdentityColumn();

            modelBuilder.Entity<Tag>()
                .Property("Id")
                .UseIdentityColumn();


            // Set up relations

            //modelBuilder.Entity<Lead>()
            //    .HasMany(left => left.Character)
            //    .WithMany(right => right.Lead)
            //    .UsingEntity(join => join.ToTable("LeadCharacter"));

        }

    }
}