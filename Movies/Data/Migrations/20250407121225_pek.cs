using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class pek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    DirectorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfStaff = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studios_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studios_MovieId",
                table: "Studios",
                column: "MovieId",
                unique: true);
        }
    }
}
