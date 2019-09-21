using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUploader.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 25, nullable: false),
                    Label = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "DeliveredIns",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveredIns", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ArticlePrices",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 25, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    DiscountPrice = table.Column<decimal>(nullable: false),
                    ArticleCode = table.Column<string>(maxLength: 25, nullable: false),
                    DeliveredInCode = table.Column<string>(maxLength: 25, nullable: false),
                    SectionCode = table.Column<string>(maxLength: 25, nullable: false),
                    SizeCode = table.Column<string>(maxLength: 25, nullable: false),
                    ColorCode = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePrices", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ArticlePrices_Articles_ArticleCode",
                        column: x => x.ArticleCode,
                        principalTable: "Articles",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePrices_Colors_ColorCode",
                        column: x => x.ColorCode,
                        principalTable: "Colors",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePrices_DeliveredIns_DeliveredInCode",
                        column: x => x.DeliveredInCode,
                        principalTable: "DeliveredIns",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePrices_Sections_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Sections",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePrices_Sizes_SizeCode",
                        column: x => x.SizeCode,
                        principalTable: "Sizes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePrices_ArticleCode",
                table: "ArticlePrices",
                column: "ArticleCode");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePrices_ColorCode",
                table: "ArticlePrices",
                column: "ColorCode");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePrices_DeliveredInCode",
                table: "ArticlePrices",
                column: "DeliveredInCode");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePrices_SectionCode",
                table: "ArticlePrices",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePrices_SizeCode",
                table: "ArticlePrices",
                column: "SizeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlePrices");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "DeliveredIns");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Sizes");
        }
    }
}
