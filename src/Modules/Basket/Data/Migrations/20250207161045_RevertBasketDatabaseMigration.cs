using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertBasketDatabaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_Id",
                schema: "basket",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItems",
                column: "ShoppingCartId",
                principalSchema: "basket",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_Id",
                schema: "basket",
                table: "ShoppingCartItems",
                column: "Id",
                principalSchema: "basket",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
