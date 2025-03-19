using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwipeCSAT.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovingLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriterionRatings_Criterions_CriterionId",
                table: "CriterionRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CriterionRatings_Products_ProductId",
                table: "CriterionRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_Id",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_CriterionRatings_CriterionId",
                table: "CriterionRatings");

            migrationBuilder.DropIndex(
                name: "IX_CriterionRatings_ProductId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "CriterionId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "CriterionRatings");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductEntityId",
                table: "Reviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriterionName",
                table: "CriterionRatings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductEntityId",
                table: "Reviews",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductEntityId",
                table: "Reviews",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductEntityId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductEntityId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CriterionName",
                table: "CriterionRatings");

            migrationBuilder.AddColumn<Guid>(
                name: "CriterionId",
                table: "CriterionRatings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "CriterionRatings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "CriterionRatings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CriterionRatings_CriterionId",
                table: "CriterionRatings",
                column: "CriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionRatings_ProductId",
                table: "CriterionRatings",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionRatings_Criterions_CriterionId",
                table: "CriterionRatings",
                column: "CriterionId",
                principalTable: "Criterions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionRatings_Products_ProductId",
                table: "CriterionRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_Id",
                table: "Reviews",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
