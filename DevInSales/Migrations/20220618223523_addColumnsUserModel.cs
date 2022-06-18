using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevInSales.Migrations
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class addColumnsUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "permition",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "role", "UserName" },
                values: new object[] { "Administrador", "romeu" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "role", "UserName" },
                values: new object[] { "Funcionario", "gustavo" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "role", "UserName" },
                values: new object[] { "Gerente", "henrique" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "role", "UserName" },
                values: new object[] { "Funcionario", "tomaspaulo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "permition",
                table: "User");

            migrationBuilder.DropColumn(
                name: "role",
                table: "User");
        }
    }
}
