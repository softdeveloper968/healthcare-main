using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ResourceId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceList",
                columns: table => new
                {
                    ServiceListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceList", x => x.ServiceListId);
                });

            migrationBuilder.CreateTable(
                name: "CareRecipient",
                columns: table => new
                {
                    CareRecipientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceListId = table.Column<int>(type: "int", nullable: false),
                    CareRecipientLocationId = table.Column<int>(type: "int", nullable: true),
                    CareRecipientRelationshipId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleInitial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alert = table.Column<bool>(type: "bit", nullable: false),
                    Forgetful = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionalLimitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderPreference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteToCaregiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receptiveness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenToStart = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareRecipient", x => x.CareRecipientId);
                    table.ForeignKey(
                        name: "FK_CareRecipient_CareRecipientLocation_CareRecipientLocationId",
                        column: x => x.CareRecipientLocationId,
                        principalTable: "CareRecipientLocation",
                        principalColumn: "CareRecipientLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareRecipient_CareRecipientRelationship_CareRecipientRelationshipId",
                        column: x => x.CareRecipientRelationshipId,
                        principalTable: "CareRecipientRelationship",
                        principalColumn: "CareRecipientRelationshipId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareRecipient_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareRecipient_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareRecipient_ServiceList_ServiceListId",
                        column: x => x.ServiceListId,
                        principalTable: "ServiceList",
                        principalColumn: "ServiceListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareRecipient_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceListId = table.Column<int>(type: "int", nullable: false),
                    CareRecipientId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyRate = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Job_CareRecipient_CareRecipientId",
                        column: x => x.CareRecipientId,
                        principalTable: "CareRecipient",
                        principalColumn: "CareRecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Job_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitNote_CareRecipientId",
                table: "VisitNote",
                column: "CareRecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNote_JobId",
                table: "VisitNote",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_CareCoordinationNote_CareRecipientId",
                table: "CareCoordinationNote",
                column: "CareRecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_CareRecipientLocationId",
                table: "CareRecipient",
                column: "CareRecipientLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_CareRecipientRelationshipId",
                table: "CareRecipient",
                column: "CareRecipientRelationshipId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_CityId",
                table: "CareRecipient",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_ClientId",
                table: "CareRecipient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_ServiceListId",
                table: "CareRecipient",
                column: "ServiceListId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_StateId",
                table: "CareRecipient",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CareRecipientId",
                table: "Job",
                column: "CareRecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CityId",
                table: "Job",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_EmployerId",
                table: "Job",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ResourceId",
                table: "Job",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareCoordinationNote_CareRecipient_CareRecipientId",
                table: "CareCoordinationNote",
                column: "CareRecipientId",
                principalTable: "CareRecipient",
                principalColumn: "CareRecipientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitNote_CareRecipient_CareRecipientId",
                table: "VisitNote",
                column: "CareRecipientId",
                principalTable: "CareRecipient",
                principalColumn: "CareRecipientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitNote_Job_JobId",
                table: "VisitNote",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareCoordinationNote_CareRecipient_CareRecipientId",
                table: "CareCoordinationNote");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitNote_CareRecipient_CareRecipientId",
                table: "VisitNote");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitNote_Job_JobId",
                table: "VisitNote");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "CareRecipient");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "ServiceList");

            migrationBuilder.DropIndex(
                name: "IX_VisitNote_CareRecipientId",
                table: "VisitNote");

            migrationBuilder.DropIndex(
                name: "IX_VisitNote_JobId",
                table: "VisitNote");

            migrationBuilder.DropIndex(
                name: "IX_CareCoordinationNote_CareRecipientId",
                table: "CareCoordinationNote");
        }
    }
}
