﻿using Microsoft.EntityFrameworkCore;
using System;
using Domain.Concrete;
using Repository.EntityFramework.Config;

namespace Repository.EntityFramework
{

    // Should NOT be public. Should only be accesible via calls to either of the Handlers.
    public class EntityRepository : DbContext
    {
        private readonly DbContextOptions<EntityRepository> options;
        private readonly bool useLazyLoading;
        public EntityRepository(DbContextOptions<EntityRepository> options) : base(options)
        {
            this.options = options;
            useLazyLoading = true;

            Database.Migrate();
        }

        // Underneath create as many DbSet' as you have domain classes you wish to persist.
        // Each DbSet should have a Config file aplied in the method 'OnModelCreating'

        // e.g
        // public virtual DbSet<YourDomainClass> YourDomainClassInPlural { get; set; }

        public virtual DbSet<DummyExplicit> DummyExplicits { get; set; }
        public virtual DbSet<DummyImplicit> DummyImplicits { get; set; }
        public virtual DbSet<DummyTimestamp> DummyTimestamps { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // To Lazy Load properties they require the keywork Virtual.
            // Making use of Lazy load means that the property only loads as it is about to be used,
            // which improves performance of the program
            optionsBuilder.UseLazyLoadingProxies(useLazyLoading);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create a new class under Config, with the name of the domain class you wish to persist,
            // ending it in Config, to differnetiate it from the actual class

            // e.g
            // modelBuilder.ApplyConfiguration(new YourDomainClassConfig());

            modelBuilder.ApplyConfiguration(new DummyExplicitConfig());
            modelBuilder.ApplyConfiguration(new DummyImplicitConfig());
            modelBuilder.ApplyConfiguration(new DummyTimestampConfig());
        }
    }
}
