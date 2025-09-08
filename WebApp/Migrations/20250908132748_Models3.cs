using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeriShip.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Models3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Projects_ProjectId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Projects_ProjectId",
                table: "Boxes");

            migrationBuilder.DropForeignKey(
                name: "FK_QcRequests_Projects_ProjectId",
                table: "QcRequests");

            migrationBuilder.DropTable(
                name: "ProjectQcRequestItemResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ProjectNumber",
                table: "Project",
                newName: "IX_Project_ProjectNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acceptance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CheckId = table.Column<int>(type: "int", nullable: false),
                    QcSpecificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectResult_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectResult_QcSpecifications_QcSpecificationId",
                        column: x => x.QcSpecificationId,
                        principalTable: "QcSpecifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResult_ProjectId_CheckId",
                table: "ProjectResult",
                columns: new[] { "ProjectId", "CheckId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResult_QcSpecificationId",
                table: "ProjectResult",
                column: "QcSpecificationId");

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
                name: "FK_QcRequests_Project_ProjectId",
                table: "QcRequests",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Project_ProjectId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Project_ProjectId",
                table: "Boxes");

            migrationBuilder.DropForeignKey(
                name: "FK_QcRequests_Project_ProjectId",
                table: "QcRequests");

            migrationBuilder.DropTable(
                name: "ProjectResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ProjectNumber",
                table: "Projects",
                newName: "IX_Projects_ProjectNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectQcRequestItemResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    QcSpecificationId = table.Column<int>(type: "int", nullable: true),
                    Acceptance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CheckId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectQcRequestItemResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectQcRequestItemResults_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectQcRequestItemResults_QcSpecifications_QcSpecificationId",
                        column: x => x.QcSpecificationId,
                        principalTable: "QcSpecifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQcRequestItemResults_ProjectId_CheckId",
                table: "ProjectQcRequestItemResults",
                columns: new[] { "ProjectId", "CheckId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQcRequestItemResults_QcSpecificationId",
                table: "ProjectQcRequestItemResults",
                column: "QcSpecificationId");

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
                name: "FK_QcRequests_Projects_ProjectId",
                table: "QcRequests",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
