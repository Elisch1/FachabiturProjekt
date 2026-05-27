using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class MakeKampfLogIdsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KampfLogs_Characters_AngreiferId",
                table: "KampfLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_KampfLogs_Characters_VerteidigerId",
                table: "KampfLogs");

            migrationBuilder.AlterColumn<int>(
                name: "VerteidigerId",
                table: "KampfLogs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AngreiferId",
                table: "KampfLogs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_KampfLogs_Characters_AngreiferId",
                table: "KampfLogs",
                column: "AngreiferId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KampfLogs_Characters_VerteidigerId",
                table: "KampfLogs",
                column: "VerteidigerId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KampfLogs_Characters_AngreiferId",
                table: "KampfLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_KampfLogs_Characters_VerteidigerId",
                table: "KampfLogs");

            migrationBuilder.AlterColumn<int>(
                name: "VerteidigerId",
                table: "KampfLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AngreiferId",
                table: "KampfLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KampfLogs_Characters_AngreiferId",
                table: "KampfLogs",
                column: "AngreiferId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KampfLogs_Characters_VerteidigerId",
                table: "KampfLogs",
                column: "VerteidigerId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
