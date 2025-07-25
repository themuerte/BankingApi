﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingAPI.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSexToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Clients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Clients");
        }
    }
}
