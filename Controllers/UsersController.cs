using API.DTOS;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ Authorize ]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController( IUserRepository userRepository, IMapper mapper )
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        private MemberDTO GetMemberDTO( AppUser appuser ) {
            return _mapper.Map<MemberDTO>( appuser );
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers() {
            var users = await _userRepository.GetUsersAsync();
            return Ok( _mapper.Map<IEnumerable<MemberDTO>>( users ) );
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<MemberDTO>> GetUserById( int id ) {
            return GetMemberDTO( await _userRepository.GetUserById( id ) );
        }
        [HttpGet("username/{username}")]
        public async Task<ActionResult<MemberDTO>> GetuserByUserName( string username ) {
            return GetMemberDTO( await _userRepository.GetUserByUsernameAsync( username ));
        }
    }
}