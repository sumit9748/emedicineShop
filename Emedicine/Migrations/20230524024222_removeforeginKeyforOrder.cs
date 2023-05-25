using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emedicine.Migrations
{
    /// <inheritdoc />
    public partial class removeforeginKeyforOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Medicalshops_MedicalShopId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MedicalShopId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MedicalShopId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalShopId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MedicalShopId",
                table: "Orders",
                column: "MedicalShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Medicalshops_MedicalShopId",
                table: "Orders",
                column: "MedicalShopId",
                principalTable: "Medicalshops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
