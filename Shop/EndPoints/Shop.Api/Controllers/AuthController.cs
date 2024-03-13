using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Shop.Application.Users.RemoveToken;
using UAParser;

namespace Shop.Api.Controllers
{

    public class AuthController : ApiController
    {
        private readonly IUserFacade _userFacade;
        private readonly IConfiguration _configuration;

        public AuthController(IUserFacade userFacade, IConfiguration configuration)
        {
            _userFacade = userFacade;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ApiResult<LoginResultDto?>> Login(LoginViewModel loginViewModel)
        {

            var user = await _userFacade.GetUserByPhoneNumber(loginViewModel.PhoneNumber);
            if (user == null)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                 return CommandResult(result);
            }

            var isPasswordCorrect = Sha256Hasher.IsCompare(user.Password, loginViewModel.Password);

            if (isPasswordCorrect == false)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                return CommandResult(result);
            }

            if (user.IsActive == false)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                return CommandResult(result);
            }

            var loginResult = await AddTokenAndGenerateJwt(user);

            return CommandResult(loginResult);
        }

        [HttpPost("Register")]
        public async Task<ApiResult> Register(RegisterViewModel registerViewModel)
        {

            var command = new RegisterUserCommand(registerViewModel.PhoneNumber, registerViewModel.Password);
            var result = await _userFacade.RegisterUser(command);
            return CommandResult(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
        {
            var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);

            if (result == null)
             return CommandResult(OperationResult<LoginResultDto?>.NotFound());

            if (result.TokenExpireDate > DateTime.Now)
                return CommandResult(OperationResult<LoginResultDto?>.Error("توکن هنوز منقضی نشده است"));

            if (result.RefreshTokenExpireDate < DateTime.Now)
            {
                return CommandResult(OperationResult<LoginResultDto?>.Error("رفرش توکن منقضی شده باید دوباره لاگین کنید"));
            }

            await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
             var user = await _userFacade.GetUserById(result.UserId);
             var loginResult = await AddTokenAndGenerateJwt(user);
             return CommandResult(loginResult);


        }

        [Authorize]
        [HttpDelete("Logout")]
        public async Task<ApiResult> Logout()
        {
            
            var token = await HttpContext.GetTokenAsync("access_token");
            var userToken = await _userFacade.GetUserTokenByJwtToken(token);
            if (userToken == null)
                return CommandResult(OperationResult.NotFound());

            await _userFacade.RemoveToken(new RemoveUserTokenCommand(userToken.UserId, userToken.Id));
            return CommandResult(OperationResult.Success());
        }

        private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
        {
            var uaParser = Parser.GetDefault();
            var info = uaParser.Parse(HttpContext.Request.Headers["user-agent"]);
            var device = $"{info.Device.Family}/{info.OS.Family}{info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
            var refreshToken = Guid.NewGuid().ToString();
            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            var hashToken = Sha256Hasher.Hash(token);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashToken, hashRefreshToken
                , DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

            if (tokenResult.Status != OperationResultStatus.Success)
                return OperationResult<LoginResultDto?>.Error();

            return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }
    }
}  
