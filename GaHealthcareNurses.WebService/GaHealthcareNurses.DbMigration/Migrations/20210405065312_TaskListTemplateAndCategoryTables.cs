using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class TaskListTemplateAndCategoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskListCategory",
                columns: table => new
                {
                    TaskListCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListCategory", x => x.TaskListCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TaskListTemplate",
                columns: table => new
                {
                    TaskListTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskCategoryId = table.Column<int>(type: "int", nullable: true),
                    TaskListCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListTemplate", x => x.TaskListTemplateId);
                    table.ForeignKey(
                        name: "FK_TaskListTemplate_TaskListCategory_TaskListCategoryId",
                        column: x => x.TaskListCategoryId,
                        principalTable: "TaskListCategory",
                        principalColumn: "TaskListCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskListTemplate_TaskListCategoryId",
                table: "TaskListTemplate",
                column: "TaskListCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskListTemplate");

            migrationBuilder.DropTable(
                name: "TaskListCategory");
        }
    }
}
