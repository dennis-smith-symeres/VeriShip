using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeriShip.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalsEid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoxTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Dimensions_LxWxH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeclaredWeight_Kg = table.Column<int>(type: "int", nullable: true),
                    DryIce = table.Column<bool>(type: "bit", nullable: false),
                    ColdPacks = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureLogger = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientEmails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CcEmails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BccEmails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InternalCheckers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalBoxCheckers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentIdsLabels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentIdsAddressLabels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentIdsPackingLists = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentIdCoA = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectResults",
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
                    table.PrimaryKey("PK_ProjectResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectResults_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectResults_QcSpecifications_QcSpecificationId",
                        column: x => x.QcSpecificationId,
                        principalTable: "QcSpecifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QcRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QcRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QcRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    BoxWeight = table.Column<int>(type: "int", nullable: true),
                    BoxDimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DryIce = table.Column<bool>(type: "bit", nullable: true),
                    ColdPacks = table.Column<bool>(type: "bit", nullable: true),
                    TemperatureLogger = table.Column<bool>(type: "bit", nullable: true),
                    BoxTypeId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxes_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boxes_BoxTypes_BoxTypeId",
                        column: x => x.BoxTypeId,
                        principalTable: "BoxTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boxes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QcRequestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    QcRequestId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StructureAsSvg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StructureAsMol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StructureSalts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QcRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QcRequestItems_QcRequests_QcRequestId",
                        column: x => x.QcRequestId,
                        principalTable: "QcRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoxId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextStatusId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousStatusId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxStages_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureAsMol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureAsSvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxId = table.Column<int>(type: "int", nullable: false),
                    RequestItemId = table.Column<int>(type: "int", nullable: true),
                    Mw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraFields = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxItems_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxItems_QcRequestItems_RequestItemId",
                        column: x => x.RequestItemId,
                        principalTable: "QcRequestItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QcRequestItemResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_QcRequestItemResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QcRequestItemResults_QcRequestItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "QcRequestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QcRequestItemResults_QcSpecifications_QcSpecificationId",
                        column: x => x.QcSpecificationId,
                        principalTable: "QcSpecifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QcRequestId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextStatusId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousStatusId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_QcRequestItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "QcRequestItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stages_QcRequests_QcRequestId",
                        column: x => x.QcRequestId,
                        principalTable: "QcRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProjectId",
                table: "Addresses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_AddressId",
                table: "Boxes",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_BoxTypeId",
                table: "Boxes",
                column: "BoxTypeId",
                unique: true,
                filter: "[BoxTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_ProjectId",
                table: "Boxes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxItems_BoxId",
                table: "BoxItems",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxItems_RequestItemId",
                table: "BoxItems",
                column: "RequestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxStages_BoxId",
                table: "BoxStages",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_ProjectId_CheckId",
                table: "ProjectResults",
                columns: new[] { "ProjectId", "CheckId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResults_QcSpecificationId",
                table: "ProjectResults",
                column: "QcSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectNumber",
                table: "Projects",
                column: "ProjectNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QcRequestItemResults_ItemId_CheckId",
                table: "QcRequestItemResults",
                columns: new[] { "ItemId", "CheckId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QcRequestItemResults_QcSpecificationId",
                table: "QcRequestItemResults",
                column: "QcSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_QcRequestItems_QcRequestId",
                table: "QcRequestItems",
                column: "QcRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_QcRequests_ProjectId",
                table: "QcRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ItemId",
                table: "Stages",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_QcRequestId",
                table: "Stages",
                column: "QcRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "BoxItems");

            migrationBuilder.DropTable(
                name: "BoxStages");

            migrationBuilder.DropTable(
                name: "EmailMessages");

            migrationBuilder.DropTable(
                name: "ProjectResults");

            migrationBuilder.DropTable(
                name: "QcRequestItemResults");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "QcRequestItems");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BoxTypes");

            migrationBuilder.DropTable(
                name: "QcRequests");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
