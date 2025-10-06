using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company_Management_Panel_CSharp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Email", "Logo", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "info@techcorp.com", "placeholder.png", "Tech Corp", "https://techcorp.com" },
                    { 2, "contact@healthinc.com", "placeholder.png", "Health Inc", "https://healthinc.com" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "alice@techcorp.com", "Alice", "Johnson", "07775845772" },
                    { 2, 1, "bob@techcorp.com", "Bob", "Smith", "07830018563" },
                    { 3, 2, "carol@healthinc.com", "Carol", "Davis", "07774750114" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
