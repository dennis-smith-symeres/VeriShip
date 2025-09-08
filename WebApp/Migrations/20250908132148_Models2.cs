using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeriShip.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Models2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectResults_Projects_ProjectId",
                table: "ProjectResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectResults_QcSpecifications_QcSpecificationId",
                table: "ProjectResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectResults",
                table: "ProjectResults");

            migrationBuilder.RenameTable(
                name: "ProjectResults",
                newName: "ProjectQcRequestItemResults");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectResults_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                newName: "IX_ProjectQcRequestItemResults_QcSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectResults_ProjectId_CheckId",
                table: "ProjectQcRequestItemResults",
                newName: "IX_ProjectQcRequestItemResults_ProjectId_CheckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectQcRequestItemResults",
                table: "ProjectQcRequestItemResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQcRequestItemResults_Projects_ProjectId",
                table: "ProjectQcRequestItemResults",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQcRequestItemResults_Projects_ProjectId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectQcRequestItemResults",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.RenameTable(
                name: "ProjectQcRequestItemResults",
                newName: "ProjectResults");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectQcRequestItemResults_QcSpecificationId",
                table: "ProjectResults",
                newName: "IX_ProjectResults_QcSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_CheckId",
                table: "ProjectResults",
                newName: "IX_ProjectResults_ProjectId_CheckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectResults",
                table: "ProjectResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectResults_Projects_ProjectId",
                table: "ProjectResults",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectResults_QcSpecifications_QcSpecificationId",
                table: "ProjectResults",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id");
        }
    }
}
