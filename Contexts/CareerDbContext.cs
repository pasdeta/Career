using System;
using Career.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Career.Contexts
{
	public class CareerDbContext : DbContext
	{
		public CareerDbContext(DbContextOptions<CareerDbContext> options)
			: base(options)
		{ }

		public DbSet<User> Users { get; set; }

		public DbSet<Company> Companies { get; set; }

		public DbSet<CV> CVs { get; set; }

		public DbSet<Job> Jobs { get; set; }

		public DbSet<Application> Applications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>(company => {
				company.HasQueryFilter(c => c.deleted_at == null);
				company.Property(c => c.created_at).HasDefaultValueSql("getdate()");
			});
			modelBuilder.Entity<User>().Property(c => c.created_at).HasDefaultValueSql("getdate()");
			modelBuilder.Entity<Job>(job => {
				job.Property(c => c.created_at).HasDefaultValueSql("getdate()");
				job.Property(c => c.is_active).HasDefaultValue(true);
				// job.HasQueryFilter(j => j.expire_on >= DateTime.Now && j.is_active);
			});
			modelBuilder.Entity<Application>().Property(c => c.applied_at).HasDefaultValueSql("getdate()");
		}
	}
}