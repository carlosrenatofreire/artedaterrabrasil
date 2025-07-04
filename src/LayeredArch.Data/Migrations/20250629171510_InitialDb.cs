using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LayeredArch.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "P_Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                    Tag = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "P_Claims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                    Tag = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_P_Claims_P_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "P_Modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "E_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordChanged = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_E_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "P_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                    Tag = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_P_Roles_E_Users_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "E_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "R_Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_R_Permissions_P_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "P_Claims",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_Permissions_P_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "P_Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_Permissions_P_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "P_Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_E_Users_RoleId",
                table: "E_Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_P_Claims_ModuleId",
                table: "P_Claims",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_P_Roles_SupervisorId",
                table: "P_Roles",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_R_Permissions_ClaimId",
                table: "R_Permissions",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_R_Permissions_ModuleId",
                table: "R_Permissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_R_Permissions_RoleId",
                table: "R_Permissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_E_Users_P_Roles_RoleId",
                table: "E_Users",
                column: "RoleId",
                principalTable: "P_Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_E_Users_P_Roles_RoleId",
                table: "E_Users");

            migrationBuilder.DropTable(
                name: "R_Permissions");

            migrationBuilder.DropTable(
                name: "P_Claims");

            migrationBuilder.DropTable(
                name: "P_Modules");

            migrationBuilder.DropTable(
                name: "P_Roles");

            migrationBuilder.DropTable(
                name: "E_Users");
        }
    }
}
