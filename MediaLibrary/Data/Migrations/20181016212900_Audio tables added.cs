using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Data.Migrations
{
    public partial class Audiotablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioFormats",
                columns: table => new
                {
                    AudioFormatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AudioFormatName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioFormats", x => x.AudioFormatID);
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    PerformerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerformerName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.PerformerID);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SongTitle = table.Column<string>(nullable: false),
                    SongLiryc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongID);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlbumTitle = table.Column<string>(nullable: false),
                    AlbumFormatAudioFormatID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumID);
                    table.ForeignKey(
                        name: "FK_Albums_AudioFormats_AlbumFormatAudioFormatID",
                        column: x => x.AlbumFormatAudioFormatID,
                        principalTable: "AudioFormats",
                        principalColumn: "AudioFormatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformerSongs",
                columns: table => new
                {
                    PerformerID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerSongs", x => new { x.PerformerID, x.SongID });
                    table.ForeignKey(
                        name: "FK_PerformerSongs_Performers_PerformerID",
                        column: x => x.PerformerID,
                        principalTable: "Performers",
                        principalColumn: "PerformerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformerSongs_Songs_SongID",
                        column: x => x.SongID,
                        principalTable: "Songs",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlbumSongs",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: false),
                    PlayTime = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumSongs", x => new { x.AlbumID, x.SongID });
                    table.ForeignKey(
                        name: "FK_AlbumSongs_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Albums",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumSongs_Songs_SongID",
                        column: x => x.SongID,
                        principalTable: "Songs",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AlbumFormatAudioFormatID",
                table: "Albums",
                column: "AlbumFormatAudioFormatID");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSongs_SongID",
                table: "AlbumSongs",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerSongs_SongID",
                table: "PerformerSongs",
                column: "SongID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumSongs");

            migrationBuilder.DropTable(
                name: "PerformerSongs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "AudioFormats");
        }
    }
}
