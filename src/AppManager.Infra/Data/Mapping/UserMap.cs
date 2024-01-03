using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AppManager.Domain.Entities;
using AppManager.Domain.Enums;

namespace AppManager.Infra.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		// Table Name
		builder.ToTable(
			TableNames.UserTable.Value, 
			SchemaNames.DefaultSchema.Value
		);

		#region Index

		builder.HasIndex(e => e.Name)
			.HasDatabaseName("idx_user_name")
			.IsUnique();

		#endregion

		#region Table Properties

		#region Base Values
		// ID
		builder.Property(p => p.Id)
			.HasColumnName("id")
			.HasColumnType("uuid")
			.HasDefaultValueSql("uuid_generate_v4()")
			.ValueGeneratedOnAdd()
			.HasColumnOrder(0)
			.IsRequired();

		// Status
		builder.Property(c => c.IsEnable)
			.HasColumnName("is_enable")
			.HasDefaultValueSql("true")
			.ValueGeneratedOnAdd()
			.HasColumnOrder(3)
			.IsRequired();

		// Created At
		builder.Property(e => e.CreatedAt)
			.HasColumnName("created_at")
			.HasDefaultValueSql("now()")
			.ValueGeneratedOnAdd()
			.HasColumnOrder(4)
			.IsRequired();

		// Updated At
		builder.Property(e => e.UpdatedAt)
			.HasColumnName("updated_at")
			.ValueGeneratedOnUpdate()
			.HasColumnOrder(5);

		#endregion

		// Name
		builder.Property(c => c.Name)
			.HasColumnName("name")
			.HasColumnType("varchar(100)")
			.HasMaxLength(100)
			.HasColumnOrder(1)
			.IsRequired();

		// Status
		builder.Property(c => c.Status)
			.HasColumnName("status")
			.HasDefaultValue(UserStatus.Active)
			.HasConversion<string>()
			.HasColumnOrder(2)
			.HasComment(
				$@"Options value: 
					{UserStatus.Active}, 
					{UserStatus.Inactive}, 
					{UserStatus.Suspended}"
			)
			.IsRequired();

		#endregion
	}
}
