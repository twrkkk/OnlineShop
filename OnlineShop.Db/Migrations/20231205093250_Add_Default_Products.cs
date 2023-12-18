using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class Add_Default_Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "IconSource", "Name" },
                values: new object[,]
                {
                    { new Guid("486951c4-63a2-42f0-9681-de01f0e7bba3"), 149.99m, "Насыщенный вкус с выразительной кислинкой — главное, за что любят апельсины. Плотная, приятная на ощупь цедра скрывает нежную и очень сочную мякоть. Слегка пьянящий, душистый аромат дополняет это цитрусовое удовольствие.", "https://i.servimg.com/u/f62/19/52/39/22/orange10.jpg", "Апельсины" },
                    { new Guid("7c0912d4-01a2-435b-b695-e2b9da93c009"), 109.99m, "Питательный фрукт с мягкой, нежной текстурой, выразительным сладким вкусом и насыщенным ароматом. Банан легко чистится от своей золотисто-жёлтой кожуры, обнажая бежевую мякоть.", "https://iberiamercaexport.es/images/banana.jpg", "Бананы" },
                    { new Guid("8ccac07f-6322-4850-a6ab-954e873c485f"), 89.99m, "У яблок сорта Голден золотистая кожура с мелкими коричневыми точками. Под ней — сочная, сладкая и душистая мякоть с медовыми нотками во вкусе и лёгкими пряными оттенками в аромате.", "https://d2j6dbq0eux0bg.cloudfront.net/images/67958791/2757175015.jpg", "Яблоки Голден" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("486951c4-63a2-42f0-9681-de01f0e7bba3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7c0912d4-01a2-435b-b695-e2b9da93c009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8ccac07f-6322-4850-a6ab-954e873c485f"));
        }
    }
}
