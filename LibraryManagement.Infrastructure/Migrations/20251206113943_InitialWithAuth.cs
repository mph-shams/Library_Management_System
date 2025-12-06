using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    public partial class InitialWithAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, new DateTime(2025, 12, 6, 15, 9, 40, 806, DateTimeKind.Local).AddTicks(2920), "admin@library.com", "System Administrator", true, "$2b$10$POTL.v.YkyxUKSdJ9eYBm.QFCTpTWQahMk8LxgS8zZF0U0/yvkbxi", 0, "admin" });

            migrationBuilder.UpdateData(
                table: "BorrowRecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BorrowDate", "UserId" },
                values: new object[] { new DateTime(2025, 11, 26, 15, 9, 40, 806, DateTimeKind.Local).AddTicks(3235), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_UserId",
                table: "BorrowRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRecords_Users_UserId",
                table: "BorrowRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRecords_Users_UserId",
                table: "BorrowRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_BorrowRecords_UserId",
                table: "BorrowRecords");

            migrationBuilder.UpdateData(
                table: "BorrowRecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BorrowDate", "UserId" },
                values: new object[] { new DateTime(2025, 10, 20, 18, 41, 43, 549, DateTimeKind.Local).AddTicks(1689), 101 });
        }
    }
}
