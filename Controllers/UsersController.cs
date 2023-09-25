using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public UsersController( DataContext context, ITokenService tokenService )
        {
            _context = context;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers() {
            var user = HttpContext.User.Claims;
            return await _context.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task <ActionResult<AppUser>> GetUserById( int id ) {
            var user = await _context.Users.FindAsync( id );
            var userClaim = HttpContext.User.Claims;
            return user == null
                ? NotFound() : user;
        }
    }
}