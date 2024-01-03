using Microsoft.EntityFrameworkCore;

using AppManager.Domain.Entities;
using AppManager.Domain.Enums;
using AppManager.Infra.Data.Mapping;

namespace AppManager.Infra.Data.Context;

public class EfDbContext : DbContext
{
	public virtual DbSet<UserEntity> Users { get; set; }

	public EfDbContext(
		DbContextOptions<EfDbContext> options
		)
		: base(options) 
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Addd the Postgres Extension for UUID generation
		modelBuilder.HasPostgresExtension("uuid-ossp");
		modelBuilder.HasDefaultSchema(SchemaNames.DefaultSchema.Value);

		#region Enums Mapping

		modelBuilder.HasPostgresEnum<UserStatus>();

		#endregion

		#region Tables Maps

		modelBuilder.ApplyConfiguration(new UserMap());

		#endregion

		base.OnModelCreating(modelBuilder);
	}

}
