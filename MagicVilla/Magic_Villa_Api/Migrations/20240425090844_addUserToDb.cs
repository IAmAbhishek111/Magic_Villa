﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_Villa_Api.Migrations
{
    /// <inheritdoc />
    public partial class addUserToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 25, 14, 38, 43, 250, DateTimeKind.Local).AddTicks(6775), "https://dotnetmastery.com/bluevillaimages/villa3.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 25, 14, 38, 43, 250, DateTimeKind.Local).AddTicks(6790), "https://dotnetmastery.com/bluevillaimages/villa1.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 25, 14, 38, 43, 250, DateTimeKind.Local).AddTicks(6792), "https://dotnetmastery.com/bluevillaimages/villa4.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 25, 14, 38, 43, 250, DateTimeKind.Local).AddTicks(6794), "https://dotnetmastery.com/bluevillaimages/villa5.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 25, 14, 38, 43, 250, DateTimeKind.Local).AddTicks(6796), "https://dotnetmastery.com/bluevillaimages/villa2.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 23, 9, 29, 12, 785, DateTimeKind.Local).AddTicks(4229), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 23, 9, 29, 12, 785, DateTimeKind.Local).AddTicks(4243), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 23, 9, 29, 12, 785, DateTimeKind.Local).AddTicks(4245), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 23, 9, 29, 12, 785, DateTimeKind.Local).AddTicks(4247), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 4, 23, 9, 29, 12, 785, DateTimeKind.Local).AddTicks(4249), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg" });
        }
    }
}
