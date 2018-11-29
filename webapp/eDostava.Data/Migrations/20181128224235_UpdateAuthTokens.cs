using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eDostava.Data.Migrations
{
    public partial class UpdateAuthTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokeni_Moderatori_ModeratorId",
                table: "AuthTokeni");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokeni_Narucioci_NarucilacId",
                table: "AuthTokeni");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokeni_Vlasnici_VlasnikId",
                table: "AuthTokeni");

            migrationBuilder.AlterColumn<int>(
                name: "VlasnikId",
                table: "AuthTokeni",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "NarucilacId",
                table: "AuthTokeni",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ModeratorId",
                table: "AuthTokeni",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokeni_Moderatori_ModeratorId",
                table: "AuthTokeni",
                column: "ModeratorId",
                principalTable: "Moderatori",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokeni_Narucioci_NarucilacId",
                table: "AuthTokeni",
                column: "NarucilacId",
                principalTable: "Narucioci",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokeni_Vlasnici_VlasnikId",
                table: "AuthTokeni",
                column: "VlasnikId",
                principalTable: "Vlasnici",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokeni_Moderatori_ModeratorId",
                table: "AuthTokeni");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokeni_Narucioci_NarucilacId",
                table: "AuthTokeni");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokeni_Vlasnici_VlasnikId",
                table: "AuthTokeni");

            migrationBuilder.AlterColumn<int>(
                name: "VlasnikId",
                table: "AuthTokeni",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NarucilacId",
                table: "AuthTokeni",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModeratorId",
                table: "AuthTokeni",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokeni_Moderatori_ModeratorId",
                table: "AuthTokeni",
                column: "ModeratorId",
                principalTable: "Moderatori",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokeni_Narucioci_NarucilacId",
                table: "AuthTokeni",
                column: "NarucilacId",
                principalTable: "Narucioci",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokeni_Vlasnici_VlasnikId",
                table: "AuthTokeni",
                column: "VlasnikId",
                principalTable: "Vlasnici",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
