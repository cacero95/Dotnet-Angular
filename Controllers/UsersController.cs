using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController( DataContext context )
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers() {
            return await _context.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task <ActionResult<AppUser>> GetUserById( int id ) {
            var user = await _context.Users.FindAsync( id );
            return user == null
                ? NotFound() : user;
        }
    }
}