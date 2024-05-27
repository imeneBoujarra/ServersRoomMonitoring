using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class updatedChecklistEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ventulation",
                table: "Checkliste",
                newName: "Ventilation");

            migrationBuilder.RenameColumn(
                name: "Id_checklist",
                table: "Checkliste",
                newName: "IdChecklist");

            migrationBuilder.AddColumn<string>(
                name: "QRCodeUrl",
                table: "Checkliste",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeUrl",
                table: "Checkliste");

            migrationBuilder.RenameColumn(
                name: "Ventilation",
                table: "Checkliste",
                newName: "Ventulation");

            migrationBuilder.RenameColumn(
                name: "IdChecklist",
                table: "Checkliste",
                newName: "Id_checklist");
        }
    }
}
