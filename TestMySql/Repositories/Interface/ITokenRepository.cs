using Microsoft.AspNetCore.Identity;

namespace TestMySql.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);

    }
}
