using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;
namespace SportyWarsaw.WebApi.Assemblers
{
    public interface IUserAssembler
    {
        UserModel ToUserModel(User entity);
        UserPlusModel ToUserPlusModel(User entity);
    }
}
