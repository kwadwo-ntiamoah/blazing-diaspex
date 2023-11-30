#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class TryMe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profile_OwnerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Profile_OwnerId",
                table: "Replies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profile",
                table: "Profile");

            migrationBuilder.RenameTable(
                name: "Profile",
                newName: "Profiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Type_Title",
                table: "Categories",
                columns: new[] { "Type", "Title" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profiles_OwnerId",
                table: "Posts",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Profiles_OwnerId",
                table: "Replies",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profiles_OwnerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Profiles_OwnerId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Type_Title",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profile",
                table: "Profile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profile_OwnerId",
                table: "Posts",
                column: "OwnerId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Profile_OwnerId",
                table: "Replies",
                column: "OwnerId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
