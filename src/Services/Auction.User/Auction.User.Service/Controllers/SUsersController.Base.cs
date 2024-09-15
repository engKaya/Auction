using Auction.Core.Logging.Common.Interfaces;
using Auction.User.Domain.Service.Handlers.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.User.Service.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public partial class SUsersController : ControllerBase 
    { 
        IMediator mediator;
        public SUsersController(IMediator mediator)
        {    
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        { 
            await mediator.Send(command);
            return Ok(); 
        }
    }
}
