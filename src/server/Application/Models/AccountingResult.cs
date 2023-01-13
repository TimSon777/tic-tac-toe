namespace Application.Models;

public sealed class AccountingResult
{
    public required bool Succeeded { get; set; }

    public required List<string> Errors { get; set; }
}