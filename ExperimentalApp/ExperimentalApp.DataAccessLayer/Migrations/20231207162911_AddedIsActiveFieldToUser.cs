using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExperimentalApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5623cead-2c55-4b59-937e-61d172eb30c9", "AQAAAAEAACcQAAAAEND1TWHX29C8DHo7oKVgCIs91fAmNbmvBpQVNsNIMw3cPqJ7nK1jarZhfGJ1G2DDYA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8704f09-8fbe-49c8-bba8-14ac46bd8f68", "AQAAAAEAACcQAAAAEHKT6f+uOKzyBR7f/iIqOMGTfwNehV0zfkQkeNPkgNu601KFPmTDGKS9wm+q+V20+Q==" });
        }
    }
}
