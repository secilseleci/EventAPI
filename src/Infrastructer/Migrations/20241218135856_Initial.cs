﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5"), "gokhan@example.com", "Gökhan Bilir", "", "gokhanBilir" },
                    { new Guid("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12"), "eda@example.com", "Eda Mayalı", "", "edaMayali" },
                    { new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db"), "hasan@example.com", "Hasan Yüksel", "", "hasanYuksel" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "secil@example.com", "Seçil Seleci", "", "secilSeleci" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EndDate", "EventDescription", "EventName", "Location", "OrganizerId", "StartDate", "Timezone" },
                values: new object[,]
                {
                    { new Guid("5dc9ba5e-53c0-4166-87de-5f6f57021256"), new DateTimeOffset(new DateTime(2025, 2, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Kickoff meeting for the new project.", "Project Kickoff Meeting", "Ankara", new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db"), new DateTimeOffset(new DateTime(2025, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "UTC" },
                    { new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"), new DateTimeOffset(new DateTime(2025, 2, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Celebrate our team leader's 40th birthday!", "Team Birthday Party", "Istanbul", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new DateTimeOffset(new DateTime(2025, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "UTC" }
                });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "EventId", "IsAccepted", "Message", "OrganizerId", "ReceiverId" },
                values: new object[,]
                {
                    { new Guid("2a5b59a3-d486-4b8b-b0e4-3fb27cf8b85b"), new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"), false, "Join us for the birthday party!", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db") },
                    { new Guid("3b5c59a3-d486-4b8b-b0e4-3fb27cf8b85b"), new Guid("5dc9ba5e-53c0-4166-87de-5f6f57021256"), false, "Kickoff meeting invitation", new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db"), new Guid("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12") }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "Id", "EventId", "UserId" },
                values: new object[,]
                {
                    { new Guid("cb8808c1-341e-4154-8bac-6f48719968f8"), new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479") },
                    { new Guid("f6521606-69f8-4887-a5c9-963ca57257f6"), new Guid("5dc9ba5e-53c0-4166-87de-5f6f57021256"), new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_EventId",
                table: "Invitations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_OrganizerId",
                table: "Invitations",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ReceiverId",
                table: "Invitations",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
