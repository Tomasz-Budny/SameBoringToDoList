using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SameBoringToDoList.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Email_Ver_Details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "users",
                newName: "Email");

            migrationBuilder.AddColumn<Guid>(
                name: "PasswordResetToken",
                table: "credentials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "credentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VerificationToken",
                table: "credentials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "credentials",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "credentials");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "credentials");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "credentials");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "credentials");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "Login");
        }
    }
}
