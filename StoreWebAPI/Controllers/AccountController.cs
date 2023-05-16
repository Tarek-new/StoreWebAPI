using AutoMapper;
using Core.Enitities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebAPI.Dtos;
using StoreWebAPI.ResponseStatusModules;
using System.Security.Claims;

namespace StoreWebAPI.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidaionErrorResponse
                {
                    Errors = new[] { "Email Address Already Exist !" }

                });
            }
            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Email,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));
            return new UserDto
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,

            };
        }


        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));
            var UserDto = new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
            return Ok(UserDto);
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new ApiResponse(400));
            var userDto = new UserDto
            {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            };
            return Ok(userDto);
        }


        [Authorize]
        [HttpGet("UserAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
            var mappedAddress = _mapper.Map<AddressDto>(user.Address);
            return Ok(mappedAddress);
        }


        [Authorize]
        [HttpPost("UserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
            user.Address = _mapper.Map<Address>(addressDto);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var mappedAddress = _mapper.Map<AddressDto>(user.Address);
                return Ok(mappedAddress);

            }
            return BadRequest(new ApiResponse(400, "Updating address Failed"));
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmailAsync([FromQuery] string email)

           => await _userManager.FindByEmailAsync(email) != null;

    }
}
