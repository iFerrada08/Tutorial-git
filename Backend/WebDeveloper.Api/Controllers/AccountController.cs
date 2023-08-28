using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.Requests;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDeveloper.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IChinookContext _context;
        public AccountController(ILogger<AccountController> logger, IChinookContext context)
        {
            _logger = logger;
            _context = context;
        }
        // POST /account/token
        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody]LoginRequest request)
        {
            // Obtener el usuario de la BD
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if(user == null)
            {
                // Retornar el 401
                return StatusCode(401, "Usuario no autorizado");
            }

            // Crear la identidad del usuario (claims)
            var identidad = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("DNI", user.Dni)
            };

            // Crear los objetos que nos van a servir para crear el JWT
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("cibertec12345678"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Generar el JWT
            // Setear una fecha de expiracion al token
            var expirationSeconds = 120;
            var jwtExpirationDate = DateTime.Now.AddSeconds(expirationSeconds);

            var token = new JwtSecurityToken(
                issuer: "Cibertec",
                audience: "app-js",
                claims: identidad,
                expires: jwtExpirationDate,
                signingCredentials: credenciales
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwtToken });
        }
    }
}
