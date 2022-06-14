using ApplicationLayer.Application;
using DataAccess.Model;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class AccountController : ControllerBase
    {

        private readonly IUserManager _user;

        public AccountController(IUserManager user)
        {
          this.  _user = user;

        }






        
        [HttpPost]
        [Route("api/values/UserSignUp")]
        public async Task<IActionResult> UserSignUp([FromBody] User userSignUp)
        {
            if (userSignUp == null)
            {
                return Ok(0);
            }
            else
            {
                var userExistence = _user.UserExistence(userSignUp);
                if (userExistence == 1)
                {
                    return Ok(0);
                }
                else
                {
                    string SignUpUser = _user.SignUp(userSignUp);
                  
                    return Ok(SignUpUser);
                }
            }
        }
        [HttpPost("api/values/GetCurrentUserRole")]
        public async Task<IActionResult> GetCurrentUserRole([FromBody] User userModelDto)
        {
            if (userModelDto == null)
            {
                return Ok(0);
            }
            else
            {
              

          


           string newUserStatus = _user.UserStatus(userModelDto);
                UserInfo userRole = _user.GetRole(userModelDto);
                return Ok(userRole);
            }

        }
    }
}
