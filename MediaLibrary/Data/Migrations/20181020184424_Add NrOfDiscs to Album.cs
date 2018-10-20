using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Data.Migrations
{
    public partial class AddNrOfDiscstoAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Disc",
                table: "AlbumSongs",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "NrOfDiscs",
                table: "Albums",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 26, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 56, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 24, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 5, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 55, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 16, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 25, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 20, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 1, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 2, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 24, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 49, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 2, 50, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 58, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 2, 25, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 4, 11, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 56, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                columns: new[] { "Disc", "PlayTime" },
                values: new object[] { (byte)1, new DateTime(2018, 10, 20, 3, 51, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "AlbumID",
                keyValue: 1,
                column: "NrOfDiscs",
                value: (byte)1);

            migrationBuilder.InsertData(
                table: "Performers",
                columns: new[] { "PerformerID", "PerformerName" },
                values: new object[,]
                {
                    { 2, "Jamie Winchester" },
                    { 3, "Hrutka Róbert" },
                    { 4, "Bery" },
                    { 5, "Váczi Eszter" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "SongID", "SongLiryc", "SongTitle" },
                values: new object[,]
                {
                    { 21, null, "It's Your Life" },
                    { 22, null, "Egyedül" }
                });

            migrationBuilder.InsertData(
                table: "PerformerSongs",
                columns: new[] { "PerformerID", "SongID" },
                values: new object[,]
                {
                    { 2, 21 },
                    { 3, 21 },
                    { 4, 22 },
                    { 5, 22 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 2, 21 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 3, 21 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 4, 22 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 5, 22 });

            migrationBuilder.DeleteData(
                table: "Performers",
                keyColumn: "PerformerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Performers",
                keyColumn: "PerformerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Performers",
                keyColumn: "PerformerID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Performers",
                keyColumn: "PerformerID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 22);

            migrationBuilder.DropColumn(
                name: "Disc",
                table: "AlbumSongs");

            migrationBuilder.DropColumn(
                name: "NrOfDiscs",
                table: "Albums");

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 26, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 16, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 20, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 2, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 24, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 49, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 2, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 58, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 2, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 4, 11, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 56, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "PlayTime",
                value: new DateTime(2018, 10, 19, 3, 51, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
