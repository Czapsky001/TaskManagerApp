using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSubTasksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTask_Tasks_ParentTaskId",
                table: "SubTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTask",
                table: "SubTask");

            migrationBuilder.RenameTable(
                name: "SubTask",
                newName: "SubTasks");

            migrationBuilder.RenameIndex(
                name: "IX_SubTask_ParentTaskId",
                table: "SubTasks",
                newName: "IX_SubTasks_ParentTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTasks",
                table: "SubTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Tasks_ParentTaskId",
                table: "SubTasks",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Tasks_ParentTaskId",
                table: "SubTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTasks",
                table: "SubTasks");

            migrationBuilder.RenameTable(
                name: "SubTasks",
                newName: "SubTask");

            migrationBuilder.RenameIndex(
                name: "IX_SubTasks_ParentTaskId",
                table: "SubTask",
                newName: "IX_SubTask_ParentTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTask",
                table: "SubTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTask_Tasks_ParentTaskId",
                table: "SubTask",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
