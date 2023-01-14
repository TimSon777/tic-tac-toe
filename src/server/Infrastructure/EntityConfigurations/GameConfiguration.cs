using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityConfigurations;

public sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .Property(g => g.Board)
            .HasConversion<BoardConverter>();

        builder
            .Property(g => g.Status)
            .HasConversion<string>();

        builder
            .HasOne(game => game.Initiator)
            .WithOne(p => p.Game)
            .HasForeignKey<Game>("InitiatorId");
        
        builder
            .HasOne(game => game.Mate)
            .WithOne(p => p.Game)
            .HasForeignKey<Game>("MateId");
        
        builder.ToTable("Games");
    }
}

public sealed class BoardConverter : ValueConverter<Board, string>
{
    public BoardConverter() 
        : base(board => ConvertToString(board), str => ConvertToBoard(str))
    { }

    private static string ConvertToString(Board board)
    {
        return string.Join("", board.Moves
            .OfType<Move>()
            .Select(m => m.ToChar()));
    }

    private static Board ConvertToBoard(string moves)
    {
        var board = new Board
        {
            Moves = new Move[Board.Size, Board.Size]
        };
        
        for (var x = 0; x < Board.Size; x++)
        {
            for (var y = 0; y < Board.Size; y++)
            {
                board[x, y] = moves[x + y * Board.Size].FromChar();
            }
        }

        return board;
    }
}