using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eBookStore.Repository.Migrations
{
    public partial class DbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishserId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Advance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Royalty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YTDSale = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Publisher_PublishserId",
                        column: x => x.PublishserId,
                        principalTable: "Publisher",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoyaltyPercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthor_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthor_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorId", "Address", "City", "EmailAddress", "FirstName", "LastName", "Phone", "State", "Zip" },
                values: new object[,]
                {
                    { 1, "Thu Duc District", "Thu Duc", "curry@gmail.com", "Stephen", "Curry", "0923999888", "Active", "71300" },
                    { 2, "Tan Phu District", "HCM", "beal@gmail.com", "Braley", "Beal", "0923777888", "Active", "76000" },
                    { 3, "Tan Binh District", "HCM", "clarkson@gmail.com", "Jordan", "Clarkson", "0923666777", "Active", "700915" }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "PublisherId", "City", "Country", "PublisherName", "State" },
                values: new object[,]
                {
                    { 1, "HCM", "Viet Nam", "Nha Nam", "Active" },
                    { 2, "Ha Noi", "Viet Nam", "Kinh Dong", "Active" },
                    { 3, "HCM", "Viet Nam", "Hai Dang", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Description" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Member" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Advance", "Note", "Price", "PublishedDate", "PublishserId", "Royalty", "Title", "Type", "YTDSale" },
                values: new object[,]
                {
                    { 1, 100000m, "Van hoc co dien Viet Nam", 54000m, new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8361), 1, 150000m, "Truyen Kieu", "Van hoc", 50 },
                    { 2, 200000m, "Tam ly hoc", 132000m, new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8363), 2, 250000m, "Tam ly hoc ve tien", "Tam ly", 40 },
                    { 3, 110000m, "Van hoc co dien", 81000m, new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8365), 3, 140000m, "Cay cam ngot cua toi", "Van hoc", 45 },
                    { 4, 130000m, "Van hoc co dien nuoc ngoai", 83000m, new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8367), 2, 180000m, "Nhung Cuoc Phieu Luu Cua Huckleberry Finn", "Van hoc", 60 },
                    { 5, 140000m, "Van hoc co dien nuoc ngoai", 98000m, new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8369), 1, 194000m, "Totto-chan Ben Cua So", "Van hoc", 78 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "EmailAddres", "FirstName", "HireDate", "LastName", "MiddleName", "Password", "PublisherId", "RoleId", "Source" },
                values: new object[,]
                {
                    { 1, "hieu@gmail.com", "Vuong", new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8328), "Hieu", "Huu", "1", 1, 2, "FPT" },
                    { 2, "trung@gmail.com", "Vuong", new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8340), "Trung", "Huu", "1", 2, 2, "FPT" },
                    { 3, "khang@gmail.com", "Vuong", new DateTime(2023, 10, 2, 22, 22, 28, 887, DateTimeKind.Local).AddTicks(8341), "Khang", "Huu", "1", 2, 2, "FPT" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "AuthorId", "BookId", "AuthorOrder", "RoyaltyPercentage" },
                values: new object[] { 1, 1, "Curry", 30.0 });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "AuthorId", "BookId", "AuthorOrder", "RoyaltyPercentage" },
                values: new object[] { 2, 2, "Beal", 35.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublishserId",
                table: "Book",
                column: "PublishserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PublisherId",
                table: "User",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
