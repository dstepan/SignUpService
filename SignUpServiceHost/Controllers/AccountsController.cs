using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SignUpService.DTO;
using SignUpService.Repositories;
using SignUpService.Validation;

namespace SignUpServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("create")]
        [EnableCors]
        public async Task<ActionResult> CreateAccount([FromBody] SignUpRequest request)
        {
            try
            {
                var validator = new SignUpRequestValidator();
                await validator.ValidateAndThrowAsync(request);
                await accountRepository.CreateAccountAsync(request.Email, request.Password);
                return Ok();
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }

        [HttpPost]
        [Route("login")]
        [EnableCors]
        public async Task<ActionResult> LogIn([FromBody] SignInRequest request)
        {
            try
            {
                bool result = await accountRepository.SignInAsync(request.Email, request.Password);
                return result ? Ok() : BadRequest("Sign in failed.");
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }

        [HttpPost]
        [Route("logout")]
        [EnableCors]
        public async Task<ActionResult> LogOut()
        {
            try
            {
                await accountRepository.SignOutAsync();
                return Ok();
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }
    }
}
