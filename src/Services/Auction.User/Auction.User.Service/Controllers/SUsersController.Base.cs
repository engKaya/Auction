using Auction.User.Common.Infastructure.UnitOfWork;
using Auction.User.Entity.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auction.User.Service.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public partial class SUsersController : ControllerBase 
    { 
        ILogger<SUsersController> _logger;
        IUserServiceUnitOfWork _unitOfWork;

        public SUsersController(ILogger<SUsersController> logger, IUserServiceUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<IActionResult> GetUsers()   
        {
            var user = new UserEntity();
            user.Email = "ibrahimsabitkaya@gmail.com";

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();


            var users = _unitOfWork.UserRepository.GetAll();
            return Task.FromResult<IActionResult>(Ok(users));
        }
    }
}
