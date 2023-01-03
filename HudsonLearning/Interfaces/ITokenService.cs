using HudsonLearning.Entities;

namespace HudsonLearning.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
