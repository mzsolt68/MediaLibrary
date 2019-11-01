using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Entities.Data.Migrations
{
    public partial class AddGenreandLanguagetoSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreID",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageID",
                table: "Songs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenreName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TagName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 26, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 16, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 20, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 2, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 49, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 2, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 58, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 2, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 4, 11, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 21 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 52, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 22 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 4, 22 },
                column: "PlayTime",
                value: new DateTime(2018, 12, 19, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreID",
                table: "Songs",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_LanguageID",
                table: "Songs",
                column: "LanguageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenreID",
                table: "Songs",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Languages_LanguageID",
                table: "Songs",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "LanguageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenreID",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Languages_LanguageID",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Songs_GenreID",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_LanguageID",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "GenreID",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "LanguageID",
                table: "Songs");

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 26, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 16, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 20, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 2, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 49, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 2, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 58, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 2, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 4, 11, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 21 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 52, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 22 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 4, 22 },
                column: "PlayTime",
                value: new DateTime(2018, 11, 4, 3, 45, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
