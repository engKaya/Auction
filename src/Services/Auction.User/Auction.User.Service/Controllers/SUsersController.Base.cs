using Auction.Core.Base.Common.Enums;
using Auction.Core.Base.Common.Infastructure;
using Auction.Core.Logging.Common.Interfaces;
using Auction.User.Domain.Service.Handlers.Commands.RegisterUser;
using Auction.User.Domain.Service.Handlers.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Auction.User.Service.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public partial class SUsersController : ControllerBase
    {
        IMediator _mediator;
        IServiceProvider _provider;
        public SUsersController(IMediator mediator, IServiceProvider provider)
        { 
            this._mediator = mediator;
            _provider = provider;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            command.RequestTypes = RequestTypes.Command; 
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginQuery query)
        {
            query.RequestTypes = RequestTypes.Query;
            var res = await _mediator.Send(query);
            return Ok(res);
        }
    }
}
