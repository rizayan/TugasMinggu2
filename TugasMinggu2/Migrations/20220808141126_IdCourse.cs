using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TugasMinggu2.Migrations
{
    public partial class IdCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Courses",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courses",
                newName: "CourseID");
        }
    }
}
