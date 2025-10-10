using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company_Management_Panel_CSharp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    LogoPath = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Email", "LogoPath", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "info@techcorp.com", null, "Tech Corp", "https://techcorp.com" },
                    { 2, "contact@healthinc.com", null, "Health Inc", "https://healthinc.com" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "alice@techcorp.com", "Alice", "Johnson", "07775845772" },
                    { 2, 1, "bob@techcorp.com", "Bob", "Smith", "07830018563" },
                    { 3, 1, "daniel@techcorp.com", "Daniel", "Wright", "07765123987" },
                    { 4, 1, "ella@techcorp.com", "Ella", "Brown", "07798104563" },
                    { 5, 1, "frank@techcorp.com", "Frank", "Thompson", "07819245670" },
                    { 6, 1, "grace@techcorp.com", "Grace", "Anderson", "07770135842" },
                    { 7, 1, "harry@techcorp.com", "Harry", "Lewis", "07804329871" },
                    { 8, 1, "isla@techcorp.com", "Isla", "Turner", "07766129854" },
                    { 9, 2, "carol@healthinc.com", "Carol", "Davis", "07774750114" },
                    { 10, 2, "jack@healthinc.com", "Jack", "Miller", "07801563472" },
                    { 11, 2, "katie@healthinc.com", "Katie", "Wilson", "07894216053" },
                    { 12, 2, "liam@healthinc.com", "Liam", "Moore", "07787520169" },
                    { 13, 2, "mia@healthinc.com", "Mia", "Taylor", "07831654729" },
                    { 14, 2, "noah@healthinc.com", "Noah", "Clark", "07790765231" },
                    { 15, 2, "olivia@healthinc.com", "Olivia", "Martin", "07762594318" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
