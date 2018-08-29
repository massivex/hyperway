using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mx.Hyperway.Smp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeppolDocuments",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identifier = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeppolDocuments", x => x.Id);
                    table.UniqueConstraint("AK_PeppolDocuments_Identifier", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "PeppolProcesses",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identifier = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeppolProcesses", x => x.Id);
                    table.UniqueConstraint("AK_PeppolProcesses_Identifier", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "SmpHosts",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hostname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmpHosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeppolParticipants",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identifier = table.Column<string>(nullable: false),
                    SmpHostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeppolParticipants", x => x.Id);
                    table.UniqueConstraint("AK_PeppolParticipants_Identifier", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_PeppolParticipants_SmpHosts_SmpHostId",
                        column: x => x.SmpHostId,
                        principalTable: "SmpHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmpServices",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PeppolParticipantId = table.Column<int>(nullable: false),
                    PeppolDocumentId = table.Column<int>(nullable: false),
                    PeppolProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmpServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmpServices_PeppolDocuments_PeppolDocumentId",
                        column: x => x.PeppolDocumentId,
                        principalTable: "PeppolDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmpServices_PeppolParticipants_PeppolParticipantId",
                        column: x => x.PeppolParticipantId,
                        principalTable: "PeppolParticipants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmpServices_PeppolProcesses_PeppolProcessId",
                        column: x => x.PeppolProcessId,
                        principalTable: "PeppolProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmpServiceEndpoints",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SmpServiceId = table.Column<int>(nullable: false),
                    RequireBusinessLevelSignature = table.Column<bool>(nullable: false),
                    Endpoint = table.Column<string>(nullable: true),
                    ServiceActivationDate = table.Column<DateTime>(nullable: false),
                    ServiceExpirationDate = table.Column<DateTime>(nullable: false),
                    Certificate = table.Column<string>(nullable: true),
                    ServiceDescription = table.Column<string>(nullable: true),
                    TechnicalContactUrl = table.Column<string>(nullable: true),
                    TransportProfile = table.Column<string>(nullable: true),
                    MinimumAuthenticationLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmpServiceEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmpServiceEndpoints_SmpServices_SmpServiceId",
                        column: x => x.SmpServiceId,
                        principalTable: "SmpServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeppolParticipants_SmpHostId",
                table: "PeppolParticipants",
                column: "SmpHostId");

            migrationBuilder.CreateIndex(
                name: "IX_SmpServiceEndpoints_SmpServiceId",
                table: "SmpServiceEndpoints",
                column: "SmpServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SmpServices_PeppolDocumentId",
                table: "SmpServices",
                column: "PeppolDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SmpServices_PeppolParticipantId",
                table: "SmpServices",
                column: "PeppolParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_SmpServices_PeppolProcessId",
                table: "SmpServices",
                column: "PeppolProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmpServiceEndpoints");

            migrationBuilder.DropTable(
                name: "SmpServices");

            migrationBuilder.DropTable(
                name: "PeppolDocuments");

            migrationBuilder.DropTable(
                name: "PeppolParticipants");

            migrationBuilder.DropTable(
                name: "PeppolProcesses");

            migrationBuilder.DropTable(
                name: "SmpHosts");
        }
    }
}
