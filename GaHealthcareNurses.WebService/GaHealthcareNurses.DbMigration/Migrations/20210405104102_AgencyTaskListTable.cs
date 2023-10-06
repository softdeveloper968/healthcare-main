using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AgencyTaskListTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgencyTaskList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TaskListTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyTaskList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyTaskList_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyTaskList_TaskListTemplate_TaskListTemplateId",
                        column: x => x.TaskListTemplateId,
                        principalTable: "TaskListTemplate",
                        principalColumn: "TaskListTemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyTaskList_EmployerId",
                table: "AgencyTaskList",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyTaskList_TaskListTemplateId",
                table: "AgencyTaskList",
                column: "TaskListTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyTaskList");
        }
    }
}
