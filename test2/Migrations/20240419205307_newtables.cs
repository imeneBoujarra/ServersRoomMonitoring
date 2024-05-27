using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checkliste",
                columns: table => new
                {
                    Id_checklist = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeatPictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SwitchersPictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Backbone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ventulation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Security = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Storage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkliste", x => x.Id_checklist);
                });

            migrationBuilder.CreateTable(
                name: "Hitoricals",
                columns: table => new
                {
                    Date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    List = table.Column<byte[]>(type: "varbinary(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hitoricals", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "ServersRoom",
                columns: table => new
                {
                    Id_Room = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Number = table.Column<int>(type: "int", nullable: false),
                    Servers_Numbers = table.Column<int>(type: "int", nullable: false),
                    Machines = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServersRoom", x => x.Id_Room);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkliste");

            migrationBuilder.DropTable(
                name: "Hitoricals");

            migrationBuilder.DropTable(
                name: "ServersRoom");
        }
    }
}
