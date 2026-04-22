using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistaMaster.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_RepsonsibleId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNotes_Customers_CustomerId",
                table: "ProjectNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Customers_CustomerId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Projects_ProjectId",
                table: "UserTasks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Versions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "UserTasks",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserTasks",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_ProjectId",
                table: "UserTasks",
                newName: "IX_UserTasks_ProjectID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserLogs",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Responsibles",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Requests",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Requests",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                newName: "IX_Requests_CustomerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RequestFiles",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projects",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ProjectNotes",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProjectNotes",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectNotes_CustomerId",
                table: "ProjectNotes",
                newName: "IX_ProjectNotes_CustomerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MyProperty",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Modules",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ErrorLogs",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "RepsonsibleId",
                table: "Actions",
                newName: "RepsonsibleID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Actions",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_RepsonsibleId",
                table: "Actions",
                newName: "IX_Actions_RepsonsibleID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ActionNotes",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_RepsonsibleID",
                table: "Actions",
                column: "RepsonsibleID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNotes_Customers_CustomerID",
                table: "ProjectNotes",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Customers_CustomerID",
                table: "Requests",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Projects_ProjectID",
                table: "UserTasks",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_RepsonsibleID",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNotes_Customers_CustomerID",
                table: "ProjectNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Customers_CustomerID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Projects_ProjectID",
                table: "UserTasks");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Versions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "UserTasks",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserTasks",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_ProjectID",
                table: "UserTasks",
                newName: "IX_UserTasks_ProjectId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Responsibles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Requests",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Requests",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_CustomerID",
                table: "Requests",
                newName: "IX_Requests_CustomerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RequestFiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Projects",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "ProjectNotes",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProjectNotes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectNotes_CustomerID",
                table: "ProjectNotes",
                newName: "IX_ProjectNotes_CustomerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "MyProperty",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Modules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ErrorLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RepsonsibleID",
                table: "Actions",
                newName: "RepsonsibleId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Actions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_RepsonsibleID",
                table: "Actions",
                newName: "IX_Actions_RepsonsibleId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ActionNotes",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_RepsonsibleId",
                table: "Actions",
                column: "RepsonsibleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNotes_Customers_CustomerId",
                table: "ProjectNotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Customers_CustomerId",
                table: "Requests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Projects_ProjectId",
                table: "UserTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
