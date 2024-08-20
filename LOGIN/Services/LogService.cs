using LOGIN.Dtos.Log;
using LOGIN.Dtos;
using LOGIN.Entities;
using LOGIN.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LOGIN.Services
{
    public class LogService : ILogService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;

        public LogService(IHttpContextAccessor httpContextAccessor)
        {

            _httpContext = httpContextAccessor.HttpContext;

            var idClaim = _httpContext.User.Claims.Where(x => x.Type == "UserId")
            .FirstOrDefault();
            _USER_ID = idClaim?.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateLogAsync(string action, string state)
        {

            var log = new LogEntity()
            {

                Id = Guid.NewGuid(),
                Action = action,
                State = state

            };

        }

    }
}
