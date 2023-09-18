using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Infrastructure.Persistence.Configurations
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            ConfigureToDoListTable(builder);
            ConfigureToDoItemsTable(builder);
        }

        public void ConfigureToDoListTable(EntityTypeBuilder<ToDoList> builder)
        {
            builder.ToTable("ToDoLists");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => ToDoListId.Create(value)
            );

            builder.Property(x => x.Title)
                .HasConversion(
                title => title.Value,
                value => ToDoListTitle.Create(value)
            );

            builder.Property(x => x.AuthorId)
                .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );
        }

        public void ConfigureToDoItemsTable(EntityTypeBuilder<ToDoList> builder)
        {
            builder.OwnsMany(x => x.ToDoItems, ib =>
            {
                ib.ToTable("ToDoItems");
                ib.HasKey("Id", "ToDoListId");
                ib.WithOwner().HasForeignKey("ToDoListId");
                ib.Property(x => x.Id)
                    .HasConversion(
                    id => id.Value,
                    value => ToDoItemId.Create(value)
                );
                ib.Property(x => x.Title)
                    .HasConversion(
                    title => title.Value,
                    value => ToDoItemTitle.Create(value)
                );

                ib.Property(x => x.Description)
                    .HasConversion(
                    description => description.Value,
                    value => ToDoItemDescription.Create(value)
                );
            });

            builder.Metadata.FindNavigation(nameof(ToDoList.ToDoItems))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
