using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_scheduler.api.Migrations
{
    /// <inheritdoc />
    public partial class update_events_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attended",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attended",
                table: "Events");
        }
    }
}
