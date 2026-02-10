namespace TaskTracker.Api.Controllers.Account.Models
{
    public class RegisterRequest
    {
        public required string Username { get; set; }

        /// <summary>
        /// The password for the new user. This should be a strong password that meets the application's security requirements.
        /// </summary>
        public required string Password { get; set; }
    }
}
