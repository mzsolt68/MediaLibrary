using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLibrary.Entities.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudioFormats",
                columns: table => new
                {
                    AudioFormatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AudioFormatName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioFormats", x => x.AudioFormatID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(nullable: false),
                    GenreType = table.Column<string>(nullable: false)
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageID);
                });

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

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumTitle = table.Column<string>(nullable: false),
                    AudioFormatID = table.Column<int>(nullable: false),
                    NrOfDiscs = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumID);
                    table.ForeignKey(
                        name: "FK_Albums_AudioFormats_AudioFormatID",
                        column: x => x.AudioFormatID,
                        principalTable: "AudioFormats",
                        principalColumn: "AudioFormatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongTitle = table.Column<string>(nullable: false),
                    SongLyric = table.Column<string>(nullable: true),
                    GenreID = table.Column<int>(nullable: false),
                    LanguageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongID);
                    table.ForeignKey(
                        name: "FK_Songs_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Songs_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlbumSongs",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: false),
                    TrackNr = table.Column<int>(nullable: false),
                    PlayTime = table.Column<string>(nullable: false),
                    Disc = table.Column<byte>(nullable: false),
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
                        name: "FK_PerformerSongs_SongPerformers_PerformerID",
                        column: x => x.PerformerID,
                        principalTable: "SongPerformers",
                        principalColumn: "PerformerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformerSongs_Songs_SongID",
                        column: x => x.SongID,
                        principalTable: "Songs",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Genres",
                columns: new[] { "GenreID", "GenreName", "GenreType" },
                values: new object[,]
                {
                    { 8, "Romantikus", "video" },
                    { 7, "Akció", "video" },
                    { 6, "Vígjáték", "video" },
                    { 5, "Dráma", "video" },
                    { 4, "Pop", "audio" },
                    { 3, "Rock", "audio" },
                    { 1, "Disco", "audio" },
                    { 2, "Jazz", "audio" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "LanguageName" },
                values: new object[,]
                {
                    { 3, "német" },
                    { 1, "angol" },
                    { 2, "francia" },
                    { 4, "magyar" }
                });

            migrationBuilder.InsertData(
                table: "SongPerformers",
                columns: new[] { "PerformerID", "PerformerName" },
                values: new object[,]
                {
                    { 5, "Váczi Eszter" },
                    { 4, "Bery" },
                    { 3, "Hrutka Róbert" },
                    { 2, "Jamie Winchester" },
                    { 1, "Boney M" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "AlbumID", "AlbumTitle", "AudioFormatID", "NrOfDiscs" },
                values: new object[,]
                {
                    { 1, "Boney M Gold", 1, (byte)1 },
                    { 2, "Bravissimo 8", 1, (byte)1 },
                    { 3, "Bravissimo 6", 1, (byte)1 },
                    { 4, "Vegyes", 1, (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "SongID", "GenreID", "LanguageID", "SongLyric", "SongTitle" },
                values: new object[,]
                {
                    { 20, 1, 1, null, "Megamix" },
                    { 19, 1, 1, null, "Happy Song" },
                    { 18, 1, 1, null, "Kalimba De Luna" },
                    { 17, 1, 1, null, "Baby Do You Wanna Bump" },
                    { 16, 1, 1, null, "El Lute" },
                    { 15, 1, 1, null, "Felicidad" },
                    { 14, 1, 1, null, "Nightflight to Venus" },
                    { 13, 1, 1, null, "Still I'm Sad" },
                    { 12, 1, 1, null, "Gotta Go Home" },
                    { 11, 1, 1, null, "Mary's Boy Child / Oh My Lord" },
                    { 9, 1, 1, null, "Belfast" },
                    { 21, 4, 1, null, "It's Your Life" },
                    { 8, 1, 1, null, "Painter Man" },
                    { 7, 1, 1, null, "Hooray! Hooray! It's A Holi-holiday" },
                    { 6, 1, 1, null, "Ma Baker" },
                    { 5, 1, 1, null, "Rasputin" },
                    { 4, 1, 1, null, "Brown Girl in the Ring" },
                    { 3, 1, 1, null, "Sunny" },
                    { 2, 1, 1, null, "Daddy Cool" },
                    { 1, 1, 1, null, "Rivers of Babylon" },
                    { 10, 1, 1, null, "No Woman, No Cry" },
                    { 22, 4, 4, null, "Egyedül" }
                });

            migrationBuilder.InsertData(
                table: "AlbumSongs",
                columns: new[] { "AlbumID", "SongID", "Disc", "Note", "PlayTime", "TrackNr" },
                values: new object[,]
                {
                    { 1, 1, (byte)1, null, "04:15", 1 },
                    { 1, 14, (byte)1, null, "03:49", 14 },
                    { 1, 17, (byte)1, null, "02:25", 17 },
                    { 1, 13, (byte)1, null, "04:24", 13 },
                    { 1, 12, (byte)1, null, "02:30", 12 },
                    { 1, 18, (byte)1, null, "04:11", 18 },
                    { 1, 11, (byte)1, null, "04:01", 11 },
                    { 1, 10, (byte)1, null, "04:20", 10 },
                    { 1, 19, (byte)1, null, "03:56", 19 },
                    { 1, 9, (byte)1, null, "03:25", 9 },
                    { 1, 15, (byte)1, null, "02:50", 15 },
                    { 1, 20, (byte)1, null, "03:51", 20 },
                    { 1, 8, (byte)1, null, "03:16", 8 },
                    { 1, 16, (byte)1, null, "03:58", 16 },
                    { 1, 6, (byte)1, null, "04:05", 6 },
                    { 2, 21, (byte)1, null, "03:52", 20 },
                    { 1, 5, (byte)1, null, "04:24", 5 },
                    { 4, 22, (byte)1, null, "03:45", 3 },
                    { 1, 2, (byte)1, null, "03:26", 2 },
                    { 1, 4, (byte)1, null, "04:00", 4 },
                    { 1, 7, (byte)1, null, "3:55", 7 },
                    { 1, 3, (byte)1, null, "03:56", 3 },
                    { 2, 22, (byte)1, null, "03:45", 1 }
                });

            migrationBuilder.InsertData(
                table: "PerformerSongs",
                columns: new[] { "PerformerID", "SongID" },
                values: new object[,]
                {
                    { 1, 16 },
                    { 3, 21 },
                    { 1, 18 },
                    { 2, 21 },
                    { 1, 19 },
                    { 1, 17 },
                    { 1, 20 },
                    { 1, 12 },
                    { 1, 14 },
                    { 1, 13 },
                    { 4, 22 },
                    { 1, 11 },
                    { 1, 10 },
                    { 1, 9 },
                    { 1, 8 },
                    { 1, 7 },
                    { 1, 6 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 1, 1 },
                    { 1, 15 },
                    { 5, 22 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AudioFormatID",
                table: "Albums",
                column: "AudioFormatID");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSongs_SongID",
                table: "AlbumSongs",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerSongs_SongID",
                table: "PerformerSongs",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreID",
                table: "Songs",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_LanguageID",
                table: "Songs",
                column: "LanguageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumSongs");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PerformerSongs");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SongPerformers");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "AudioFormats");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
