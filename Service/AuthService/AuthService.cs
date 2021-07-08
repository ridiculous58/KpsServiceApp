using DataAccess.Interfaces;
using Entities.Dtos;
using Infrastructure.Aspects.Autofac.Logging;
using Infrastructure.Aspects.Autofac.Validation;
using Infrastructure.CrossCuttingConcerns.Logging.ElasticLogger;
using Infrastructure.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Infrastructure.Entities.Concrete;
using Infrastructure.Utilities.BusinessLogic;
using Infrastructure.Utilities.Results;
using Infrastructure.Utilities.Security.Hashing;
using Infrastructure.Utilities.Security.Jwt;
using Service.Constants;
using Service.UserControlService;
using Service.UserControlService.KpsServiceAdapter.Extensions;
using Service.UserService;
using Service.ValidationRuleServices.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthService
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private readonly IUserControlService _userControlService;
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimDal _operationClaimDal;
        public AuthService(IUserService userService, ITokenHelper tokenHelper,IUserControlService userControlService,
            IUserOperationClaimDal userOperationClaimDal,
            IOperationClaimDal operationClaimDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userControlService = userControlService;
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimDal = operationClaimDal;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Select(x=> new OperationClaim { Id = x.Id,Name =x.Name }).ToList());
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
        [ValidationAspect(typeof(UserForRegisterDtoValidator))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                TcNo = userForRegisterDto.TcNo
            };
            user = _userControlService.FillUserInfo(user);
            _userService.Add(user);
            var operationCliamId = _operationClaimDal.Get(x=>x.Name == "User").Id;
            _userOperationClaimDal.Add(new UserOperationClaim { UserId = user.Id, OperationClaimId = operationCliamId });
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
