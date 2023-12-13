﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyTimer.Application.Models;
using StudyTimer.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudyTimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IToastNotification _toastNotification;
        //private readonly IResend _resend;
        private readonly IWebHostEnvironment _environment;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthRegisterViewModel registerViewModel)
        {
            var userId = Guid.NewGuid();

            if (!ModelState.IsValid)
                return BadRequest(registerViewModel);

            var user = new User()
            {
                Id = userId,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Gender = registerViewModel.Gender,
                BirthDate = registerViewModel.BirthDate.Value.ToUniversalTime(),
                UserName = registerViewModel.LastName,
                CreatedOn = DateTime.UtcNow,
                CreatedByUserId = userId.ToString()
            };



            var identityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                // User creation was successful.
                // You might want to customize the response based on your requirements.
                return Ok(new { Message = "User registration successful." });
            }
            else
            {
                // User creation failed. Return an appropriate response.
                return BadRequest(new { Errors = identityResult.Errors });
            }
        }


    }
}