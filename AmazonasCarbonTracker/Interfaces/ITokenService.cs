using AmazonasCarbonTracker.Models;

namespace AmazonasCarbonTracker.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
