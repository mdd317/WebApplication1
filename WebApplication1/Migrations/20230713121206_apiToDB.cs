using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class apiToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Movie",
                newName: "release_date");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Movie",
                newName: "poster_path");

            migrationBuilder.AddColumn<string>(
                name: "Popularity",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "original_language",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "original_title",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "video",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "vote_average",
                table: "Movie",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "vote_count",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "original_language",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "original_title",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "video",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "vote_average",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "vote_count",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "release_date",
                table: "Movie",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "poster_path",
                table: "Movie",
                newName: "ImageURL");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Movie",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
