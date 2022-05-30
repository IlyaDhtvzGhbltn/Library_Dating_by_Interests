using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Entities.Migrations
{
    public partial class UsersRelation_tbo_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequesterPositiveReaction",
                table: "UsersRelations");

            migrationBuilder.DropColumn(
                name: "IsResponserPositiveReaction",
                table: "UsersRelations");

            migrationBuilder.AddColumn<int>(
                name: "RelationStatus",
                table: "UsersRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Creator",
                table: "Dialogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Invited",
                table: "Dialogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationStatus",
                table: "UsersRelations");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "Invited",
                table: "Dialogs");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequesterPositiveReaction",
                table: "UsersRelations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsResponserPositiveReaction",
                table: "UsersRelations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
