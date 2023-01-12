using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2023, 1, 1, 0, 0)]
public sealed class TestMigration : ForwardOnlyMigration
{
    public override void Up()
    { }
}