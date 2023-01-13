namespace Migrator.Migrations;

[TimestampedMigration(2023, 1, 13, 18, 29)]
public sealed class AddGameAndPlayer : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Table("Players")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().ForeignKey().Indexed();

        Create
            .Table("Games")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("InitiatorId").AsInt32().ForeignKey().Indexed()
            .WithColumn("MateId").AsInt32().Nullable().ForeignKey().Indexed()
            .WithColumn("Board").AsString(9).NotNullable()
            .WithColumn("Status").AsString(30).NotNullable()
            .WithColumn("CreatedDateTimeUtc").AsDateTime().NotNullable();
    }
}