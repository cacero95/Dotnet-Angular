using API.DTOS;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update( AppUser user );
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserById( int id );
        Task<AppUser> GetUserByUsernameAsync( string username );
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO> GetMember( string username );
    }
}