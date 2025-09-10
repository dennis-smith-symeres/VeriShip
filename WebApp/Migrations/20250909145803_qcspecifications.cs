using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeriShip.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class qcspecifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropForeignKey(
                name: "FK_QcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "QcRequestItemResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_QcRequestItems_ItemId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_ItemId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_QcRequestItemResults_ItemId_CheckId",
                table: "QcRequestItemResults");

            migrationBuilder.DropIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_CheckId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "CheckId",
                table: "QcRequestItemResults");

            migrationBuilder.DropColumn(
                name: "CheckId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.AlterColumn<int>(
                name: "QcSpecificationId",
                table: "QcRequestItemResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QcRequestItemResults_ItemId_QcSpecificationId",
                table: "QcRequestItemResults",
                columns: new[] { "ItemId", "QcSpecificationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                columns: new[] { "ProjectId", "QcSpecificationId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "QcRequestItemResults",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.DropForeignKey(
                name: "FK_QcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "QcRequestItemResults");

            migrationBuilder.DropIndex(
                name: "IX_QcRequestItemResults_ItemId_QcSpecificationId",
                table: "QcRequestItemResults");

            migrationBuilder.DropIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_QcSpecificationId",
                table: "ProjectQcRequestItemResults");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Stages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QcSpecificationId",
                table: "QcRequestItemResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CheckId",
                table: "QcRequestItemResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CheckId",
                table: "ProjectQcRequestItemResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ItemId",
                table: "Stages",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QcRequestItemResults_ItemId_CheckId",
                table: "QcRequestItemResults",
                columns: new[] { "ItemId", "CheckId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_CheckId",
                table: "ProjectQcRequestItemResults",
                columns: new[] { "ProjectId", "CheckId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QcRequestItemResults_QcSpecifications_QcSpecificationId",
                table: "QcRequestItemResults",
                column: "QcSpecificationId",
                principalTable: "QcSpecifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_QcRequestItems_ItemId",
                table: "Stages",
                column: "ItemId",
                principalTable: "QcRequestItems",
                principalColumn: "Id");
        }
    }
}
