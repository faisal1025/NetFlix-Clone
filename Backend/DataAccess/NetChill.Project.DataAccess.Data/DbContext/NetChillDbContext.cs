using Company.Project.Core.ExceptionManagement.CustomException;
using Microsoft.EntityFrameworkCore;
using NetChill.Project.DataAccess.Domains.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NetChill.Project.DataAccess.Data.Dbcontext
{
    public class NetChillDbContext: DbContext
    {
        public DbSet<MovieDomain> Movies { get; set; }
        public DbSet<UserDomain> Users { get; set; }
        public object UserDomain { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieUser>()
                .HasKey(ma => new { ma.MovieId, ma.UserId });
            builder.Entity<MovieUser>()
                .HasOne(ma => ma.Movie)
                .WithMany(ma => ma.Users)
                .HasForeignKey(ma => ma.MovieId);
            builder.Entity<MovieUser>()
                .HasOne(ma => ma.User)
                .WithMany(ma => ma.Movies)
                .HasForeignKey(ma => ma.UserId);
        }
        public NetChillDbContext(DbContextOptions<NetChillDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            string errorMessage = string.Empty;
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            List<ValidationException> validationExceptionList = new List<ValidationException>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults, true))
                {
                    foreach (var result in validationResults)
                    {
                        if (result != ValidationResult.Success)
                        {
                            validationExceptionList.Add(new ValidationException(result.ErrorMessage));
                        }
                    }

                    throw new ValidationExceptions(validationExceptionList);
                }
            }

            return base.SaveChanges();
        }
    }
    
}
