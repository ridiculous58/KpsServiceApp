using API.Models;
using Infrastructure.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getuserbymail")]
        public IDataResult<UserModel> GetUserByMail([FromQuery]string mail)
        {
            var user = _userService.GetByMail(mail);
            var userRoles = _userService.GetClaims(user);
            return new SuccessDataResult<UserModel>(new UserModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = userRoles.Select(x => x.Name).ToArray()
            }, "Successful");
        }
       
    }
}
