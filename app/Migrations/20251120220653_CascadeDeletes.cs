using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerApp.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_AspNetUsers_CreatedByUserId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Tasks_ParentTaskId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_WorkTables_WorkTableId",
                table: "Tabs");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_AspNetUsers_CreatedByUserId",
                table: "SubTasks",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Tasks_ParentTaskId",
                table: "SubTasks",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_WorkTables_WorkTableId",
                table: "Tabs",
                column: "WorkTableId",
                principalTable: "WorkTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_AspNetUsers_CreatedByUserId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Tasks_ParentTaskId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_WorkTables_WorkTableId",
                table: "Tabs");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_AspNetUsers_CreatedByUserId",
                table: "SubTasks",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Tasks_ParentTaskId",
                table: "SubTasks",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_WorkTables_WorkTableId",
                table: "Tabs",
                column: "WorkTableId",
                principalTable: "WorkTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
