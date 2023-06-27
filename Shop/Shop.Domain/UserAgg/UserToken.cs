using System.Runtime.Versioning;
using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.UserAgg;

public class UserToken : BaseEntity
{
    public UserToken(string hasJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        HasJwtToken = hasJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
    public long UserId { get; internal set; }
    public string HasJwtToken {get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }

    public void Guard()
    {
        NullOrEmptyDomainDataException.CheckString(HasJwtToken, nameof(HasJwtToken));
        NullOrEmptyDomainDataException.CheckString(HashRefreshToken, nameof(HashRefreshToken));

        if (TokenExpireDate < DateTime.Now)
            throw new InvalidDomainDataException("Invalid Token ExpireDate");

        if (RefreshTokenExpireDate < TokenExpireDate)
            throw new InvalidDomainDataException("Invalid RefreshToken ExpireDate");
    }
}