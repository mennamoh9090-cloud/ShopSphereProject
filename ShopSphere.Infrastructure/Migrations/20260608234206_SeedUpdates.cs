using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1498049794561-7780e7231661?w=800");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1489987707025-afc232f7ea0f?w=800");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=800");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 4, "خلاطات، ماكينات قهوة، وأدوات مطبخ", "https://images.unsplash.com/photo-1584622650111-993a426fbf0a?w=800", "أجهزة منزلية", null },
                    { 5, "أجهزة بلايستيشن، كروت شاشة، واكسسوارات ألعاب", "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f?w=800", "ألعاب وقيمينج", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 5, 1, "شاشة AMOLED 120Hz، كاميرا بدقة 200 ميجابكسل، وبطارية ضخمة", "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800", "هاتف ذكي ألترا برو", 28000.00m, 8 },
                    { 6, 1, "مراقبة ضربات القلب والأنشطة اليومية، مقاومة للماء بالكامل", "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=800", "ساعة ذكية رياضية", 3200.00m, 12 },
                    { 7, 2, "نعل مرن ومريح للمشي والجري لمسافات طويلة", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=800", "حذاء رياضي خفيف", 1800.00m, 25 },
                    { 8, 2, "جلد طبيعي ممتاز، بتصميم كلاسيكي رائع للطقس البارد", "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=800", "جاكيت جلد أسود عصري", 3500.00m, 10 },
                    { 9, 3, "أهم كتاب في التثقيف المالي وإدارة الأموال وتطوير الذات", "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=800", "كتاب الأب الغني والأب الفقير", 250.00m, 30 },
                    { 10, 3, "رواية الإثارة والغموض الأكثر مبيعاً للكاتب أحمد مراد", "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=800", "رواية الفيل الأزرق", 150.00m, 18 },
                    { 11, 4, "قوة 1200 واط مع إناء زجاجي متين لتحضير العصائر والشوربات", "https://images.unsplash.com/photo-1578643463396-0997cb5328c1?w=800", "خلاط كهربائي سريع", 2400.00m, 7 },
                    { 12, 5, "متوافق مع الكمبيوتر والبلايستيشن، استجابة سريعة جداً وبدون سلك", "https://images.unsplash.com/photo-1531525645387-7f14be1bdbbd?w=800", "ذراع تحكم ألعاب لاسلكي", 1600.00m, 14 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);
        }
    }
}
