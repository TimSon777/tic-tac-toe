namespace Migrator.Migrations;

[TimestampedMigration(2023, 1, 14, 19, 5)]
public sealed class ChangeCreatedDateTime : ForwardOnlyMigration
{
    public override void Up()
    {
        Alter
            .Column("CreatedDateTimeUtc")
            .OnTable("Games")
            .AsDateTimeOffset()
            .NotNullable();
    }
}