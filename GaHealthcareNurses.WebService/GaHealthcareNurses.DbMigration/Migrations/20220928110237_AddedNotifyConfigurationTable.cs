using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedNotifyConfigurationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotifyConfiguration",
                columns: table => new
                {
                    NotifyConfigurationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DaysBeforeDocExpires = table.Column<int>(type: "int", nullable: false),
                    HoursFrequencyForDocs = table.Column<double>(type: "float", nullable: false),
                    NotifyDocExpByApp = table.Column<bool>(type: "bit", nullable: false),
                    NotifyDocExpByText = table.Column<bool>(type: "bit", nullable: false),
                    NotifyDocExpByEmail = table.Column<bool>(type: "bit", nullable: false),
                    MinutesBeforeClockIn = table.Column<int>(type: "int", nullable: false),
                    MinutesFreqToRepeat = table.Column<int>(type: "int", nullable: false),
                    MinutesAfterShiftEnd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyConfiguration", x => x.NotifyConfigurationId);
                    table.ForeignKey(
                        name: "FK_NotifyConfiguration_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotifyConfiguration_EmployerId",
                table: "NotifyConfiguration",
                column: "EmployerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotifyConfiguration");
        }
    }
}
