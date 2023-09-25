namespace API.Controllers
{
    public class BugsController : BaseApiController
    {
        private readonly DataContext _context;
        public BugsController( DataContext context )
        {
            _context = context;
        }
        [ HttpGet( "auth" ) ]
        public ActionResult<string> GetSecret() {
            return "";
        }
        [ HttpGet( "notFound" ) ]
        public ActionResult<AppUser> GetNotFound() {
            var validate = _context.Users.Find(-1);
            return validate == null ? NotFound() : validate;
        }
        [ HttpGet( "serverError" ) ]
        public ActionResult<string> GetServerError() {
            return _context.Users.Find(-1).ToString();
        }
        [ HttpGet( "badRequest" ) ]
        public ActionResult<string> GetBadRequest() {
            return BadRequest();
        }
    }
}