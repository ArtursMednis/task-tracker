using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskTracker.Application.Contracts.Identity;

namespace TaskTracker.Identity.Services
{
    public class HttpCurrentUser(IHttpContextAccessor http) : ICurrentUser
    {
        public string UserId => http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();

    }
}
