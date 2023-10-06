using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class _202302160402_ClientProfileDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoesBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LeadCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InitAssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConversionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MailingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geocode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobAppGeofence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverrideGeofenceRadiusMet = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HeightInFeet = table.Column<int>(type: "int", nullable: false),
                    HeightInInch = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TriageLevelId = table.Column<int>(type: "int", nullable: false),
                    AdvanceDir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DNR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Will = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephonyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientProfile");
        }
    }
}
