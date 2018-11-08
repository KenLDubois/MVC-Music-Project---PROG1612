using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kdubois1_MVC_Music.Data.MUMigrations
{
    public partial class iit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MU");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "MU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                schema: "MU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                schema: "MU",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    YearProduced = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Albums_Genres_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "MU",
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Musicians",
                schema: "MU",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    MName = table.Column<string>(maxLength: 30, nullable: true),
                    LName = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    SIN = table.Column<string>(maxLength: 9, nullable: false),
                    InstrumentID = table.Column<int>(nullable: false),
                    StageName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Musicians_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "MU",
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                schema: "MU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    GenreID = table.Column<int>(nullable: false),
                    AlbumID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalSchema: "MU",
                        principalTable: "Albums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Genres_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "MU",
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plays",
                schema: "MU",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(nullable: false),
                    MusicianID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plays", x => new { x.InstrumentID, x.MusicianID });
                    table.ForeignKey(
                        name: "FK_Plays_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "MU",
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plays_Musicians_MusicianID",
                        column: x => x.MusicianID,
                        principalSchema: "MU",
                        principalTable: "Musicians",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                schema: "MU",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false),
                    MusicianID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => new { x.SongID, x.MusicianID });
                    table.ForeignKey(
                        name: "FK_Performances_Musicians_MusicianID",
                        column: x => x.MusicianID,
                        principalSchema: "MU",
                        principalTable: "Musicians",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Performances_Songs_SongID",
                        column: x => x.SongID,
                        principalSchema: "MU",
                        principalTable: "Songs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GenreID",
                schema: "MU",
                table: "Albums",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_Name_YearProduced",
                schema: "MU",
                table: "Albums",
                columns: new[] { "Name", "YearProduced" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_InstrumentID",
                schema: "MU",
                table: "Musicians",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_SIN",
                schema: "MU",
                table: "Musicians",
                column: "SIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Performances_MusicianID",
                schema: "MU",
                table: "Performances",
                column: "MusicianID");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_MusicianID",
                schema: "MU",
                table: "Plays",
                column: "MusicianID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumID",
                schema: "MU",
                table: "Songs",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreID",
                schema: "MU",
                table: "Songs",
                column: "GenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Performances",
                schema: "MU");

            migrationBuilder.DropTable(
                name: "Plays",
                schema: "MU");

            migrationBuilder.DropTable(
                name: "Songs",
                schema: "MU");

            migrationBuilder.DropTable(
                name: "Musicians",
                schema: "MU");

            migrationBuilder.DropTable(
                name: "Albums",
                schema: "MU");

            migrationBuilder.DropTable(
                name: "Instruments",
                schema: "MU");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "MU");
        }
    }
}
