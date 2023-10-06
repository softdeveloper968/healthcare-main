using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class SalaryCalculator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlsAreaZipcode",
                columns: table => new
                {
                    BlsAreaCode = table.Column<double>(type: "float", nullable: false),
                    BlsAreaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BlsSalaryDetails",
                columns: table => new
                {
                    Id = table.Column<double>(type: "float", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaCode = table.Column<double>(type: "float", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field3 = table.Column<double>(type: "float", nullable: false),
                    OccupationCode = table.Column<double>(type: "float", nullable: true),
                    OccupationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<double>(type: "float", nullable: true),
                    YearPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedianSalary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlsAreaZipcode");

            migrationBuilder.DropTable(
                name: "BlsSalaryDetails");
        }
    }
}
