using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class FullERD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Characters_CharacterId",
                table: "InventoryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_CharacterId",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InventoryItems");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "InventoryItems",
                newName: "InventoryId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Characters",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Characters",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "ItemTypeId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BerufId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReligionId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolkId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems",
                columns: new[] { "InventoryId", "ItemId" });

            migrationBuilder.CreateTable(
                name: "AbilityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmorStats",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Defense = table.Column<int>(type: "INTEGER", nullable: false),
                    Durability = table.Column<int>(type: "INTEGER", nullable: false),
                    Slot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorStats", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ArmorStats_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Berufe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berufe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableStats",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Effect = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Cooldown = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableStats", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ConsumableStats_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kämpfe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kämpfe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religionen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religionen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Völker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Völker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeaponStats",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    Range = table.Column<int>(type: "INTEGER", nullable: false),
                    Durability = table.Column<int>(type: "INTEGER", nullable: false),
                    WeaponType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponStats", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_WeaponStats_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Welten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Welten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MaxLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abilities_AbilityTypes_AbilityTypeId",
                        column: x => x.AbilityTypeId,
                        principalTable: "AbilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KampfLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KampfId = table.Column<int>(type: "INTEGER", nullable: false),
                    AngreiferId = table.Column<int>(type: "INTEGER", nullable: false),
                    VerteidigerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rolled = table.Column<int>(type: "INTEGER", nullable: false),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KampfLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KampfLogs_Characters_AngreiferId",
                        column: x => x.AngreiferId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KampfLogs_Characters_VerteidigerId",
                        column: x => x.VerteidigerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KampfLogs_Kämpfe_KampfId",
                        column: x => x.KampfId,
                        principalTable: "Kämpfe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KampfTeilnehmer",
                columns: table => new
                {
                    KampfId = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentHp = table.Column<int>(type: "INTEGER", nullable: false),
                    Initiative = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KampfTeilnehmer", x => new { x.KampfId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_KampfTeilnehmer_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KampfTeilnehmer_Kämpfe_KampfId",
                        column: x => x.KampfId,
                        principalTable: "Kämpfe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fraktionen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeltId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fraktionen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fraktionen_Welten_WeltId",
                        column: x => x.WeltId,
                        principalTable: "Welten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeltId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orte_Welten_WeltId",
                        column: x => x.WeltId,
                        principalTable: "Welten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAbilities", x => new { x.CharacterId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_BerufId",
                table: "Characters",
                column: "BerufId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_InventoryId",
                table: "Characters",
                column: "InventoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RaceId",
                table: "Characters",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ReligionId",
                table: "Characters",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_VolkId",
                table: "Characters",
                column: "VolkId");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_AbilityTypeId",
                table: "Abilities",
                column: "AbilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAbilities_AbilityId",
                table: "CharacterAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Fraktionen_WeltId",
                table: "Fraktionen",
                column: "WeltId");

            migrationBuilder.CreateIndex(
                name: "IX_KampfLogs_AngreiferId",
                table: "KampfLogs",
                column: "AngreiferId");

            migrationBuilder.CreateIndex(
                name: "IX_KampfLogs_KampfId",
                table: "KampfLogs",
                column: "KampfId");

            migrationBuilder.CreateIndex(
                name: "IX_KampfLogs_VerteidigerId",
                table: "KampfLogs",
                column: "VerteidigerId");

            migrationBuilder.CreateIndex(
                name: "IX_KampfTeilnehmer_CharacterId",
                table: "KampfTeilnehmer",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Orte_WeltId",
                table: "Orte",
                column: "WeltId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Berufe_BerufId",
                table: "Characters",
                column: "BerufId",
                principalTable: "Berufe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Inventories_InventoryId",
                table: "Characters",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Religionen_ReligionId",
                table: "Characters",
                column: "ReligionId",
                principalTable: "Religionen",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Völker_VolkId",
                table: "Characters",
                column: "VolkId",
                principalTable: "Völker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Inventories_InventoryId",
                table: "InventoryItems",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Berufe_BerufId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Inventories_InventoryId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Religionen_ReligionId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Völker_VolkId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Inventories_InventoryId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ArmorStats");

            migrationBuilder.DropTable(
                name: "AttributeDefinitions");

            migrationBuilder.DropTable(
                name: "Berufe");

            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "ConsumableStats");

            migrationBuilder.DropTable(
                name: "Fraktionen");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "KampfLogs");

            migrationBuilder.DropTable(
                name: "KampfTeilnehmer");

            migrationBuilder.DropTable(
                name: "Orte");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Religionen");

            migrationBuilder.DropTable(
                name: "Völker");

            migrationBuilder.DropTable(
                name: "WeaponStats");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Kämpfe");

            migrationBuilder.DropTable(
                name: "Welten");

            migrationBuilder.DropTable(
                name: "AbilityTypes");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_Characters_BerufId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_InventoryId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_RaceId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_ReligionId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_VolkId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BerufId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "VolkId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "InventoryItems",
                newName: "CharacterId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Characters",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Characters",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InventoryItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_CharacterId",
                table: "InventoryItems",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Characters_CharacterId",
                table: "InventoryItems",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
