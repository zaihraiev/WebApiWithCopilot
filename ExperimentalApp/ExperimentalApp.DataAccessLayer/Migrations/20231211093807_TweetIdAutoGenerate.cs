using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExperimentalApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class TweetIdAutoGenerate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "85888866-0a51-4215-b7bb-2833a666e347", "AQAAAAEAACcQAAAAEGnTTueGcs977AKWZozWR+wwoUWRrEUdl7qL1PVqVD+DqMxIySAeNf4q5wQ5h9j3+w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "16522b3a-fde1-4903-b708-24921051a587", "AQAAAAEAACcQAAAAEOE9kKZ1ExGWCV73DRunBoEku2ojDCy5sh/afmBFJ6tG5+kZDAVQvGbMVezf/8jzmQ==" });
        }
    }
}
