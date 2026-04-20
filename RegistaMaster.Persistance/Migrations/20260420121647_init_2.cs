using System;
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
                name: "FK_ActionNote_Actions_ActionId",
                table: "ActionNote");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFile_Requests_RequestId",
                table: "RequestFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestFile",
                table: "RequestFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActionNote",
                table: "ActionNote");

            migrationBuilder.RenameTable(
                name: "RequestFile",
                newName: "RequestFiles");

            migrationBuilder.RenameTable(
                name: "ActionNote",
                newName: "ActionNotes");

            migrationBuilder.RenameColumn(
                name: "AuthorizonStatus",
                table: "Users",
                newName: "AuthorizationStatus");

            migrationBuilder.RenameIndex(
                name: "IX_RequestFile_RequestId",
                table: "RequestFiles",
                newName: "IX_RequestFiles_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionNote_ActionId",
                table: "ActionNotes",
                newName: "IX_ActionNotes_ActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestFiles",
                table: "RequestFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActionNotes",
                table: "ActionNotes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Responsibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ObjectStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CetegoryId = table.Column<int>(type: "int", nullable: true),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: true),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    ProjetId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ObjectStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTasks_Versions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_ProjectId",
                table: "UserTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_VersionId",
                table: "UserTasks",
                column: "VersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionNotes_Actions_ActionId",
                table: "ActionNotes",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFiles_Requests_RequestId",
                table: "RequestFiles",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionNotes_Actions_ActionId",
                table: "ActionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFiles_Requests_RequestId",
                table: "RequestFiles");

            migrationBuilder.DropTable(
                name: "Responsibles");

            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestFiles",
                table: "RequestFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActionNotes",
                table: "ActionNotes");

            migrationBuilder.RenameTable(
                name: "RequestFiles",
                newName: "RequestFile");

            migrationBuilder.RenameTable(
                name: "ActionNotes",
                newName: "ActionNote");

            migrationBuilder.RenameColumn(
                name: "AuthorizationStatus",
                table: "Users",
                newName: "AuthorizonStatus");

            migrationBuilder.RenameIndex(
                name: "IX_RequestFiles_RequestId",
                table: "RequestFile",
                newName: "IX_RequestFile_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionNotes_ActionId",
                table: "ActionNote",
                newName: "IX_ActionNote_ActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestFile",
                table: "RequestFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActionNote",
                table: "ActionNote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionNote_Actions_ActionId",
                table: "ActionNote",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFile_Requests_RequestId",
                table: "RequestFile",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
