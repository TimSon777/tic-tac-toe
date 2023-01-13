using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2023, 1, 13, 11, 7)]
public sealed class AddUser : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString(256).Nullable().Unique()
            .WithColumn("PasswordHash").AsString(200).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable().Unique()
            .WithColumn("NormalizedEmail").AsString(256).Nullable().Unique()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("AccessFailedCount").AsInt32().Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().Nullable()
            .WithColumn("LockoutEnd").AsDateTime().Nullable()
            .WithColumn("PhoneNumber").AsString(20).Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().Nullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().Nullable();
    }
}