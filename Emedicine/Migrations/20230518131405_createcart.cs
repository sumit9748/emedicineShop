using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emedicine.Migrations
{
    /// <inheritdoc />
    public partial class createcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalShopId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_MedicalShopId",
                table: "Carts",
                column: "MedicalShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Medicalshops_MedicalShopId",
                table: "Carts",
                column: "MedicalShopId",
                principalTable: "Medicalshops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Medicalshops_MedicalShopId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_MedicalShopId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "MedicalShopId",
                table: "Carts");
        }
    }
}
