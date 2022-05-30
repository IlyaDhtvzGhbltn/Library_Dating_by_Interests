using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Entities.Migrations
{
    public partial class wwwwwww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_AspNetUsers_ApiUserId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_ApiUserId",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "ApiUserId",
                table: "Dialogs");

            migrationBuilder.CreateTable(
                name: "ApiUserDialog",
                columns: table => new
                {
                    DialogsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUserDialog", x => new { x.DialogsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ApiUserDialog_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiUserDialog_Dialogs_DialogsId",
                        column: x => x.DialogsId,
                        principalTable: "Dialogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiUserDialog_ParticipantsId",
                table: "ApiUserDialog",
                column: "ParticipantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUserDialog");

            migrationBuilder.AddColumn<string>(
                name: "ApiUserId",
                table: "Dialogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_ApiUserId",
                table: "Dialogs",
                column: "ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_AspNetUsers_ApiUserId",
                table: "Dialogs",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
