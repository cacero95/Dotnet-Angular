using API.DTOS;

namespace API.Services
{
    public class CodeResponse : ICodeResponseService
    {
        public CodeResponse() {}

        public CodeResponseDto ResponseCode( int code ) {

            switch ( code ) {
                case 2:
                    return new CodeResponseDto {
                        Code = 2,
                        Message = "The user already exists"
                    };
                case 3:
                    return new CodeResponseDto {
                        Code = 3,
                        Message = "The user was not found"
                    };
                default: return new CodeResponseDto();
            }
        }
    }
}