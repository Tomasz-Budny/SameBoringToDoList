﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SameBoringToDoList.Infrastructure.Persistence;

#nullable disable

namespace SameBoringToDoList.Infrastructure.Migrations
{
    [DbContext(typeof(SameBoringToDoListDbContext))]
    partial class SameBoringToDoListDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SameBoringToDoList.Domain.Entities.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ToDoLists", (string)null);
                });

            modelBuilder.Entity("SameBoringToDoList.Domain.Entities.ToDoList", b =>
                {
                    b.OwnsMany("SameBoringToDoList.Domain.Entities.ToDoItem", "ToDoItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ToDoListId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("IsDone")
                                .HasColumnType("bit");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id", "ToDoListId");

                            b1.HasIndex("ToDoListId");

                            b1.ToTable("ToDoItems", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ToDoListId");
                        });

                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
