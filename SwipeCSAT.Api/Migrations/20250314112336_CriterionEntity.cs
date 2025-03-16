using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwipeCSAT.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriterionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriterionRatings_Products_ProductEntityId",
                table: "CriterionRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CriterionRatings",
                table: "CriterionRatings");

            migrationBuilder.DropIndex(
                name: "IX_CriterionRatings_ProductEntityId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "CriterionName",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "Criterions",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CriterionRatings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CriterionId",
                table: "CriterionRatings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CriterionRatings",
                table: "CriterionRatings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Criterions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEntityCriterionEntity",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriterionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntityCriterionEntity", x => new { x.CategoriesId, x.CriterionsId });
                    table.ForeignKey(
                        name: "FK_CategoryEntityCriterionEntity_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEntityCriterionEntity_Criterions_CriterionsId",
                        column: x => x.CriterionsId,
                        principalTable: "Criterions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriterionEntityProductEntity",
                columns: table => new
                {
                    CriterionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriterionEntityProductEntity", x => new { x.CriterionsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CriterionEntityProductEntity_Criterions_CriterionsId",
                        column: x => x.CriterionsId,
                        principalTable: "Criterions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriterionEntityProductEntity_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriterionRatings_CriterionId",
                table: "CriterionRatings",
                column: "CriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityCriterionEntity_CriterionsId",
                table: "CategoryEntityCriterionEntity",
                column: "CriterionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionEntityProductEntity_ProductsId",
                table: "CriterionEntityProductEntity",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionRatings_Criterions_CriterionId",
                table: "CriterionRatings",
                column: "CriterionId",
                principalTable: "Criterions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriterionRatings_Criterions_CriterionId",
                table: "CriterionRatings");

            migrationBuilder.DropTable(
                name: "CategoryEntityCriterionEntity");

            migrationBuilder.DropTable(
                name: "CriterionEntityProductEntity");

            migrationBuilder.DropTable(
                name: "Criterions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CriterionRatings",
                table: "CriterionRatings");

            migrationBuilder.DropIndex(
                name: "IX_CriterionRatings_CriterionId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "CriterionId",
                table: "CriterionRatings");

            migrationBuilder.AddColumn<string>(
                name: "CriterionName",
                table: "CriterionRatings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductEntityId",
                table: "CriterionRatings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Criterions",
                table: "Categories",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CriterionRatings",
                table: "CriterionRatings",
                column: "CriterionName");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionRatings_ProductEntityId",
                table: "CriterionRatings",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionRatings_Products_ProductEntityId",
                table: "CriterionRatings",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
