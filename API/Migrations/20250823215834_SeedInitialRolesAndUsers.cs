using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed Roles
            migrationBuilder.Sql("INSERT INTO Roles (Name) VALUES ('Admin'), ('Supervisor')");

            // Seed Users with hashed passwords (example SHA256 hashes)
            migrationBuilder.Sql(@"INSERT INTO Users (Name, Email, PasswordHash, RoleId, CreatedAt, UpdatedAt) VALUES
                ('Admin User', 'admin@example.com', 'e99a18c428cb38d5f260853678922e03abd8334a', 1, NOW(), NOW()),
                ('Supervisor User', 'supervisor@example.com', 'f379eaf3c831b04de153469d1bec345e5b0e1b50', 2, NOW(), NOW())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
