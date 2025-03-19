using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwipeCSAT.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductIdReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductID",
                table: "Reviews",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductID",
                table: "Reviews",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductEntityId",
                table: "Reviews",
                type: "uuid",
                nullable: true);

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
    }
}
