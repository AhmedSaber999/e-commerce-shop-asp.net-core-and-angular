using System.Security.Claims;
using System.Threading.Tasks;
using API.DataShape;
using API.Errors;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using API.Extensions;
using AutoMapper;
using System.Collections.Generic;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                ITokenService tokenService,
                                IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user == null) return Unauthorized(new ApiResponse(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto{
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                Username = user.UserName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            if(CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                var apiValidationErrorResponse = new ApiValidationErrorResponse();
                apiValidationErrorResponse.Errors = new []{"Email address is in use"};
                
                return new BadRequestObjectResult(apiValidationErrorResponse);
            }
            // foreach(var u in userManager.Users){
            //     await userManager.DeleteAsync(u);
            // }
            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                Address = new Address{
                    Country = "Egypt",
                    City = "Cairo",
                    Street = "Al Sayed Al Bably",
                    ZipCode = "1243",
                    State = "A3"
                }
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpGet]
        [Authorize]
        public ActionResult<UserDto> GetCurrentUserAsync()
        {
            var user = userManager.FindByEmailWithAddress(User);
            
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailexit")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        [Authorize]
        public ActionResult<AddressDto> GetUserAddress()
        {
            var user = userManager.FindByEmailWithAddress(HttpContext.User);
            return mapper.Map<Address, AddressDto>(user.Address);
        }

        [HttpPost("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto address)
        {
            var user = userManager.FindByEmailWithAddress(HttpContext.User);
            user.Address = mapper.Map<AddressDto, Address>(address);

            var result = await userManager.UpdateAsync(user);
            if(result.Succeeded) return mapper.Map<Address, AddressDto>(user.Address);

            return BadRequest("Problem updating the user address");
        }
    }   
}