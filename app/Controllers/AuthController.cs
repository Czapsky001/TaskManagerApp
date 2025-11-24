using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManagerApp.Model.Authentication;
using TaskManagerApp.Services.AuthenticationService;

namespace TaskManagerApp.Controllers
{
    /// <summary>
    /// Provides authentication endpoints such as user registration and login.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Service responsible for authentication operations.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">The registration data (email, username, password, company, role, etc.).</param>
        /// <returns>
        /// A <see cref="RegistrationResponse"/> containing basic information about the created user
        /// or validation errors if the registration failed.
        /// </returns>
        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(
                request.Email,
                request.Name,
                request.SurName,
                request.UserName,
                request.Password,
                request.CompanyId,
                request.Role);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(
                nameof(Register),
                new RegistrationResponse(result.Email, result.UserName));
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token on success.
        /// </summary>
        /// <param name="request">The authentication request containing email and password.</param>
        /// <returns>
        /// An <see cref="AuthResponse"/> with user information and JWT token if authentication succeeds;
        /// otherwise validation errors.
        /// </returns>
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(request.Email, request.Password);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(new AuthResponse(result.Email, result.UserName, result.Token));
        }

        /// <summary>
        /// Adds authentication errors from the <see cref="AuthResult"/> to the current <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <param name="result">The authentication result containing error messages.</param>
        private void AddErrors(AuthResult result)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
