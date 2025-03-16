using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwipeCSAT.Api.Migrations
{
    /// <inheritdoc />
    public partial class TableCriterionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCriterionEntity_Categories_CategoriesId",
                table: "CategoryEntityCriterionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCriterionEntity_Criterions_CriterionsId",
                table: "CategoryEntityCriterionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntityCriterionEntity",
                table: "CategoryEntityCriterionEntity");

            migrationBuilder.RenameTable(
                name: "CategoryEntityCriterionEntity",
                newName: "CategoryCriterion");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryEntityCriterionEntity_CriterionsId",
                table: "CategoryCriterion",
                newName: "IX_CategoryCriterion_CriterionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryCriterion",
                table: "CategoryCriterion",
                columns: new[] { "CategoriesId", "CriterionsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCriterion_Categories_CategoriesId",
                table: "CategoryCriterion",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCriterion_Criterions_CriterionsId",
                table: "CategoryCriterion",
                column: "CriterionsId",
                principalTable: "Criterions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCriterion_Categories_CategoriesId",
                table: "CategoryCriterion");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCriterion_Criterions_CriterionsId",
                table: "CategoryCriterion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryCriterion",
                table: "CategoryCriterion");

            migrationBuilder.RenameTable(
                name: "CategoryCriterion",
                newName: "CategoryEntityCriterionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryCriterion_CriterionsId",
                table: "CategoryEntityCriterionEntity",
                newName: "IX_CategoryEntityCriterionEntity_CriterionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntityCriterionEntity",
                table: "CategoryEntityCriterionEntity",
                columns: new[] { "CategoriesId", "CriterionsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEntityCriterionEntity_Categories_CategoriesId",
                table: "CategoryEntityCriterionEntity",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEntityCriterionEntity_Criterions_CriterionsId",
                table: "CategoryEntityCriterionEntity",
                column: "CriterionsId",
                principalTable: "Criterions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
