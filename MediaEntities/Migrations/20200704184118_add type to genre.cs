using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Entities.Migrations
{
    public partial class addtypetogenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenreType",
                table: "Genres",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 26, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 16, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 20, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 2, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 49, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 2, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 58, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 2, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 4, 11, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 21 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 52, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 22 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 4, 22 },
                column: "PlayTime",
                value: new DateTime(2020, 7, 4, 3, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 1,
                column: "GenreType",
                value: "audio");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 2,
                column: "GenreType",
                value: "audio");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 3,
                column: "GenreType",
                value: "audio");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 4,
                column: "GenreType",
                value: "audio");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreID", "GenreName", "GenreType" },
                values: new object[,]
                {
                    { 7, "Akció", "video" },
                    { 6, "Vígjáték", "video" },
                    { 8, "Romantikus", "video" },
                    { 5, "Dráma", "video" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "GenreType",
                table: "Genres");

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
        }
    }
}
