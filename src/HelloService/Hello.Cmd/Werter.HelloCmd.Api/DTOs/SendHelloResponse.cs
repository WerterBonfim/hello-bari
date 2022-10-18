
using Hello.Common.DTOs;

namespace Werter.HelloCmd.Api.DTOs;

public class SendHelloResponse : BaseResponse
{
    public Guid Id { get; set; }
}