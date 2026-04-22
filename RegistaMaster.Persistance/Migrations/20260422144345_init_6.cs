using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistaMaster.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionNotes_Actions_ActionId",
                table: "ActionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Requests_RequestId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_Projects_ProjectId",
                table: "ErrorLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Projects_ProjectId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_Projects_ProjectId",
                table: "MyProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNotes_Projects_ProjectId",
                table: "ProjectNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFiles_Requests_RequestId",
                table: "RequestFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Projects_ProjectId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Versions_VersionId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_Projects_ProjectId",
                table: "UserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Versions_VersionId",
                table: "UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Projects_ProjectId",
                table: "Versions");

            migrationBuilder.DropColumn(
                name: "ProjetId",
                table: "UserTasks");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Versions",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Versions_ProjectId",
                table: "Versions",
                newName: "IX_Versions_ProjectID");

            migrationBuilder.RenameColumn(
                name: "VersionId",
                table: "UserTasks",
                newName: "VersionID");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "UserTasks",
                newName: "ModuleID");

            migrationBuilder.RenameColumn(
                name: "CetegoryId",
                table: "UserTasks",
                newName: "CetegoryID");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_VersionId",
                table: "UserTasks",
                newName: "IX_UserTasks_VersionID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "UserLogs",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "UserLogs",
                newName: "MemberID");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "UserLogs",
                newName: "ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogs_ProjectId",
                table: "UserLogs",
                newName: "IX_UserLogs_ProjectID");

            migrationBuilder.RenameColumn(
                name: "VersionId",
                table: "Requests",
                newName: "VersionID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Requests",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "NotificationTypeId",
                table: "Requests",
                newName: "NotificationTypeID");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Requests",
                newName: "NotificationID");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "Requests",
                newName: "ModuleID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Requests",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_VersionId",
                table: "Requests",
                newName: "IX_Requests_VersionID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ProjectId",
                table: "Requests",
                newName: "IX_Requests_ProjectID");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "RequestFiles",
                newName: "RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_RequestFiles_RequestId",
                table: "RequestFiles",
                newName: "IX_RequestFiles_RequestID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectNotes",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectNotes_ProjectId",
                table: "ProjectNotes",
                newName: "IX_ProjectNotes_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "MyProperty",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_ProjectId",
                table: "MyProperty",
                newName: "IX_MyProperty_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Modules",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_ProjectId",
                table: "Modules",
                newName: "IX_Modules_ProjectID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ErrorLogs",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "ErrorLogs",
                newName: "MemberID");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ErrorLogs",
                newName: "ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_ErrorLogs_ProjectId",
                table: "ErrorLogs",
                newName: "IX_ErrorLogs_ProjectID");

            migrationBuilder.RenameColumn(
                name: "CustomerDescriptionId",
                table: "Customers",
                newName: "CustomerDescriptionID");

            migrationBuilder.RenameColumn(
                name: "ResponsibleId",
                table: "Actions",
                newName: "ResponsibleID");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Actions",
                newName: "RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_RequestId",
                table: "Actions",
                newName: "IX_Actions_RequestID");

            migrationBuilder.RenameColumn(
                name: "ActionId",
                table: "ActionNotes",
                newName: "ActionID");

            migrationBuilder.RenameIndex(
                name: "IX_ActionNotes_ActionId",
                table: "ActionNotes",
                newName: "IX_ActionNotes_ActionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionNotes_Actions_ActionID",
                table: "ActionNotes",
                column: "ActionID",
                principalTable: "Actions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Requests_RequestID",
                table: "Actions",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_Projects_ProjectID",
                table: "ErrorLogs",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Projects_ProjectID",
                table: "Modules",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_Projects_ProjectID",
                table: "MyProperty",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNotes_Projects_ProjectID",
                table: "ProjectNotes",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFiles_Requests_RequestID",
                table: "RequestFiles",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Projects_ProjectID",
                table: "Requests",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Versions_VersionID",
                table: "Requests",
                column: "VersionID",
                principalTable: "Versions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_Projects_ProjectID",
                table: "UserLogs",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Versions_VersionID",
                table: "UserTasks",
                column: "VersionID",
                principalTable: "Versions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Projects_ProjectID",
                table: "Versions",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionNotes_Actions_ActionID",
                table: "ActionNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Requests_RequestID",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_Projects_ProjectID",
                table: "ErrorLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Projects_ProjectID",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_Projects_ProjectID",
                table: "MyProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNotes_Projects_ProjectID",
                table: "ProjectNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFiles_Requests_RequestID",
                table: "RequestFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Projects_ProjectID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Versions_VersionID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_Projects_ProjectID",
                table: "UserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Versions_VersionID",
                table: "UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Projects_ProjectID",
                table: "Versions");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Versions",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Versions_ProjectID",
                table: "Versions",
                newName: "IX_Versions_ProjectId");

            migrationBuilder.RenameColumn(
                name: "VersionID",
                table: "UserTasks",
                newName: "VersionId");

            migrationBuilder.RenameColumn(
                name: "ModuleID",
                table: "UserTasks",
                newName: "ModuleId");

            migrationBuilder.RenameColumn(
                name: "CetegoryID",
                table: "UserTasks",
                newName: "CetegoryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_VersionID",
                table: "UserTasks",
                newName: "IX_UserTasks_VersionId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "UserLogs",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "UserLogs",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "UserLogs",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogs_ProjectID",
                table: "UserLogs",
                newName: "IX_UserLogs_ProjectId");

            migrationBuilder.RenameColumn(
                name: "VersionID",
                table: "Requests",
                newName: "VersionId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Requests",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "NotificationTypeID",
                table: "Requests",
                newName: "NotificationTypeId");

            migrationBuilder.RenameColumn(
                name: "NotificationID",
                table: "Requests",
                newName: "NotificationId");

            migrationBuilder.RenameColumn(
                name: "ModuleID",
                table: "Requests",
                newName: "ModuleId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Requests",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_VersionID",
                table: "Requests",
                newName: "IX_Requests_VersionId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ProjectID",
                table: "Requests",
                newName: "IX_Requests_ProjectId");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "RequestFiles",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestFiles_RequestID",
                table: "RequestFiles",
                newName: "IX_RequestFiles_RequestId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "ProjectNotes",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectNotes_ProjectID",
                table: "ProjectNotes",
                newName: "IX_ProjectNotes_ProjectId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "MyProperty",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_ProjectID",
                table: "MyProperty",
                newName: "IX_MyProperty_ProjectId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Modules",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_ProjectID",
                table: "Modules",
                newName: "IX_Modules_ProjectId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "ErrorLogs",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "ErrorLogs",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "ErrorLogs",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ErrorLogs_ProjectID",
                table: "ErrorLogs",
                newName: "IX_ErrorLogs_ProjectId");

            migrationBuilder.RenameColumn(
                name: "CustomerDescriptionID",
                table: "Customers",
                newName: "CustomerDescriptionId");

            migrationBuilder.RenameColumn(
                name: "ResponsibleID",
                table: "Actions",
                newName: "ResponsibleId");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "Actions",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_RequestID",
                table: "Actions",
                newName: "IX_Actions_RequestId");

            migrationBuilder.RenameColumn(
                name: "ActionID",
                table: "ActionNotes",
                newName: "ActionId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionNotes_ActionID",
                table: "ActionNotes",
                newName: "IX_ActionNotes_ActionId");

            migrationBuilder.AddColumn<int>(
                name: "ProjetId",
                table: "UserTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionNotes_Actions_ActionId",
                table: "ActionNotes",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Requests_RequestId",
                table: "Actions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_Projects_ProjectId",
                table: "ErrorLogs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Projects_ProjectId",
                table: "Modules",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_Projects_ProjectId",
                table: "MyProperty",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNotes_Projects_ProjectId",
                table: "ProjectNotes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFiles_Requests_RequestId",
                table: "RequestFiles",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Projects_ProjectId",
                table: "Requests",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Versions_VersionId",
                table: "Requests",
                column: "VersionId",
                principalTable: "Versions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_Projects_ProjectId",
                table: "UserLogs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Versions_VersionId",
                table: "UserTasks",
                column: "VersionId",
                principalTable: "Versions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Projects_ProjectId",
                table: "Versions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
