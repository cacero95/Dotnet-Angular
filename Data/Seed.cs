using System.Security.Cryptography;
using System.Text.Json;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUser( DataContext context ) {
            if ( !await context.Users.AnyAsync() ) {
                var userData = await File.ReadAllTextAsync( "Data/UserSeedData.json" );
                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                var users = JsonSerializer.Deserialize<List<AppUser>>( userData, options );

                foreach( var user in users ) {
                    using var hmac = new HMACSHA512();
                    user.UserName = user.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash( Encoding.UTF8.GetBytes( "a09Z3DT$QLTm" ));
                    user.PasswordSalt = hmac.Key;
                    context.Users.Add( user );
                }
                await context.SaveChangesAsync(); 
            } return;
        }
    }
}