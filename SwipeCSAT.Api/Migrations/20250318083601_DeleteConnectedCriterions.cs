using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwipeCSAT.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConnectedCriterions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriterionRatings_Reviews_Id",
                table: "CriterionRatings");

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "CriterionRatings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriterionRatings_ReviewId",
                table: "CriterionRatings",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionRatings_Reviews_ReviewId",
                table: "CriterionRatings",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriterionRatings_Reviews_ReviewId",
                table: "CriterionRatings");

            migrationBuilder.DropIndex(
                name: "IX_CriterionRatings_ReviewId",
                table: "CriterionRatings");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "CriterionRatings");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionRatings_Reviews_Id",
                table: "CriterionRatings",
                column: "Id",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
