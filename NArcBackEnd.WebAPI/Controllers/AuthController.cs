﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Register")]
        public IActionResult Register(AuthRegisterDto authDto)
        {
            _authService.Register(authDto);
            return Ok("Kullanici kayit başarı ile tamamlandı!");
        }

        [HttpPost("Login")]
        public IActionResult Login(AuthLoginDto authLoginDto)
        {
            var result = _authService.Login(authLoginDto);
            return Ok(result);
        }
    }
}