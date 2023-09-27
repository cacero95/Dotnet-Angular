using API.DTOS;

namespace API.Interfaces
{
    public interface ICodeResponseService
    {
        CodeResponseDto ResponseCode( int code );
    }
}