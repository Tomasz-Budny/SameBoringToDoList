﻿using Microsoft.EntityFrameworkCore;
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
                value => UserId.Create(value).Value
            );
            builder.Property(x => x.Login)
                .HasConversion(
                login => login.Value,
                value => UserLogin.Create(value).Value
            );
        }

        public void ConfigureCrentialsTable(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(x => x.Credential, cb =>
            {
                cb.ToTable("credentials");
                cb.HasKey("Id", "UserId");
                cb.WithOwner().HasForeignKey("UserId");
                cb.Property(x => x.Id)
                    .HasConversion(
                    id => id.Value,
                    value => CredentialId.Create(value).Value
                );
            });
        }
    }
}
