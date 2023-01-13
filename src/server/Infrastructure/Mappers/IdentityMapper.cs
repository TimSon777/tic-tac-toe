using Application.Models;
using Microsoft.AspNetCore.Identity;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Mappers;

[Mapper]
public static partial class IdentityMapper
{
    public static AccountingResult MapToRegistrationResult(this IdentityResult identityResult)
    {
        return new AccountingResult
        {
            Succeeded = identityResult.Succeeded,
            Errors = identityResult.Errors.Select(error => error.Code).ToList()
        };
    }
}