﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class VideosAddedToMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Trailer",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trailer",
                table: "Movie");
        }
    }
}
