using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.Migrations.Data
{
    /// <inheritdoc />
    public partial class DataCountrySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"), "Brazil" },
                    { new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "South Africa" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { new Guid("01409dbc-d3c4-4c77-8b2e-99ae560e7a4b"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Northern Cape" },
                    { new Guid("2c70ae23-454b-4916-8a40-17e28a75ffec"), new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"), "Central-West" },
                    { new Guid("49a3f3ab-57e7-422a-8b60-b45e068f0b3c"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "South Africa" },
                    { new Guid("500f8d34-f7e0-4ca2-9f7a-1d41ae166af3"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Free State" },
                    { new Guid("63b2726a-ac0b-4116-bd49-6d79b1f3001d"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "KwaZulu-Natal" },
                    { new Guid("65da3eab-f9d2-4e31-aeb9-3c9b8bd249cd"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Western Cape" },
                    { new Guid("6e62fbf8-c483-49cb-b028-dc506253b5bc"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Mpumalanga" },
                    { new Guid("77d7ac21-6fe6-4df3-8d9d-103412fdac09"), new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"), "Southeast" },
                    { new Guid("88b2ca74-c15a-4233-94c3-47a916521736"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "North West" },
                    { new Guid("8d12024c-c78d-449d-9adc-de35518e406e"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Limpopo" },
                    { new Guid("a666453a-98aa-4189-9ab5-c4c5bc01892e"), new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"), "South" },
                    { new Guid("bf0abf09-2d00-41bc-a95f-e5277f382b6d"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Eastern Cape" },
                    { new Guid("d90bc179-371f-4bce-b756-6b32ab96b2a9"), new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"), "North" },
                    { new Guid("e2c1f27b-6be7-4483-aeac-d35994abbd5e"), new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"), "Northeast" },
                    { new Guid("e3da3f83-61e6-4a89-93c5-3db02d85686b"), new Guid("53439402-f7aa-4d36-b654-947c555b80d0"), "Gauteng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("01409dbc-d3c4-4c77-8b2e-99ae560e7a4b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2c70ae23-454b-4916-8a40-17e28a75ffec"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("49a3f3ab-57e7-422a-8b60-b45e068f0b3c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("500f8d34-f7e0-4ca2-9f7a-1d41ae166af3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("63b2726a-ac0b-4116-bd49-6d79b1f3001d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("65da3eab-f9d2-4e31-aeb9-3c9b8bd249cd"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6e62fbf8-c483-49cb-b028-dc506253b5bc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("77d7ac21-6fe6-4df3-8d9d-103412fdac09"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("88b2ca74-c15a-4233-94c3-47a916521736"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8d12024c-c78d-449d-9adc-de35518e406e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a666453a-98aa-4189-9ab5-c4c5bc01892e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bf0abf09-2d00-41bc-a95f-e5277f382b6d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d90bc179-371f-4bce-b756-6b32ab96b2a9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e2c1f27b-6be7-4483-aeac-d35994abbd5e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e3da3f83-61e6-4a89-93c5-3db02d85686b"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("3d1c862a-8f45-4f41-a3f3-570811a430cd"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("53439402-f7aa-4d36-b654-947c555b80d0"));
        }
    }
}
