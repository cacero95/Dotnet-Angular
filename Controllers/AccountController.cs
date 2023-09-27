using System.Security.Cryptography;
using API.DTOS;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ICodeResponseService _codeResponseService;
        public AccountController ( DataContext context, ITokenService tokenService, ICodeResponseService codeResponseService )
        {
            _codeResponseService = codeResponseService;
            _tokenService = tokenService;
            _context = context;
        }
        [HttpPost("register")]
        public async Task <ActionResult <UserDTO>> Register ( RegisterDTO registerDTO )
        {
            if ( await UserExist( registerDTO.Username ) ){
                return BadRequest( _codeResponseService.ResponseCode( 2 ));
            }
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash( Encoding.UTF8.GetBytes( registerDTO.Password )),
                PasswordSalt = hmac.Key
            }; 
            _context.Users.Add( user );
            await _context.SaveChangesAsync();
            return OutUser( user );
        }

        [HttpPost("login")]
        public async Task <ActionResult<UserDTO>> Login ( LoginDTO loginDTO ) { 
            var user = await _context.Users.SingleOrDefaultAsync( person => person.UserName == loginDTO.Username );
            if ( user == null || validateToken( user.PasswordSalt, user.PasswordHash, loginDTO.Password ) == false ) {
                return Unauthorized( _codeResponseService.ResponseCode( 3 ));
            }
            return OutUser( user );
        }
        private UserDTO OutUser( AppUser user ) {
            return new UserDTO {
                UserName = user.UserName,
                Token = _tokenService.CreateToken( user )
            };
        }
        private bool validateToken ( byte[] passwordSalt, byte[] passwordHash, string password ) {
            var validate = true;
            using var hmac = new HMACSHA512( passwordSalt );
            var computed = hmac.ComputeHash( Encoding.UTF8.GetBytes( password ) );
            for ( var index = 0; index < computed.Length; index++ ) {
                validate = computed[ index ] != passwordHash[ index ] ? false : validate;
            }
            return validate;
        }
        private async Task<bool> UserExist ( string username )
        {
            return await _context.Users.AnyAsync( user => user.UserName == username );
        }
    }
}