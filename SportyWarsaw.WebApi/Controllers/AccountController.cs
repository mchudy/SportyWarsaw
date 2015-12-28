using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Infrastructure;
using SportyWarsaw.WebApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly ApplicationUserManager userManager;

        public AccountController(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
        }

        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User { UserName = model.UserName, Email = model.Email };
            IdentityResult result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }

    }
}
