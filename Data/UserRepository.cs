
using API.DTOS;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDTO> GetMember( string username )
        {
            return await _context.Users
                .Where( user => user.UserName == username )
                .ProjectTo<MemberDTO>( _mapper.ConfigurationProvider )
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        {
            return await _context.Users
                .ProjectTo<MemberDTO>( _mapper.ConfigurationProvider )
                .ToListAsync();
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _context.Users.FindAsync( id );
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include( photo => photo.Photos )
                .SingleOrDefaultAsync(
                    user => user.UserName == username
                );
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include( photo => photo.Photos )
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            // if the method return 0 its means that nothing was changed
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            // notify to the framework that something was changed
            _context.Entry( user ).State = EntityState.Modified;
        }
    }
}