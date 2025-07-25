using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingAPI.Api.Migrations
{
    /// <inheritdoc />
    public partial class addFieldsMissings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transactions",
                newName: "Balance");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Transactions",
                newName: "Description");
        }
    }
}
