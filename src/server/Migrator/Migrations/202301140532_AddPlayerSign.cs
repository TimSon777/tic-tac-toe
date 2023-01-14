namespace Migrator.Migrations;

[TimestampedMigration(2023, 1, 14, 5, 32)]
public sealed class AddPlayerSign : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Column("PlayerSign")
            .OnTable("Players")
            .AsString(30)
            .NotNullable()
            .SetExistingRowsTo("Nought");
    }
}