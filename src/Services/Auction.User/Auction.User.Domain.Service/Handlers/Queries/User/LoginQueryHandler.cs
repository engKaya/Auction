using Auction.Core.Base.Common.Infastructure;
using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Service.MediatR;
using Auction.User.Common.Infastructure.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auction.User.Domain.Service.Handlers.Queries.User
{
    public class LoginQueryHandler : BaseRequestHandler<LoginQuery, BaseResponse<LoginQueryResponse>>
    {
        private readonly IUserServiceUnitOfWork _uof;
        public LoginQueryHandler(ITraceService trace,IUserServiceUnitOfWork uof) : base(trace) 
        {
            _uof = uof;
        }
        protected override Task<BaseResponse<LoginQueryResponse>> HandleRequest(LoginQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<LoginQueryResponse>();

            var user = _uof.UserRepository.FindOne(x => x.Email == request.Email);
            if (user is null)
            {
                response.ProcessCode = BaseProcessCodes.PasswordOrMailInCorrect;
                return Task.FromResult(response);
            } 

            var passverify = user.VerifyPassword(request.Password);
            if (!passverify)
            {
                response.ProcessCode = BaseProcessCodes.PasswordOrMailInCorrect;
                return Task.FromResult(response);
            }

            response.Data.Email = user.Email;


            var claims = new Claim[]
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TooImportantSecretKey2024Auction!!"));
            var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddHours(2);
            var token = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: creeds, notBefore: DateTime.Now);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            response.Data.Token  = encodedToken;
            response.Data.Expire = expire;
            return Task.FromResult(response);
        }
    }
}
