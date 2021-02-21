using Microsoft.EntityFrameworkCore.Migrations;

namespace Career.Migrations
{
	public partial class CreateTriggers : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
				CREATE OR ALTER TRIGGER [dbo].[CleanUpCVAfterUserDeleteTrigger]
				ON [dbo].[Users]
				AFTER DELETE
				AS
				BEGIN
					DECLARE @CVid INT;
					SELECT @CVid = d.cv_id FROM DELETED d;
					IF (@CVid IS NOT NULL)
					BEGIN
						DELETE FROM CVs WHERE id = @CVid;
					END
				END
			");

			migrationBuilder.Sql(@"
				CREATE OR ALTER TRIGGER [dbo].[CompanyJobDeactivationTrigger]
				ON [dbo].[Companies]
				AFTER UPDATE
				AS
				BEGIN
					SET NOCOUNT ON;
					IF UPDATE(deleted_at)
					BEGIN
					
						UPDATE [Jobs] SET is_active = (select (CASE WHEN deleted_at IS NULL THEN 1 ELSE 0 END) FROM INSERTED) WHERE company_id = (SELECT id FROM INSERTED);
					END;
				END
			");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[CompanyJobDeactivationTrigger]");
			migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS [dbo].[CleanUpCVAfterUserDeleteTrigger]");
		}
	}
}
