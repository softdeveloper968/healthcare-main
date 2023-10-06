using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class RemovedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CareRecipient");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "RequiredService");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "RequiredService",
                columns: table => new
                {
                    RequiredServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredService", x => x.RequiredServiceId);
                    table.ForeignKey(
                        name: "FK_RequiredService_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredService_ServiceList_ServiceListId",
                        column: x => x.ServiceListId,
                        principalTable: "ServiceList",
                        principalColumn: "ServiceListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CareRecipient",
                columns: table => new
                {
                    CareRecipientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Alert = table.Column<bool>(type: "bit", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CareRecipientLocationId = table.Column<int>(type: "int", nullable: true),
                    CareRecipientRelationshipId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CodeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Forgetful = table.Column<bool>(type: "bit", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionalLimitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderPreference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleInitial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteToCaregiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receptiveness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredServiceId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                        column: x => x.RequiredServiceId,
                        principalTable: "RequiredService",
                        principalColumn: "RequiredServiceId",
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
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HourlyRate = table.Column<int>(type: "int", nullable: false),
                    PostedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequiredServiceId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
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
                        name: "FK_Job_RequiredService_RequiredServiceId",
                        column: x => x.RequiredServiceId,
                        principalTable: "RequiredService",
                        principalColumn: "RequiredServiceId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CareRecipient_RequiredServiceId",
                table: "CareRecipient",
                column: "RequiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_StateId",
                table: "CareRecipient",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CityId",
                table: "Job",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_EmployerId",
                table: "Job",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_RequiredServiceId",
                table: "Job",
                column: "RequiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ResourceId",
                table: "Job",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredService_ClientId",
                table: "RequiredService",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredService_ServiceListId",
                table: "RequiredService",
                column: "ServiceListId");

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
    }
}
