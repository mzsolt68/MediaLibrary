using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Entities.Data.Migrations
{
    public partial class AddtracknumberpropertytoAlbumSongtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackNr",
                table: "AlbumSongs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 },
                column: "TrackNr",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 },
                column: "TrackNr",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 },
                column: "TrackNr",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 },
                column: "TrackNr",
                value: 4);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 },
                column: "TrackNr",
                value: 5);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 },
                column: "TrackNr",
                value: 6);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 },
                column: "TrackNr",
                value: 7);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 },
                column: "TrackNr",
                value: 8);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 },
                column: "TrackNr",
                value: 9);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 },
                column: "TrackNr",
                value: 10);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 },
                column: "TrackNr",
                value: 11);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 },
                column: "TrackNr",
                value: 12);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 },
                column: "TrackNr",
                value: 13);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 },
                column: "TrackNr",
                value: 14);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 },
                column: "TrackNr",
                value: 15);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 },
                column: "TrackNr",
                value: 16);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 },
                column: "TrackNr",
                value: 17);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 },
                column: "TrackNr",
                value: 18);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 },
                column: "TrackNr",
                value: 19);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 },
                column: "TrackNr",
                value: 20);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 21 },
                column: "TrackNr",
                value: 20);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 2, 22 },
                column: "TrackNr",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 4, 22 },
                column: "TrackNr",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackNr",
                table: "AlbumSongs");
        }
    }
}
