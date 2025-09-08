using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeriShip.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Models4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Project_ProjectId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Project_ProjectId",
                table: "Boxes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectResult_Project_ProjectId",
                table: "ProjectResult");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectResult_QcSpecifications_QcSpecificationId",
                table: "ProjectResult");

            migrationBuilder.DropForeignKey(
                name: "FK_QcRequests_Project_ProjectId",
                table: "QcRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectResult",
                table: "ProjectResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "ProjectResult",
                newName: "ProjectQcRequestItemResults");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectResult_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                newName: "IX_ProjectQcRequestItemResults_QcSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectResult_ProjectId_CheckId",
                table: "ProjectQcRequestItemResults",
                newName: "IX_ProjectQcRequestItemResults_ProjectId_CheckId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ProjectNumber",
                table: "Projects",
                newName: "IX_Projects_ProjectNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectQcRequestItemResults",
                table: "ProjectQcRequestItemResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Projects_ProjectId",
                table: "Addresses",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Projects_ProjectId",
                table: "Boxes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_QcRequests_Projects_ProjectId",
                table: "QcRequests",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Projects_ProjectId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Projects_ProjectId",
                table: "Boxes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQcRequestItemResults_Projects_ProjectId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropForeignKey(
                name: "FK_QcRequests_Projects_ProjectId",
                table: "QcRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectQcRequestItemResults",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "ProjectQcRequestItemResults",
                newName: "ProjectResult");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ProjectNumber",
                table: "Project",
                newName: "IX_Project_ProjectNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectQcRequestItemResults_QcSpecificationId",
                table: "ProjectResult",
                newName: "IX_ProjectResult_QcSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_CheckId",
                table: "ProjectResult",
                newName: "IX_ProjectResult_ProjectId_CheckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectResult",
                table: "ProjectResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Project_ProjectId",
                table: "Addresses",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Project_ProjectId",
                table: "Boxes",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectResult_Project_ProjectId",
                table: "ProjectResult",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectResult_QcSpecifications_QcSpecificationId",
                table: "ProjectResult",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QcRequests_Project_ProjectId",
                table: "QcRequests",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
