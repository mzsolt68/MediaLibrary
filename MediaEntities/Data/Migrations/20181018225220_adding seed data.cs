using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaEntities.Data.Migrations
{
    public partial class addingseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AudioFormats",
                columns: new[] { "AudioFormatID", "AudioFormatName" },
                values: new object[,]
                {
                    { 1, "Audio CD" },
                    { 2, "MP3" },
                    { 3, "FLAC" },
                    { 4, "WMA" },
                    { 5, "WAV" },
                    { 6, "OGG" }
                });

            migrationBuilder.InsertData(
                table: "Performers",
                columns: new[] { "PerformerID", "PerformerName" },
                values: new object[] { 1, "Boney M" });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "SongID", "SongLiryc", "SongTitle" },
                values: new object[,]
                {
                    { 18, null, "Kalimba De Luna" },
                    { 17, null, "Baby Do You Wanna Bump" },
                    { 16, null, "El Lute" },
                    { 15, null, "Felicidad" },
                    { 14, null, "Nightflight to Venus" },
                    { 13, null, "Still I'm Sad" },
                    { 12, null, "Gotta Go Home" },
                    { 11, null, "Mary's Boy Child / Oh My Lord" },
                    { 10, null, "No Woman, No Cry" },
                    { 7, null, "Hooray! Hooray! It's A Holi-holiday" },
                    { 8, null, "Painter Man" },
                    { 19, null, "Happy Song" },
                    { 6, null, "Ma Baker" },
                    { 5, null, "Rasputin" },
                    { 4, null, "Brown Girl in the Ring" },
                    { 3, null, "Sunny" },
                    { 2, null, "Daddy Cool" },
                    { 1, null, "Rivers of Babylon" },
                    { 9, null, "Belfast" },
                    { 20, null, "Megamix" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "AlbumID", "AlbumFormatAudioFormatID", "AlbumTitle" },
                values: new object[] { 1, 1, "Boney M Gold" });

            migrationBuilder.InsertData(
                table: "PerformerSongs",
                columns: new[] { "PerformerID", "SongID" },
                values: new object[,]
                {
                    { 1, 18 },
                    { 1, 17 },
                    { 1, 16 },
                    { 1, 15 },
                    { 1, 14 },
                    { 1, 13 },
                    { 1, 12 },
                    { 1, 11 },
                    { 1, 19 },
                    { 1, 10 },
                    { 1, 8 },
                    { 1, 7 },
                    { 1, 6 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 1, 1 },
                    { 1, 9 },
                    { 1, 20 }
                });

            migrationBuilder.InsertData(
                table: "AlbumSongs",
                columns: new[] { "AlbumID", "SongID", "Note", "PlayTime" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2018, 10, 19, 4, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 18, null, new DateTime(2018, 10, 19, 4, 11, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 17, null, new DateTime(2018, 10, 19, 2, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 16, null, new DateTime(2018, 10, 19, 3, 58, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 15, null, new DateTime(2018, 10, 19, 2, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 14, null, new DateTime(2018, 10, 19, 3, 49, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 13, null, new DateTime(2018, 10, 19, 4, 24, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 12, null, new DateTime(2018, 10, 19, 2, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 11, null, new DateTime(2018, 10, 19, 4, 1, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 10, null, new DateTime(2018, 10, 19, 4, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 9, null, new DateTime(2018, 10, 19, 3, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 8, null, new DateTime(2018, 10, 19, 3, 16, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 7, null, new DateTime(2018, 10, 19, 3, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 6, null, new DateTime(2018, 10, 19, 4, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 5, null, new DateTime(2018, 10, 19, 4, 24, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 4, null, new DateTime(2018, 10, 19, 4, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 3, null, new DateTime(2018, 10, 19, 3, 56, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, null, new DateTime(2018, 10, 19, 3, 26, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 19, null, new DateTime(2018, 10, 19, 3, 56, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 20, null, new DateTime(2018, 10, 19, 3, 51, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "AlbumSongs",
                keyColumns: new[] { "AlbumID", "SongID" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "AudioFormats",
                keyColumn: "AudioFormatID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AudioFormats",
                keyColumn: "AudioFormatID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AudioFormats",
                keyColumn: "AudioFormatID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AudioFormats",
                keyColumn: "AudioFormatID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AudioFormats",
                keyColumn: "AudioFormatID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "PerformerSongs",
                keyColumns: new[] { "PerformerID", "SongID" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "AlbumID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Performers",
                keyColumn: "PerformerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AudioFormats",
                keyColumn: "AudioFormatID",
                keyValue: 1);
        }
    }
}
