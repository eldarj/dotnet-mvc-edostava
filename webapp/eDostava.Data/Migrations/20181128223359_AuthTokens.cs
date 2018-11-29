using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eDostava.Data.Migrations
{
    public partial class AuthTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthTokeni",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    DatumGenerisanja = table.Column<DateTime>(nullable: false),
                    ModeratorId = table.Column<int>(nullable: false),
                    NarucilacId = table.Column<int>(nullable: false),
                    VlasnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthTokeni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthTokeni_Moderatori_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "Moderatori",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthTokeni_Narucioci_NarucilacId",
                        column: x => x.NarucilacId,
                        principalTable: "Narucioci",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthTokeni_Vlasnici_VlasnikId",
                        column: x => x.VlasnikId,
                        principalTable: "Vlasnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokeni_ModeratorId",
                table: "AuthTokeni",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokeni_NarucilacId",
                table: "AuthTokeni",
                column: "NarucilacId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokeni_VlasnikId",
                table: "AuthTokeni",
                column: "VlasnikId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthTokeni");
        }
    }
}
