using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedServiceAgreementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceAgreement",
                columns: table => new
                {
                    ServiceAgreementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    EmployerId = table.Column<int>(type: "int", nullable: true),
                    ReferredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InitialDateOfContact = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMedicaid = table.Column<bool>(type: "bit", nullable: false),
                    IsInsurance = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivatePay = table.Column<bool>(type: "bit", nullable: false),
                    DateBilledOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDueOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfMonthAgreed = table.Column<int>(type: "int", nullable: false),
                    Condition1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeastNoOfDays1 = table.Column<int>(type: "int", nullable: false),
                    LeastNoOfDays2 = table.Column<int>(type: "int", nullable: false),
                    CanAccessPersonalFunds = table.Column<bool>(type: "bit", nullable: false),
                    CanAccessPersonalVichle = table.Column<bool>(type: "bit", nullable: false),
                    HasReceivedBillOfRights = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IsSignedByClient = table.Column<bool>(type: "bit", nullable: false),
                    SignedByClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSignedByAgency = table.Column<bool>(type: "bit", nullable: false),
                    SignedByAgency = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAgreement", x => x.ServiceAgreementId);
                    table.ForeignKey(
                        name: "FK_ServiceAgreement_Employer_EmployerId1",
                        column: x => x.EmployerId1,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceAgreement_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAgreement_EmployerId1",
                table: "ServiceAgreement",
                column: "EmployerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAgreement_JobId",
                table: "ServiceAgreement",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAgreement");
        }
    }
}
