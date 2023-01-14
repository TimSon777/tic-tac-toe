namespace Migrator.Migrations;

[TimestampedMigration(2023, 1, 14, 6, 7)]
public sealed class AddRating : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Column("Rating")
            .OnTable("Users")
            .AsInt32()
            .SetExistingRowsTo(0)
            .NotNullable();
    }
}