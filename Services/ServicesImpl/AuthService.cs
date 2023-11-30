using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.IServices;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Data.Constants;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata;
using Data.Repositories.IRepositories;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthService> _logger;
        private readonly IProfileRepo _profileRepo;
        private readonly JWTService _jwtService;

        public AuthService(
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            JWTService jwtService, ILogger<AuthService> logger, IProfileRepo profileRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _profileRepo = profileRepo;
            _logger = logger;
        }

        public async Task<ApiResponse<Tokens>> Login(LoginDto model)
        {
            var response = new ApiResponse<Tokens>();

            try
            {
                // check if user exists
                var user = await _userManager.FindByEmailAsync(model.Email);

                // check password if user exists
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    // add user details and roles to claims
                    TokenDto token = new()
                    {
                        UserId = user.Id,
                        Email = model.Email,
                        Roles = roles.ToList()
                    };

                    response = _jwtService.GenerateToken(token);
                    response.Message = "Login successful";

                    return response;
                }

                // else unauthorized
                response.Status = HttpStatusCode.Unauthorized;
                response.Message = "Email/Password Invalid";

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at AuthService::Login => message::{message}", ex.Message);

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public ApiResponse<string> Me(string email)
        {
            var response = new ApiResponse<string>
            {
                Message = "Email retrieved",
                Data = email,
                Status = HttpStatusCode.OK
            };

            if (email.IsNullOrEmpty())
            {
                response.Message = "User not authenticated";
                response.Status = HttpStatusCode.Forbidden;
            }

            return response;
        }

        public async Task<ApiResponse<Tokens>> SignUp(SignUpDto model)
        {
            var response = new ApiResponse<Tokens>();

            try
            {
                // get user by email
                var user = await _userManager.FindByEmailAsync(model.Auth!.Email!);

                // if exists, throw error
                if (user != null)
                {
                    response.Status = HttpStatusCode.Forbidden;
                    response.Message = "Account already exists";

                    return response;
                }

                // if new user, create user and profile
                var tempAppUser = new ApplicationUser()
                {
                    Email = model.Auth.Email,
                    PhoneNumber = model.Profile!.PhoneNumber,
                    UserName = model.Auth.Email,
                };

                var createUserResponse = await _userManager.CreateAsync(tempAppUser, model.Auth.Password!);

                if (createUserResponse.Succeeded)
                {
                    // create user profile
                    await CreateProfile(tempAppUser.Id, model.Profile);

                    // get token
                    TokenDto tokenModel = new()
                    {
                        UserId = tempAppUser.Id,
                        Email = tempAppUser.Email,
                        Roles = new()
                    };

                    response = _jwtService.GenerateToken(tokenModel);
                    response.Status = HttpStatusCode.Created;
                    response.Message = "Account created successfully";

                    return response;
                }

                response.Message = "Account could not be created. Try again later";
                response.Status = HttpStatusCode.BadRequest;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at AuthService::SignUp => message::{message}", ex.Message);

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }


        #region PRIVATE
        private async Task CreateOrAddUserToRoles(ApplicationUser user, List<string> roles)
        {
            foreach (var role in roles)
            {
                // create role if not existent
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                // add user to role group if role exists
                if (await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }

        private async Task CreateProfile(string user, SignUpProfileDto model)
        {
            Profile profile = new()
            {
                Country = model.Country,
                DateJoined = DateTime.Now,
                DateOfBirth = model.DateOfBirth,
                OtherNames = model.OtherNames,
                Surname = model.Surname,
                User = user
            };

            await _profileRepo.CreateProfileAsync(profile);
        }
        #endregion
    }
}