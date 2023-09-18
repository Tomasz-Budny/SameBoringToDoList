using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
            ConfigureCrentialsTable(builder);
        }

        public void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );
            builder.Property(x => x.Email)
                .HasConversion(
                login => login.Value,
                value => Email.Create(value)
            );
        }

        public void ConfigureCrentialsTable(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(x => x.Credential, cb =>
            {
                cb.ToTable("credentials");
                cb.Property<Guid>("Id");
                cb.HasKey("Id", "UserId");
                cb.WithOwner().HasForeignKey("UserId");
            });
        }
    }
}
