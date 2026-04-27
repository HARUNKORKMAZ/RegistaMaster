using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistaMaster.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_RepsonsibleID",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_RepsonsibleID",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "RepsonsibleID",
                table: "Actions");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ResponsibleID",
                table: "Actions",
                column: "ResponsibleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_ResponsibleID",
                table: "Actions",
                column: "ResponsibleID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_ResponsibleID",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_ResponsibleID",
                table: "Actions");

            migrationBuilder.AddColumn<int>(
                name: "RepsonsibleID",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actions_RepsonsibleID",
                table: "Actions",
                column: "RepsonsibleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_RepsonsibleID",
                table: "Actions",
                column: "RepsonsibleID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
