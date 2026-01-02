using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDtosAndValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations",
                column: "DiningTableId",
                principalTable: "DiningTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations",
                column: "DiningTableId",
                principalTable: "DiningTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
