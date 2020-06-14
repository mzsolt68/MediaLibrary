using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Entities.Migrations
{
    public partial class RenamePerformertoSongPerformer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformerSongs_Performers_PerformerID",
                table: "PerformerSongs");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.CreateTable(
                name: "SongPerformers",
                columns: table => new
                {
                    PerformerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongPerformers", x => x.PerformerID);
                });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 26, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 16, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 20, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 2, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 49, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 2, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 58, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 2, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 4, 11, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 21 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 52, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 22 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 4, 22 },
                column: "PlayTime",
                value: new DateTime(2020, 6, 14, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "SongPerformers",
                columns: new[] { "PerformerID", "PerformerName" },
                values: new object[,]
                {
                    { 4, "Bery" },
                    { 3, "Hrutka Róbert" },
                    { 2, "Jamie Winchester" },
                    { 5, "Váczi Eszter" },
                    { 1, "Boney M" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PerformerSongs_SongPerformers_PerformerID",
                table: "PerformerSongs",
                column: "PerformerID",
                principalTable: "SongPerformers",
                principalColumn: "PerformerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformerSongs_SongPerformers_PerformerID",
                table: "PerformerSongs");

            migrationBuilder.DropTable(
                name: "SongPerformers");

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    PerformerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.PerformerID);
                });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 26, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 16, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 20, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 2, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 49, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 2, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 58, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 2, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 4, 11, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 21 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 52, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 22 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 4, 22 },
                column: "PlayTime",
                value: new DateTime(2020, 4, 11, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Performers",
                columns: new[] { "PerformerID", "PerformerName" },
                values: new object[,]
                {
                    { 4, "Bery" },
                    { 3, "Hrutka Róbert" },
                    { 2, "Jamie Winchester" },
                    { 5, "Váczi Eszter" },
                    { 1, "Boney M" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PerformerSongs_Performers_PerformerID",
                table: "PerformerSongs",
                column: "PerformerID",
                principalTable: "Performers",
                principalColumn: "PerformerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
