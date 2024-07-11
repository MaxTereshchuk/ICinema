using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICinema.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
            INSERT INTO AspNetRoles (Id, Name, NormalizedName)
            VALUES 
            ('1', 'Admin', 'ADMIN'),
            ('2', 'User', 'USER');
            
        ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
