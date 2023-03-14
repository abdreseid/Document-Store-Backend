
using Microsoft.EntityFrameworkCore;
using DocAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DocAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Genre> Genres { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UpdateDocHistory> UpdateDocHistories { get; set; }
        

    }
}

