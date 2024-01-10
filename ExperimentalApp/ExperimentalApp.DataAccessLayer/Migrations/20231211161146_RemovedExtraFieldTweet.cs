using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExperimentalApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedExtraFieldTweet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retweets",
                table: "Tweets");

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "Tweets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "013ab800-05d2-4f0d-8489-3e096446da9f", "AQAAAAEAACcQAAAAEEhuit9rsQuqS+FKlq2QjyFHDLuNppDbDnwRlatYsVBlIuhBdUQcSDwUcZLFzXpN6Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Likes",
                table: "Tweets",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "Retweets",
                table: "Tweets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "85888866-0a51-4215-b7bb-2833a666e347", "AQAAAAEAACcQAAAAEGnTTueGcs977AKWZozWR+wwoUWRrEUdl7qL1PVqVD+DqMxIySAeNf4q5wQ5h9j3+w==" });
        }
    }
}
