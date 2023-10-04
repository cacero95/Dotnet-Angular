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
        private readonly ICodeResponseService _codeResponseService;

        public UsersController( IUserRepository userRepository, IMapper mapper, ICodeResponseService codeResponseService )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _codeResponseService = codeResponseService;
        }
        private MemberDTO GetMemberDTO( AppUser appuser ) {
            return _mapper.Map<MemberDTO>( appuser );
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<MemberDTO>>> GetUsers() {
            return Ok( await _userRepository.GetMembersAsync() );
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<MemberDTO>> GetUserById( int id ) {
            return GetMemberDTO( await _userRepository.GetUserById( id ) );
        }
        [HttpGet("username/{username}")]
        public async Task<ActionResult<MemberDTO>> GetuserByUserName( string username ) {
            // return GetMemberDTO( await _userRepository.GetUserByUsernameAsync( username ));
            var user = await _userRepository.GetMember( username );
            if ( user == null ) {
                return NotFound( _codeResponseService.ResponseCode( 3 ));
            }
            return user;
        }
    }
}